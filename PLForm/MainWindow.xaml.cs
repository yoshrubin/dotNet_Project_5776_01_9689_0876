using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;


namespace PLForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        IBL bl;
        public IBL Form1() // Clarify WHY I need to do it this way.
        {
            bl = FactoryBL.getIBL();
            return bl;
        }

        public MainWindow()
        {
            InitializeComponent();
            Form1();
            x.grantAccess();
            try
            {
                BranchBoxItems();
                DishBoxItems();
                OrderBoxItems();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region comboBox
        //Selects the branchName of all Branches
        void BranchBoxItems(string ToAdd = null)
        {
            if (ToAdd == null)
            {
                ComboBoxItem temp;
                foreach (Branch item in bl.sumBranch())
                {
                    temp = new ComboBoxItem();
                    temp.Content = item.branchName;
                    temp.Tag = item.branchID;
                    comboBoxBranch.Items.Add(temp);
                }
            }
            else
            {
                comboBoxBranch.Items.Add(ToAdd);
            }
        }

        void DishBoxItems(string ToAdd = null)
        {
            if (ToAdd == null)
            {
                ComboBoxItem temp;
                foreach (Dish item in bl.sumDish())
                {
                    temp = new ComboBoxItem();
                    temp.Content = item.dishName;
                    temp.Tag = item.dishID;
                    comboBoxBranch.Items.Add(temp);
                }
            }
            else
            {
                comboBoxBranch.Items.Add(ToAdd);
            }
        }

        //Only chooses Orders that fit to the branchID that we selected:
        void OrderBoxItems(string ToAdd = null)
        {
            if (ToAdd == null)
            {
                ComboBoxItem temp;
                int item2  = (int)((comboBoxBranch.SelectedItem as ComboBoxItem).Tag);
                bl.chooseOrder(item => item.orderBranch == item2);
                foreach (Order item in bl.chooseOrder(item => item.orderBranch == item2))
                {
                    temp = new ComboBoxItem();
                    temp.Content = item.orderID;
                    temp.Tag = item.orderID;
                    comboBoxBranch.Items.Add(temp);
                }
            }
            else
            {
                comboBoxBranch.Items.Add(ToAdd);
            }
        }
        #endregion

        #region click to open new Window
        Branch x = new Branch();
        private void branchWindowOpen(object sender, RoutedEventArgs e)
        {
            if (x.getAccess())
            {
                Window branchWindow = new branchWindow();
                branchWindow.Show();
            }
            else
            {
                new passwordWindow().Show();
            }
        }

        private void branchOrderOpen(object sender, RoutedEventArgs e)
        {
                new orderWindow().Show();
        }

        private void dishWindowOpen(object sender, RoutedEventArgs e)
        {
            if (x.getAccess())
                new dishWindow().Show();
            else
                new passwordWindow().Show();
        }

        private void buttonNewPassword_Click(object sender, RoutedEventArgs e)
        {
            new passwordWindow().Show();
        }
        #endregion

        private void addOrdDish(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxDish.SelectedItem == null || comboBoxOrder.SelectedItem == null) // if didn't pick on the specific box.
                {
                    int currentDish = (int)comboBoxDish.Tag;
                    int currentOrder = (int)comboBoxOrder.Tag;
                    Ordered_Dish currentOrdDish = new Ordered_Dish(currentOrder, currentDish);
                    bl.addOrdDish(currentOrdDish);
                }
                else
                    throw new Exception("Missing critical information.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
