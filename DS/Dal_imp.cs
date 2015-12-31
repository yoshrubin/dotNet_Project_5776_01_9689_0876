using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using DAL;

namespace DS
{
    class Dal_imp : DataSource, IDAL
    {
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
            Ordered_Dish tempOD = getOrdDish(x, y);
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
        // SUM
        #region sum functions
        //SUM
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
    }
}
