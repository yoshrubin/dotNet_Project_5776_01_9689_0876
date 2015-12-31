using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public enum orderHechser { rabbanut, mehadrin, lubavitch }; // Check if it has to be in Class?
    public class Order
    {
        //ctor
        public Order(int orderID, DateTime orderTime, int orderBranch, orderHechser orderHechserOrder, int orderStaff, string orderCustomer, string orderCustAddress, string orderCustLocation, int orderCustCC, int orderAge)
        {
            this.orderID = orderID;
            this.orderTime = orderTime;
            this.orderBranch = orderBranch;
            this.orderHechserOrder = orderHechserOrder;
            this.orderStaff = orderStaff;
            this.orderCustAddress = orderCustAddress;
            this.orderCustLocation = orderCustLocation;
            this.orderCustCC = orderCustCC;
            this.orderAge = orderAge;
        }
        //properties
        public int orderID
        {
            get
            {
                return orderID;
            }
            private set
            {
                if (value > 10000 && value <= 0 )
                    throw new Exception("orderID isn't within range of usable numbers.");
                else
                    orderID = value;
            }
        }
        // The exception will be checked when try to add it to the orderList.
        public int orderBranch { get; private set; } // The branch that the Order is being placed in.
        public DateTime orderTime { get; private set; }
        public orderHechser orderHechserOrder { get; private set; }
        public int orderStaff { get; private set; }
        public string orderCustomer { get; private set; }
        public string orderCustAddress { get; private set; } //Where he is from
        public string orderCustLocation { get; private set; } // Where he wants to be sent to
        public int orderCustCC
        {
            get
            {
                return orderCustCC;
            }
            private set
            {
                if (value.ToString().Length < 12 && value.ToString().Length > 12) // CC have to have 12 digits.
                    throw new Exception("Credit Card Number not valid.");
                else
                    orderCustCC = value;
            }
        }
        public int orderAge { get; private set; }
        //func
        public override string ToString()
        {
            string temp = null;
            temp += orderID.ToString() + " order time: " + orderTime.ToString() + " order branch: " + orderBranch.ToString() + " order customer: " + orderCustomer + " order customer's current location: " + orderCustLocation;
            return temp;
        }
        public void randOrderNum()
        {
            Random r = new Random();
            orderID = r.Next(1, 10000);
        }
    }
}
