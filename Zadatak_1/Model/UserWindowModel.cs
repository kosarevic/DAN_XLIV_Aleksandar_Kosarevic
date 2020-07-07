using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    /// <summary>
    /// Class responsible for generating objects dedicated to UserWindow data grid.
    /// </summary>
    class UserWindowModel
    {

        public Meal Meal { get; set; }
        public int Amount { get; set; }

        public UserWindowModel()
        {
        }

        public UserWindowModel(Meal meal, int amount)
        {
            Meal = meal;
            Amount = amount;
        }
    }
}
