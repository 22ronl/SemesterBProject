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
                sliderMove("set controls/flight/rudder", rudder);
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
                //Console.WriteLine(throttle);
                sliderMove("set controls/engines/current-engine/throttle", throttle);
                NotifyPropertyChanged("throttle");
            }
        }
         private void sliderMove(string slider ,double value)
        {
          string val = value.ToString("0.00");
          string input = slider + " " + val;
          //Console.WriteLine(input);
          Commands.Instance.commandSimulator(input);
        }
    }
}
