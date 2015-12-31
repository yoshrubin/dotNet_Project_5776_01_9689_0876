using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum dishSize { medium, large, extraLarge }; 
    public enum dishHechser { rabbanut, mehadrin, lubavitch};
    public class Dish
    {
        //ctor
        public Dish(int dishID, string dishName, double dishPrice, dishSize dishSizeDish, dishHechser dishHechserDish)
        {
            this.dishID = dishID;
            this.dishName = dishName;
            this.dishPrice = dishPrice;
            this.dishSizeDish = dishSizeDish;
            this.dishHechserDish = dishHechserDish;
        }
        //properties
        public int dishID
        {
            get
            {
                return dishID;
            }
            private set
            {
                if (value > 99 && value <= 0) // don't want to have to many dishes available.
                    throw new Exception("dishID isn't within range of usable numbers.");
                else
                    dishID = value;
            }
        }
        public string dishName { get; private set; }
        public double dishPrice { get; private set; }
        public dishSize dishSizeDish { get; private set; }
        public dishHechser dishHechserDish { get; private set; }
        //func
        public override string ToString()
        {
            string temp = null;
            temp += dishID.ToString() + " dish Name:" + dishName + " dish Price:" + dishPrice.ToString();
            return temp;

        }
        public void randDishNum()
        {
            Random r = new Random();
            dishID = r.Next(1, 99);
        }
    }
}
