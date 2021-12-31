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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        
        MainWindow current;
        public static List<Account> accounts = new();
        public static void addAccount(Account ac)
        {
            accounts.Add(ac);
        }
        public LoginScreen()
        {
            InitializeComponent();
            accounts.Add(new Account("Avi Gold", "123"));
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in accounts)
            {
                if (TextUser.Text == item.UserName && TextPass.Password == item.password)
                {
                    current = new MainWindow(item.UserName);
                    return;
                }
            }
            if ((TextUser.Text == "Admin" || TextUser.Text == "admin")&&TextPass.Password == "123456" )
            {
                current = new MainWindow("");
                current.Show();
            }
            MessageBox.Show("Something's wrong! If you're new here, create an account");
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            new NewUser(accounts).Show();
        }
    }
}
