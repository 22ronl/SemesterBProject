using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.ComponentModel;
namespace FlightSimulator.Model
{
    class InfoServer : INotifyPropertyChanged
    {
        private bool isProgramAlive = true;
        private TcpListener listener = null;
        private TcpClient client = null;
        private Nullable<float>lon =null;
        private Nullable<float>lat=null;
        private static InfoServer m_Instance = null;
        private static Mutex mutex = new Mutex();
        public event PropertyChangedEventHandler PropertyChanged;
        // singleton
        public static InfoServer Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new InfoServer();
                }
                return m_Instance;
            }
        }
        // will change in case there the disconnect button was pressed 
        public bool IsProgramAlive
        {
            get
            {
                return isProgramAlive;
            }
            set
            {
                mutex.WaitOne();
                isProgramAlive = value;
                mutex.ReleaseMutex();
            }
        }
        public Nullable<float> Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }
        public Nullable<float> Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }
        // open the server on a thread
        public void openServer()
        {
            new Thread(delegate ()
            {
                listen(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
                if (IsProgramAlive)
                {
                    readFromServer();
                }
            }).Start();
        }
        public void listen(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);
            listener.Start();
            // in case the dissconnect button was pressed will not crush the program
            try { client = listener.AcceptTcpClient(); }
            catch { return; }
        }

        public void readFromServer()
        {
            string simulatorData;
            string[] planeData;
            char letter;
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // read every char until new line and will update that Lon and Lat changed
                while (IsProgramAlive)
                {
                    simulatorData = "";
                    while (true)
                    { 
                        // will not crush the program if the server is down 
                        try {
                            if ((letter = reader.ReadChar()) == '\n') 
                                break;
                        }
                        catch { return; }
                        simulatorData += letter;
                    }
                    if(simulatorData == "")
                    {
                        break;
                    }
                    planeData = simulatorData.Split(',');
                    Lon = float.Parse(planeData[0]);
                    Lat = float.Parse(planeData[1]);
                }
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void close()
        {
            IsProgramAlive = false;
            if (client != null)
            {
                client.Close();
            }
            if (listener != null)
            {
                listener.Stop();
            }

        }

    }
}
