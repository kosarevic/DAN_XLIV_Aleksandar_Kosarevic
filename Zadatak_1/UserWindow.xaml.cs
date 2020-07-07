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
        public static bool OrderMade = false;

        public UserWindow(User user)
        {
            InitializeComponent();
            DataContext = uvm;
            CurrentUser = user;
        }
        //Method initiated when user makes desired order.
        private void Btn_Ok(object sender, RoutedEventArgs e)
        {
            uvm.CreateOrder(CurrentUser);
            //Validation added if user inserts invalid values, so that he can correct invalid inputs.
            if (OrderMade)
            {
                OrderMade = true;
                LoginScreen login = new LoginScreen();
                login.Show();
                this.Close(); 
            }
        }
        //Method initiated if user cancels process of creating order.
        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }
    }
}
