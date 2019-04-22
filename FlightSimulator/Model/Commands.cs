using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
namespace FlightSimulator.Model
{
   
    class Commands
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;
        private static Commands m_Instance = null;
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
        public void openClient()
        {
            new Thread(delegate ()
            {
                connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
            }).Start();
            
        }
        public void connect(string ip, int port)
        {
           // new Thread(delegate (){
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                client = new TcpClient();
                //client.Connect(ep);
                //
                
                while (!client.Connected)
                {
                    try { client.Connect(ep); }
                    catch (Exception) { }
                }
            isConnected = true;
            stream = client.GetStream();
            //}).Start();

        }
        public void commandSimulator(string command)
        {
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
                //using (BinaryWriter writer = new BinaryWriter(stream))
                // {
                commandSimulator(command);
               // }
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
