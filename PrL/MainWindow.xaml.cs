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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using DalApi;


namespace PrL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     public partial class MainWindow : Window
    {

        internal IBl bl;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bl = (BlApi.BO.BL)BlFactory.GetBl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            //this.Closing += new System.ComponentModel.CancelEventHandler(MyWindow_Closing);
        }

        //void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    e.Cancel = true;
        //}
        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            new dronesList(bl).Show();
        }
    }
}
