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
using System.Windows.Shapes;
using BE;

namespace PLForm
{
    /// <summary>
    /// Interaction logic for branchWindow.xaml
    /// </summary>
    public partial class branchWindow : Window
    {
        BL.IBL bl;
        BE.Branch currentBranch = new BE.Branch();

        public branchWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.getIBL();

            comboBoxHechser.ItemsSource = Enum.GetValues(typeof(BE.branchHechser));
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                if (txtBranchID.Text == "")
                    id = 0;
                else
                    id = int.Parse(txtBranchID.Text);
                if (id > 1000)
                    throw new Exception("Inaccurate id number.");
                string name = textBoxName.Text;
                if (name == "")
                    throw new Exception("Lacking Name.");
                string address = textBoxAddress.Text;
                if (address == "")
                    throw new Exception("Lacking Address.");
                long phoneNum = long.Parse(textBoxPhoneNum.Text);
                if (textBoxPhoneNum.Text.Length != 10)
                    throw new Exception("Inaccurate PhoneNum.");
                string manager = textBoxManager.Text;
                if (manager == "")
                    throw new Exception("Lacking Manager.");
                int employee = int.Parse(textBoxEmployees.Text);
                if (employee < 1)
                    throw new Exception("Inaccurate Employees");
                int deliveryFree = int.Parse(textBoxDelivery.Text);
                if (deliveryFree < 0)
                    throw new Exception("Inaccurate Delivery.");
                branchHechser hechser = (branchHechser)comboBoxHechser.SelectedItem;
                if ((int)hechser < 0)
                    throw new Exception("Lacking Hechser.");
                BE.Branch currentBranch = new BE.Branch(name, address, phoneNum, manager, employee, deliveryFree, hechser, id);
                bl.addBranch(currentBranch);
                MessageBox.Show("You have added the Branch with the id: " + currentBranch.branchID.ToString());
                //Clear the form:
                txtBranchID.Text = "";
                textBoxAddress.Text = "";
                textBoxDelivery.Text = "";
                textBoxEmployees.Text = "";
                textBoxManager.Text = "";
                textBoxName.Text = "";
                textBoxPhoneNum.Text = "";
                comboBoxHechser.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Branch tempBranch = bl.getBranch(int.Parse(txtBranchID.Text));

                if (tempBranch == null) // if the id doesn't exists within the branchlist.
                    throw new Exception("Dish with given id not found.");

                else
                {
                    //If the textBox == "", meaning it's empty, it wont update that info.

                    string name;
                    if (textBoxName.Text == "")
                        name = tempBranch.branchName;
                    else
                        name = textBoxName.Text;

                    string address;
                    if (textBoxAddress.Text == "")
                        address = tempBranch.branchAddress;
                    else
                        address = textBoxAddress.Text;

                    long phoneNum;
                    if (textBoxPhoneNum.Text == "")
                        phoneNum = tempBranch.branchPhoneNum;
                    else
                    {
                        if (textBoxPhoneNum.Text.Length != 10)
                            throw new Exception("Phone number not accurate.");
                        else
                            phoneNum = int.Parse(textBoxPhoneNum.Text);
                    }

                    string manager;
                    if (textBoxManager.Text == "")
                        manager = tempBranch.branchManager;
                    else
                        manager = textBoxManager.Text;
                  
                    int employee;
                    if (textBoxEmployees.Text == "")
                        employee = tempBranch.branchEmployee;
                    else
                    {
                        if (int.Parse(textBoxEmployees.Text) < 1)
                            throw new Exception("Innacurate Employees.");
                        else
                            employee = int.Parse(textBoxEmployees.Text);
                    }

                    int deliveryFree;
                    if (textBoxDelivery.Text == "")
                        deliveryFree = tempBranch.branchDeliveryFree;
                    else
                    {
                        if (int.Parse(textBoxDelivery.Text) < 0)
                            throw new Exception("Innacurate Delivery.");
                        else
                            deliveryFree = int.Parse(textBoxDelivery.Text);
                    }

                    branchHechser hechser;
                    if ((branchHechser)comboBoxHechser.SelectedItem < 0)
                        hechser = tempBranch.branchHechserBranch;
                    else
                        hechser = (branchHechser)comboBoxHechser.SelectedItem;

                    bl.updateBranch(tempBranch);
                    MessageBox.Show("Branch: " + tempBranch.branchID.ToString() + " has been updated.");          
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Branch tempBranch = bl.getBranch(int.Parse(txtBranchID.Text));

                if (tempBranch == null) // if the id doesn't exists within the branchlist.
                    throw new Exception("Dish with given id not found.");
                bl.deleteBranch(tempBranch.branchID);
                throw new Exception("Branch with id: " + tempBranch.branchID + " has been deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = sender as DataGrid;
        }
    }
}
