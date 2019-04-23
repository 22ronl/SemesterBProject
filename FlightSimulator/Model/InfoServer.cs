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
        private bool isSereverOpen;
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream stream;
        private BinaryReader reader;
        private Nullable<float> lon =null;
        private Nullable<float> lat=null;
        private static InfoServer m_Instance = null;
        public event PropertyChangedEventHandler PropertyChanged;
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

        NetworkStream GetNetworkStream()
        {
            return stream;
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

        public void openServer()
        {
            new Thread(delegate ()
            {
                listen(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
                //System.Windows.Forms.MessageBox.Show("connected");
                isSereverOpen = true;
                readFromServer();
                
            }).Start();
        }
        public void listen(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);
            listener.Start();
            client = listener.AcceptTcpClient();
        }

        public void readFromServer()
        {
            string simulatorData;
            string[] planeData;
            char letter;
            //NetworkStream networkStream;
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (isSereverOpen)
                {
                    //networkStream = GetNetworkStream();
                    //stream = client.GetStream();
                    //BinaryReader reader = new BinaryReader(networkStream);
                    simulatorData = "";
                    while ((letter = reader.ReadChar()) != '\n')
                    {
                        simulatorData += letter;
                    }
                    if(simulatorData == "")
                    {
                        break;
                    }
                    planeData = simulatorData.Split(',');
                    Lon = float.Parse(planeData[0]);
                    Lat = float.Parse(planeData[1]);
                    Console.WriteLine(simulatorData);
                    Thread.Sleep(500);
                }
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
