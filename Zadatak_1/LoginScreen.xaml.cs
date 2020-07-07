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
                //User is extracted from the database matching inserted paramaters Username and Password.
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
                //If username is as value below, Employe window is engaged.
                if (user.Username == "Zaposleni")
                {
                    EmployeWindow dashboard = new EmployeWindow();
                    dashboard.Show();
                    this.Close();
                }
                //If username is as value below, User window is engaged.
                else if (user.Password == "Gost")
                {
                    //Validation if user has pending order to be approved.
                    if (!OrderValidation.UserHasOrder(user))
                    {
                        UserWindow dashboard = new UserWindow(user);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        //If user has order with pending approval, application exits to the login screen.
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
