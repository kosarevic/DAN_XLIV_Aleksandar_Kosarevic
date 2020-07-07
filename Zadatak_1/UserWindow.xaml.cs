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
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        UserViewModel uvm = new UserViewModel();
        public User CurrentUser = new User();

        public UserWindow(User user)
        {
            InitializeComponent();
            DataContext = uvm;
            CurrentUser = user;
        }

        private void Btn_Ok(object sender, RoutedEventArgs e)
        {
            uvm.CreateOrder(CurrentUser);
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Order successfully created.", "Notification");
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }
    }
}
