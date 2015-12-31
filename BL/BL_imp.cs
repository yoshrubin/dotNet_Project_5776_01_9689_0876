using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace BL
{
    class BL_imp : DataSource, IBL
    {
        //fixed
        #region // Functions similar to IDAL
        //ADD
        #region // add functions revamped
        // Checks if the branch exists by the branchID, if it doesn't, it is add it to the branchlist.
        public void addBranch(Branch x)
        {
            bool available = true;
            if (x.branchID > 0)
            {
                foreach (Branch item in branchList)
                {
                    if (item.branchID == x.branchID) // search through if key exists in list.
                        available = false;
                }
            }
            if (x.branchID == 0)
            {
                do
                {
                    available = true;
                    x.randBranchNum(); // func in Branch.cs that gives a new random branchNum -> [1,1000]
                    foreach (Branch item in branchList)
                    {
                        if (item.branchID == x.branchID)
                            available = false;
                    }
                } while (!available); // The fact that the number exists in the list doesn't mean we arent going to add it, just give it a new random numer
            }
            if (available) // add to the list.
                branchList.Add(x);
            else
                throw new Exception("branchID already exists for a different branch.");
        }
        // Checks if the dish exists by the dishID, if it doesn't, it is added to the dishlist.
        public void addDish(Dish x)
        {
            bool available = true;
            if (x.dishID > 0)
            {
                foreach (Dish item in dishList)
                {
                    if (item.dishID == x.dishID) // search through if key exists in list.
                        available = false;
                }
            }
            if (x.dishID == 0)
            {
                do
                {
                    available = true;
                    x.randDishNum(); // func in Branch.cs that gives a new random dishNum -> [1,100]
                    foreach (Dish item in dishList)
                    {
                        if (item.dishID == x.dishID)
                            available = false;
                    }
                } while (!available); // The fact that the number exists in the list doesn't mean we arent going to add it, just give it a new random numer
            }
            if (available) // add to the list.
                dishList.Add(x);
            else
                throw new Exception("dishID already exists for a different dish.");
        }
        // Checks it the ordDish exists by the ordDishID, if it does, I just increase the amount - if not, I add it to the ordDishlist.
        public void addOrdDish(Ordered_Dish x)
        {
           foreach (Ordered_Dish item in ordDishList)
            {
                if (item.ordDishID == x.ordDishID && item.ordDishNum == x.ordDishNum) // The same order and dish num.
                {
                    //Since the order and dish already exists, all it means is that the person just ordered more of that dish.
                    Ordered_Dish tempOrdDish = new Ordered_Dish(item.ordDishID, item.ordDishNum, (item.ordDishAmount + x.ordDishAmount));
                    ordDishList.Remove(item);
                    ordDishList.Add(tempOrdDish);
                    return;
                }
            }
            // if either the orderID wasn't the same, or the dishID wasn't the same - means this dish was never ordered by this order before.
            ordDishList.Add(new Ordered_Dish(x.ordDishID, x.ordDishNum, x.ordDishAmount));
        }

        public void addOrder(Order x)
        {
            bool available = true;
            if (x.orderID > 0)
            {
                foreach (Order item in orderList)
                {
                    if (item.orderID == x.orderID) // search through if key exists in list.
                        available = false;
                }
            }
            if (x.orderID == 0)
            {
                do
                {
                    available = true;
                    x.randOrderNum(); // func in Branch.cs that gives a new random orderNum -> [1,10000]
                    foreach (Order item in orderList)
                    {
                        if (item.orderID == x.orderID)
                            available = false;
                    }
                } while (!available); // The fact that the number exists in the list doesn't mean we arent going to add it, just give it a new random numer
            }
            //Have to check that the order's hechser fits the branch:
            if ((int)x.orderHechserOrder < (int)getBranch(x.orderBranch).branchHechserBranch)
                throw new Exception("The Order's Hechser isn't high enough for the Branch");
            if (available) // add to the list.
                orderList.Add(x);
            else
                throw new Exception("orderID already exists for a different order.");
        }
        #endregion
        //DELETE
        #region // delete functions revamped
        public void deleteBranch(int x)
        {
            Branch tempB = getBranch(x);
            if (tempB != null)
                branchList.Remove(tempB);
            else
                throw new Exception("Can't delete Branch, for the Branch doesn't exist.");
        }

        public void deleteDish(int x)
        {
            Dish tempD = getDish(x);
            if (tempD != null)
                dishList.Remove(tempD);
            else
                throw new Exception("Can't delete dish, for the dish doesn't exist.");
        }

        public void deleteOrdDish(int x, int y)//Need to send the OrdDish ID and NUM.
        {
            Ordered_Dish tempOD = getOrdDish(x,y);
            if (tempOD == null)
                ordDishList.Remove(tempOD);
            else
                throw new Exception("Can't delete Ordered-Dish, for the Ordered-dish doesn't exist in the ordDishlist.");
        }

        public void deleteOrder(int x)
        {
            Order tempO = getOrder(x);
            if (tempO == null)
                orderList.Remove(tempO);
            else
                throw new Exception("Can't delete Order, for the Order doesn't exist in the orderlist.");
        }
        #endregion
        //UPDATE
        #region // update functions revamped
        public void updateBranch(Branch x)
        {
            Branch tempB = getBranch(x.branchID);
            if (tempB != null)
            {
                branchList.Remove(tempB);
                branchList.Add(x);
            }
            else
                throw new Exception("branch not found.");
        }

        public void updateDish(Dish x)
        {
            Dish tempD = getDish(x.dishID);
            if (tempD != null)
            {
                dishList.Remove(tempD);
                dishList.Add(x);
            }
            else
                throw new Exception("dish not found.");
        }

        public void updateOrdDish(Ordered_Dish x)
        {
            Ordered_Dish tempOD = getOrdDish(x.ordDishID, x.ordDishNum);
            if (tempOD != null)
            {
                ordDishList.Remove(tempOD);
                ordDishList.Add(x);
            }
            else
                throw new Exception("Ordered-Dish not found.");
        }

        public void updateOrder(Order x)
        {
            Order tempO = getOrder(x.orderID);
            if (tempO != null)
            {
                orderList.Remove(tempO);
                orderList.Add(x);
            }
            else
                throw new Exception("Order not found.");
        }
        #endregion
        //SUM
        #region // Sum functions
        public List<Branch> sumBranch()
        {
            return branchList;
        }

        public List<Dish> sumDish()
        {
            return dishList;
        }

        public List<Order> sumOrder()
        {
            return orderList;
        }
        #endregion
        //GETS - Search engines to find the class in it's respective list.
        #region // Get functions
        public Dish getDish(int dishID)
        {
            foreach (Dish item in dishList)
                if (item.dishID == dishID)
                    return item;
            return null;
        }

        public Order getOrder(int orderID)
        {
            foreach (Order item in orderList)
                if (item.orderID == orderID)
                    return item;
            return null;
        }

        public Ordered_Dish getOrdDish(int OrdDishID, int OrdDishNum)
        {
            foreach (Ordered_Dish item in ordDishList)
                if (item.ordDishID == OrdDishID && item.ordDishNum == OrdDishNum)
                    return item;
            return null;
        }

        public Branch getBranch(int branchID)
        {
            foreach (Branch item in branchList)
                if (item.branchID == branchID)
                    return item;
            return null;
        }
        #endregion
        #endregion

        public double SumMoneyDishes()
        {
            double sumMoney = 0;
            foreach (Ordered_Dish item in ordDishList)
            {
                double temp = findDishPrice(item.ordDishID); // sending to func we created to find and return dish price.
                for (int i = 0; i < item.ordDishNum; i++)
                {
                    sumMoney += temp;
                }
            }
            return sumMoney;
        }

        public bool tooMuchMonies(double x)
        {
            if (x > 2000)
                return true; // true that the order is too expensive
            else
                return false;
        }

        public bool tooLittleHoly(orderHechser x, dishHechser y)
        {
            if ((int)x > (int)y)//dish is less holy then order
                return true;//meaning dish isnt holy enough
            else
                return false;//meaning dish is good
        }

        public List<Order> chooseOrder(Func<Order, bool> predicate = null)
        {
            var queryAllOrders = from orders in orderList
                                 where (predicate(orders))
                                 select orders;
            return (List<Order>)queryAllOrders;
        }

        #region // Not Sure how to Implement w/ Grouping
        public double moniesOrder()
        {
            throw new NotImplementedException();
        }

        public double moniesTime()
        {
            throw new NotImplementedException();
        }

        public double moniesPlace()
        {
            throw new NotImplementedException();
        }

        public bool tooYoung(Order x)
        {
            if (x.orderAge < 18)
                return true;
            else
                return false;
        }
        #endregion

        //EXTRA
        public double findDishPrice(int x)
        {
            foreach (Dish item in dishList)
            {
                if (x == item.dishID)
                    return item.dishPrice;
            }
            return 0;
        }

        public Dish mostOrderedDish()
        {
            int counter = 0;
            Dish bestDish = null;
            foreach (Ordered_Dish item in ordDishList)
            {
                if (item.ordDishNum > counter)
                {
                    counter = item.ordDishNum;
                    bestDish = getDish(item.ordDishID);
                }
            }
            return bestDish;
        }//Finds the most ordered dish per Order

        public List<Dish> holierThanThou()
        {
            var queryHolyDish = from dish in dishList
                                where (int)dish.dishHechserDish >= 2 //he'll prefer a better hescher, but will take high.
                                select dish;
            return (List<Dish>)queryHolyDish;
        } //offers the dishes available for the holy.

        public bool tooLittleMoniesDelivery(double x)
        {
            if (x < 20)
                return true; // true that the order is too low for DELIVARY
            else
                return false;
        } // checks if order is high enough for delivery.

        public List<Dish> americanMenu()
        {
            var queryAmericanMenu = from dish in dishList
                                    where (int)dish.dishSizeDish >= 2 // he'll prefer a larger dish, but will take large.
                                    select dish;
            return (List<Dish>)queryAmericanMenu;
        }//Finds the menu for the Americans.

        public string managerOfTheMonth()
        {
            return branchSuccessMonth().branchManager;
        }//Finds the manager whose branch made the most money

        public Branch branchSuccessMonth()
        {
            double mostMoney = 0; // Highest amount of money for the Branch
            double sumOrdDishes = 0; // Sum of money from all the ordered dishes of a branch
            Branch bestBranch = null;
            foreach (Branch branchitem in branchList)
            {
                foreach (Order item in branchitem.listOrderforBranch)
                {
                    if (item.orderTime.Month == DateTime.Now.Month) // Only consider the orders made within the Month.
                        sumOrdDishes += SumMoneyDishes();
                }
                if (sumOrdDishes > mostMoney)
                {
                    mostMoney = sumOrdDishes;
                    bestBranch = branchitem; // found the bestBranch as of now
                }
                sumOrdDishes = 0;
            }
            return bestBranch;
        }

        public List<Branch> rankBranchPerMonth()
        {
            throw new NotImplementedException();
        }

        /*public List<Branch> rankBranchPerMonth(List<Branch> branchList)
        {
            var queryWhatevra = from item in branchList
                                from item2 in item.listOrderforBranch
                                where (item2.orderTime.Month == DateTime.Now.Month)
                                orderby SumMoneyDishes(item2.listofOrderedDishes) descending
                                select item;
            return queryWhatevra.ToList<Branch>();

        }*/

        //FIND OUT HOW TO USE LAMBDA?!?!?!??!?!?!?!?!?!?!?
    }
}
