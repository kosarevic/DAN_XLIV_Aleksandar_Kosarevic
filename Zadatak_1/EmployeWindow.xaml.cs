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
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for EmployeWindow.xaml
    /// </summary>
    public partial class EmployeWindow : Window
    {
        EmployeViewModel evm = new EmployeViewModel();

        public EmployeWindow()
        {
            InitializeComponent();
            DataContext = evm;
        }

        private void Approve_Btn(object sender, RoutedEventArgs e)
        {
            evm.Approve();
        }

        private void Delete_Btn(object sender, RoutedEventArgs e)
        {
            evm.DeleteOrder();
        }
    }
}
