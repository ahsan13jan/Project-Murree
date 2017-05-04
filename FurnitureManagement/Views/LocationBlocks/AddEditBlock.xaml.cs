using FurnitureManagement.Service;
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

namespace FurnitureManagement.Views.LocationBlocks
{
    /// <summary>
    /// Interaction logic for AddEditBlock.xaml
    /// </summary>
    /// 
    public partial class AddEditBlock : Page
    {



        public AddEditBlock()
        {
            InitializeComponent();
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            if ( Input_Name.Text != "" && cmb_CategoryLocation.SelectedIndex != -1 )
            {
                BlockService.addBlock(Input_Name.Text, cmb_CategoryLocation.SelectedIndex + 1);
                Input_Name.Text = "";
                cmb_CategoryLocation.SelectedIndex = -1;
                MessageBox.Show("Added Successfully");
            }

        }
    }
}
