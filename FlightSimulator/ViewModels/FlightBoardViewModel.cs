using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.ComponentModel;
namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private InfoServer infoServer;
        public FlightBoardViewModel() {
            this.infoServer = InfoServer.Instance;
            infoServer.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged(e.PropertyName);
                };
        }
        public double Lon
        {
            get { return infoServer.Lon; }
        }

        public double Lat
        {
            get { return infoServer.Lat;}
        }
    }
}
