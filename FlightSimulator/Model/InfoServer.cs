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
        private double lon;
        private double lat;
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
        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }
        public double Lat
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
                readFromServer();
                isSereverOpen = true;
            }).Start();
        }
        public void listen(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for client connections...");
            client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");
            stream = client.GetStream();
            reader = new BinaryReader(stream);
        }

        public void readFromServer()
        {
            string simulatorData;
            string[] planeData;
          //  while (isSereverOpen)
            //{
                simulatorData = reader.ReadString();
                planeData = simulatorData.Split(',');
                Lon = Double.Parse(planeData[0]);
                Lat = Double.Parse(planeData[1]); 
            //}
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
