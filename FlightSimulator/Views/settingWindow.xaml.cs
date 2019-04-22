using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Model;
namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for settingWindow.xaml
    /// </summary>
    public partial class settingWindow : Window
    {
        public settingWindow()
        {
            InitializeComponent();
            DataContext = new SettingsWindowViewModel(ApplicationSettingsModel.Instance,this);
        }

      
    }
}
