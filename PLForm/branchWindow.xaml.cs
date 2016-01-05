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

            dataGridBranch.ItemsSource = bl.sumBranch();
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
                dataGridBranch.Items.Refresh();
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

                    if (textBoxName.Text != "")
                        tempBranch.branchName = textBoxName.Text;

                    if (textBoxAddress.Text != "")
                        tempBranch.branchAddress = textBoxAddress.Text;

                    if (textBoxPhoneNum.Text != "")
                    { 
                        if (textBoxPhoneNum.Text.Length != 10)
                            throw new Exception("Phone number not accurate.");
                        else
                            tempBranch.branchPhoneNum= int.Parse(textBoxPhoneNum.Text);
                    }

                    if (textBoxManager.Text != "")
                        tempBranch.branchManager = textBoxManager.Text;

                    if (textBoxEmployees.Text != "")
                    {
                        if (int.Parse(textBoxEmployees.Text) < 1)
                            throw new Exception("Innacurate Employees.");
                        else
                            tempBranch.branchEmployee = int.Parse(textBoxEmployees.Text);
                    }

                    if (textBoxDelivery.Text != "")
                    {
                        if (int.Parse(textBoxDelivery.Text) < 0)
                            throw new Exception("Innacurate Delivery.");
                        else
                            tempBranch.branchDeliveryFree = int.Parse(textBoxDelivery.Text);
                    }

                    //not change the hechser:
                    if ((int)comboBoxHechser.SelectedItem >= 0)
                        tempBranch.branchHechserBranch = (branchHechser)comboBoxHechser.SelectedItem;

                    bl.updateBranch(tempBranch);
                    dataGridBranch.Items.Refresh();
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
                dataGridBranch.Items.Refresh();
                throw new Exception("Branch with id: " + tempBranch.branchID + " has been deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
