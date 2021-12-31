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

namespace PrL
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser(List<Account> accounts)
        {
            InitializeComponent();
        }

        private void SubmitNewUser_Click(object sender, RoutedEventArgs e)
        {
            var ac = new Account(NewTextUser.Text, (string)NewTextPass.Password);
            try
            {
                if (NewTextPass.Password == NewTextPassAgain.Password)
                {
                    LoginScreen.addAccount(ac);
                    MessageBox.Show("welcome!");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
