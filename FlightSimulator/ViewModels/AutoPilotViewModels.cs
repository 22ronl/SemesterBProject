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
        public bool whiteBackGround = true;
        public string inputString ="";
        public string changeColor
        {
            get
            {
                if (whiteBackGround)
                {
                    return "white";
                }
                else
                {
                    return "Pink";
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
                if(inputString == "")
                {
                    whiteBackGround = true;
                } else
                {
                    whiteBackGround = false;
                } 
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
            whiteBackGround = true;
            NotifyPropertyChanged("changeColor");
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
