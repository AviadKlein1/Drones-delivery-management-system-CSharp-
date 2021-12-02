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

namespace PL
{
    /// <summary>
    /// Interaction logic for dronesListWindow1.xaml
    /// </summary>
    public partial class dronesListWindow : Window
    {
        IBL.BO.BL dlw_bl;
        public dronesListWindow(IBL.BO.BL bl)
        {
            static bool AllDrones(IBL.BO.DroneToList d) { return true; }
            System.Predicate<IBL.BO.DroneToList> allDrones = AllDrones;
            dlw_bl = bl;
            dlw_bl.GetDronesList(allDrones);
        }
    }
}
