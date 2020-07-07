using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class EmployeViewModel : INotifyPropertyChanged
    {
        static readonly string ConnectionString = @"Data Source=(local);Initial Catalog=Zadatak_1;Integrated Security=True;";
        public ObservableCollection<Order> Orders { get; set; }

        private Order order;

        public Order Order
        {
            get { return order; }
            set
            {
                if (order != value)
                {
                    order = value;
                    OnPropertyChanged("Order");
                }
            }
        }

        public EmployeViewModel()
        {
            FillList();
        }

        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand query = new SqlCommand("select * from tblOrder", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Orders == null)
                    Orders = new ObservableCollection<Order>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Order o = new Order
                    {
                        Id = int.Parse(row[0].ToString()),
                        User = new User(int.Parse(row[1].ToString())),
                        OrderTimeStamp = (DateTime)row[2],
                        Price = int.Parse(row[3].ToString()),
                        Approved = (bool)row[4]
                    };
                    Orders.Add(o);
                }
            }
        }

        public void Approve()
        {
            var con = new SqlConnection(ConnectionString);
            con.Open();
            var cmd = new SqlCommand("update tblOrder set Approved=@Approved where OrderID=@OrderID;", con);
            cmd.Parameters.AddWithValue("@Approved", true);
            cmd.Parameters.AddWithValue("@OrderID", Order.Id);
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            Orders.Clear();
            FillList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
