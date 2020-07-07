using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.Validation
{
    public static class OrderValidation
    {

        public static bool UserHasOrder(User u)
        {
            List<Order> AllOrders = new List<Order>();

            string ConnectionString = @"Data Source=(local);Initial Catalog=Zadatak_1;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand query = new SqlCommand("select * from tblOrder", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (AllOrders == null)
                    AllOrders = new List<Order>();

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
                    AllOrders.Add(o);
                }
            }

            foreach (Order o in AllOrders)
            {
                if(u.Id == o.User.Id && o.Approved == false)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
