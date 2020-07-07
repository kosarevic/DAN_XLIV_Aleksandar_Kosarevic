using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class responsible for generating data ment for UserWindow grid table.
    /// </summary>
    class UserViewModel : INotifyPropertyChanged
    {
        static readonly string ConnectionString = @"Data Source=(local);Initial Catalog=Zadatak_1;Integrated Security=True;";
        public ObservableCollection<UserWindowModel> UserWindowModels { get; set; }

        public UserViewModel()
        {
            FillList();
        }

        private UserWindowModel row;

        public UserWindowModel Row
        {
            get { return row; }
            set
            {
                if (row != value)
                {
                    row = value;
                    OnPropertyChanged("Row");

                    TotalPrice = 0;

                    foreach (var item in UserWindowModels)
                    {
                        TotalPrice += item.Amount * item.Meal.Price;
                    }
                }
            }
        }
        //Total price of all orders is being refreshed with each change to its value.
        private int totalPrice;

        public int TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged("TotalPrice");
                }
            }
        }
        /// <summary>
        /// Method fills the list dedicated to the coresponding window.
        /// </summary>
        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand query = new SqlCommand("select * from tblMeal", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (UserWindowModels == null)
                    UserWindowModels = new ObservableCollection<UserWindowModel>();

                foreach (DataRow row in dataTable.Rows)
                {
                    UserWindowModel u = new UserWindowModel
                    {
                        Meal = new Meal
                        {
                            Id = int.Parse(row[0].ToString()),
                            Name = row[1].ToString(),
                            Price = int.Parse(row[2].ToString())
                        },
                        Amount = 0
                    };
                    UserWindowModels.Add(u);
                }
            }
        }
        /// <summary>
        /// Method executed upon button press to confirm order creation.
        /// </summary>
        /// <param name="CurrentUser"></param>
        public void CreateOrder(User CurrentUser)
        {
            //Total price calculated for all orders chosen by the user.
            int total = 0;
            foreach (var row in UserWindowModels)
            {
                total += row.Meal.Price * row.Amount;
            }
            //Validation so that order cannot contain no values.
            if (total != 0)
            {
                //Order is inserted into database.
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand(@"insert into tblOrder values (@UserId, @Time, @Amount, @Approved);", conn);
                    cmd.Parameters.AddWithValue("@UserId", CurrentUser.Id);
                    cmd.Parameters.AddWithValue("@Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Amount", total);
                    cmd.Parameters.AddWithValue("@Approved", false);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    UserWindow.OrderMade = true;
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Order successfully created.", "Notification");
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Order amounts can't be 0, please try again.", "Notification");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
