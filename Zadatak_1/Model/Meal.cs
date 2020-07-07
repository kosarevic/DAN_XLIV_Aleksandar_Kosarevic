using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Meal()
        {
        }

        public Meal(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
