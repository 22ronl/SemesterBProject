using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using FlightSimulator.Model;
namespace FlightSimulator.ViewModels
{
    class MainWindowVM
    {
        public ICommand disconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                return disconnectCommand ?? (disconnectCommand = new CommandHandler(() => disconnect()));
            }
        }
        private void disconnect()
        {
            new Thread(delegate ()
            {
                Commands.Instance.close();
                InfoServer.Instance.close();
            }).Start();
        }

    }
}
