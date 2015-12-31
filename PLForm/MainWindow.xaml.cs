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
    public partial class MainWindow : Window
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
            //comboBoxHechser.ItemsSource = Enum.GetValues(typeof(BE.branchHechser));
        }
       /* private void addDishClick(object sender, RoutedEventArgs e)
        {
            string dishname = this.inputText.Text;
        }
        */
    }
}
