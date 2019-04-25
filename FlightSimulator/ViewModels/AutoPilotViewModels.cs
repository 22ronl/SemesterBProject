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
                // in case there is nothing written the background should be white
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
            // press on ok change the background to white and sends commnad to the simualtor 
            whiteBackGround = true;
            NotifyPropertyChanged("changeColor");
            Commands.Instance.sendCommand(userInput);
        }

        private ICommand clearCommand;
        public ICommand Clear
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => clearOnClick()));
            }
        }
        
        private void clearOnClick()
        {
            userInput = "";
            NotifyPropertyChanged("userInput");
        }
    }
}
