using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FlightSimulator.Model;
namespace FlightSimulator.ViewModels
{
    class ManualVM : BaseNotify
    {
        
        private double rudder;
        private double throttle;
        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                NotifyPropertyChanged("rudder");
            }
        }
        
        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                Console.WriteLine(throttle);
                NotifyPropertyChanged("throttle");
            }
        }
        // private void OnSlider()
        //{
        //  string num = rudder.ToString;
        //  string input = "set controls/filght/rudder";
        //  Commands.Instance.sendCommand(input);
        //  }
    }
}
