using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Views;
namespace FlightSimulator.ViewModels
   
{
    class PlaneBoardVM
    {
        private ICommand settingsCommand;
        private bool isConnected = false;
       
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnClick()));
            }
        }

        private void OnClick()
        {
            // show the settings window
            settingWindow settings = new settingWindow();
            settings.ShowDialog();
        }
        public ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnect()));
            }
        }
        private void OnConnect()
        {
            // will connect to servers only once (first press)
            if(!isConnected)
            {
                InfoServer.Instance.openServer();
                Commands.Instance.openClient();
                isConnected = true;
            }

        }

    }
}
