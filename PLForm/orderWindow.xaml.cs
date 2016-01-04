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

namespace PLForm
{
    /// <summary>
    /// Interaction logic for orderWindow.xaml
    /// </summary>
    public partial class orderWindow : Window
    {
        public orderWindow()
        {
            InitializeComponent();
           
        }
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
    }
}
