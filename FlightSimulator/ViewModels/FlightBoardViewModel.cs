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
            infoServer = InfoServer.Instance;
            infoServer.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged(e.PropertyName);
                };
        }
        public Nullable<float> Lon
        {
            get { return infoServer.Lon; }
        }

        public Nullable<float> Lat
        {
            get { return infoServer.Lat;}
        }
    }
}
