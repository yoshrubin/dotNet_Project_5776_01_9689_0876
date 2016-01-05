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
    /// Interaction logic for dishWindow.xaml
    /// </summary>
    public partial class dishWindow : Window
    {
        BL.IBL bl;
        Dish currentDish = new Dish();

        public dishWindow()
        {
            InitializeComponent();

            bl = BL.FactoryBL.getIBL();
            comboBoxHechser.ItemsSource = Enum.GetValues(typeof(BE.dishHechser));
            comboBoxSize.ItemsSource = Enum.GetValues(typeof(BE.dishSize));
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxID.Text);//add id from txtbox
                if (id < 1)
                    throw new Exception("Inaccurate ID");
                string name = textBoxName.Text;//add name from txtbox
                if (name == "")
                    throw new Exception("Inaccurate Name");
                int price = int.Parse(textBoxPrice.Text);//add price from txtbox
                if (price < 1)
                    throw new Exception("Inaccurate Price");
                dishSize size = (dishSize)comboBoxSize.SelectedItem;//add size from combobox
                if ((int)size < 0)
                    throw new Exception("Lacking Size.");
                dishHechser hechser = (dishHechser)comboBoxHechser.SelectedItem;//add hechsher from combobox
                if ((int)hechser < 0)
                    throw new Exception("Lacking Hechser.");
                Dish currDish = new Dish(id, name, price, size, hechser);//adding dish
                //clear all fields
                textBoxName.Text = "";
                textBoxID.Text = "";
                textBoxPrice.Text = "";
                comboBoxSize.SelectedItem = null;
                comboBoxHechser.SelectedItem = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Dish tempDish = bl.getDish(int.Parse(textBoxID.Text));

                if (tempDish == null) // if the id doesn't exists within the branchlist.
                    throw new Exception("Dish with given id not found.");

                else
                {
                    //If the textBox == "", meaning it's empty, it wont update that info.

                    string name;
                    if (textBoxName.Text == "")
                        name = tempDish.dishName;
                    else
                        name = textBoxName.Text;

                    int price;
                    if (textBoxPrice.Text == "")
                        price = (int)tempDish.dishPrice;
                    else
                        name = textBoxName.Text;

                    dishHechser hechser;
                    if ((dishHechser)comboBoxHechser.SelectedItem < 0)
                        hechser = tempDish.dishHechserDish;
                    else
                        hechser = (dishHechser)comboBoxHechser.SelectedItem;

                    dishSize size;
                    if ((dishSize)comboBoxSize.SelectedItem < 0)
                        size = tempDish.dishSizeDish;
                    else
                        size = (dishSize)comboBoxSize.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Dish tempDish = bl.getDish(int.Parse(textBoxID.Text));

                if (tempDish == null) // if the id doesn't exists within the Dishlist.
                    throw new Exception("Dish with given id not found.");
                bl.deleteDish(tempDish.dishID);
                throw new Exception("Dish with id: " + tempDish.dishID + " has been deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
