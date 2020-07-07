using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Zadatak_1.Validation;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(local); Initial Catalog=Zadatak_1; Integrated Security=True;");
            try
            {
                SqlCommand query = new SqlCommand("SELECT * FROM tblUser WHERE Username=@Username AND Password=@Password", sqlCon);
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@Username", txtUsername.Text);
                query.Parameters.AddWithValue("@Password", txtPassword.Password);
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                User user = new User();

                foreach (DataRow row in dataTable.Rows)
                {
                    user = new User
                    {
                        Id = int.Parse(row[0].ToString()),
                        Username = row[1].ToString(),
                        Password = row[2].ToString()
                    };
                }

                if (user.Username == "Zaposleni")
                {
                    EmployeWindow dashboard = new EmployeWindow();
                    dashboard.Show();
                    this.Close();
                }
                else if (user.Password == "Gost")
                {
                    if (!OrderValidation.UserHasOrder(user))
                    {
                        UserWindow dashboard = new UserWindow(user);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("You already have order with pending approval, please try again latter.", "Notification");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
