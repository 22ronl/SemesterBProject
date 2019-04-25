using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FlightSimulator.Model.EventArgs
{
    public class VirtualJoystickEventArgs
    {
        double aileron;
        double elevator;
        public double Aileron {
            set
            {
                aileron = value;
                string input = "set controls/flight/aileron " + aileron.ToString("0.00");
                // send the command to the simulator throw command chanel 
                Commands.Instance.commandSimulator(input);
            }

        }
        public double Elevator {
            set
            {
                elevator = value;
                string input = "set controls/flight/elevator " + elevator.ToString("0.00");
                Commands.Instance.commandSimulator(input);
            }
        }
    }
}
