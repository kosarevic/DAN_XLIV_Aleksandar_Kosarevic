using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    class Order
    {
        public int Id { get; set; }
        public DateTime OrderTimeStamp { get; set; }
        public User User { get; set; }
        public int Price { get; set; }
        public bool Approved { get; set; }

        public Order()
        {
        }

        public Order(int id, DateTime orderTimeStamp, User user, int price, bool approved)
        {
            Id = id;
            OrderTimeStamp = orderTimeStamp;
            User = user;
            Price = price;
            Approved = approved;
        }
    }
}
