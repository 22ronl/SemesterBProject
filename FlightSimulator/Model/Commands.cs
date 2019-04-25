using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace FlightSimulator.Model
{
   
    class Commands
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;
        private bool isProgramAlive = true;
        private static Commands m_Instance = null;
        private static Mutex mutex = new Mutex();
        private Thread connection;
        public static Commands Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Commands();
                }
                return m_Instance;
            }
        }
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

        public void openClient()
        {
            connection = new Thread(delegate ()
            {
                connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
            });
            connection.Start();
        }
        public void connect(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            while (true)
            {
                if(!IsProgramAlive)
                {
                    return;
                }
                if(client.Connected)
                {
                    break;
                }
                try { client.Connect(ep); }
                catch (Exception) { }
            }
            isConnected = true;
            stream = client.GetStream();

        }
        public void commandSimulator(string command)
        {
            if (!isConnected)
            {
                return;
            }
            string binaryCommand = command + "\r\n";
            byte[] bufferRoWrite = Encoding.ASCII.GetBytes(binaryCommand);
            stream.Write(bufferRoWrite, 0, bufferRoWrite.Length);
        }
              
  
        public void sendCommand(string userCommands)
        {
            if (!isConnected)
            {
                return;
            }
            string[] commands = userCommands.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach(string command in commands)
            {
                commandSimulator(command);
                Thread.Sleep(2000);
            }
        }

        public void close()
        {
            if(connection.IsAlive)
            {
                IsProgramAlive = false;
            }
            client.Close();
        }
    }
}
