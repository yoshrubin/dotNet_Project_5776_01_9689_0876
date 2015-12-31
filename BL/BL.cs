using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region similar to IDAL

        #region dish functions
        //DISH
        void addDish(Dish x);
        void deleteDish(int x);
        void updateDish(Dish x);
        Dish getDish(int dishID);
        #endregion

        #region order function
        //ORDER
        void addOrder(Order x);
        void deleteOrder(int x);
        void updateOrder(Order x);
        Order getOrder(int orderID);
        #endregion

        #region ordered-dish function
        //Ordered-Dish
        void addOrdDish(Ordered_Dish x);
        void updateOrdDish(Ordered_Dish x);
        void deleteOrdDish(int x, int y);
        Ordered_Dish getOrdDish(int OrdDishID, int OrdDishNum);
        #endregion

        #region branch function
        //BRANCH
        void addBranch(Branch x);
        void deleteBranch(int x);
        void updateBranch(Branch x);
        Branch getBranch(int branchID);
        #endregion

        #region sum functions
        List<Order> sumOrder();
        List<Dish> sumDish();
        List<Branch> sumBranch();
        #endregion


        #endregion

        //EXTRA
        double SumMoneyDishes();
        Dish mostOrderedDish();
        bool tooLittleMoniesDelivery(double x);
        List<Dish> holierThanThou();
        bool tooMuchMonies(double x);
        bool tooLittleHoly(orderHechser x, dishHechser y);
        List<Order> chooseOrder(Func<Order, bool> predicate = null);
        double moniesTime();
        double moniesPlace();
        bool tooYoung(Order x); // Need to get age.
        List<Dish> americanMenu();
        string managerOfTheMonth();
        Branch branchSuccessMonth();
        List<Branch> rankBranchPerMonth();
    }
}
