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
using BL;
using BE;

namespace PLForm
{
    /// <summary>
    /// Interaction logic for passwordWindow.xaml
    /// </summary>
    public partial class passwordWindow : Window
    {
        public passwordWindow()
        {
            InitializeComponent();
        }
        IBL bl = FactoryBL.getIBL();
        Branch x = new Branch();
        private void buttonPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (x.passwordCorrect(this.passwordBox.Password))
                {
                    x.grantAccess();
                    this.Close();
                }
                else
                    throw new Exception("Password Incorrect.");
            }catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNewPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (x.insertNewPassword(this.passwordBox.Password, this.passwordBoxNewPassword.Password))
                {
                    throw new Exception("Password Changed");
                }
                else
                    throw new Exception("Password not changed.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
