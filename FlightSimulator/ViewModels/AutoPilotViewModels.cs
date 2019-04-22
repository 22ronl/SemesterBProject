using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
namespace FlightSimulator.ViewModels
{  
    class AutoPilotViewModels : BaseNotify
    {
        
        public string inputString ="";
        public string changeColor
        {
            get
            {
                if (inputString == "")
                {
                    return "white";
                }
                else
                {
                    return "Red";
                }
                
            }
        }
        public string userInput
        {
            get
            {
                return inputString;
            }
            set
            {
                inputString = value;
                NotifyPropertyChanged("inputString");
                NotifyPropertyChanged("changeColor");
            }
        }

        public ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => OnOk()));
            }
        }
        private void OnOk()
        {
            Commands.Instance.sendCommand(userInput);
        }

        private ICommand clearCommand = null;
        public ICommand Clear
        {
            get
            {
                if(clearCommand == null)
                {
                   return clearCommand = new CommandHandler(() => clearOnClick());
                } else
                {
                    return clearCommand;
                }
            }
        }
        
        private void clearOnClick()
        {
            userInput = "";
            NotifyPropertyChanged("userInput");
        }
    }
}
