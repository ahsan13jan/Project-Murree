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
using System.Windows.Shapes;

namespace FurnitureManagement.Views.LocationBlocks
{
    /// <summary>
    /// Interaction logic for Edit_Block.xaml
    /// </summary>
    public partial class Edit_Block : Window
    {
        Block updated;
        public Edit_Block( Block b)
        {

            InitializeComponent();
            BindCombo();
            cmb_Category.SelectedValue =(int) b.CategoryId;
            txt_name.Text = b.Name;
            updated = b;
        }

       

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            updated.CategoryId = (int)cmb_Category.SelectedValue;
            updated.Name = txt_name.Text;
            if(BlockService.UpdateBlock(updated))
            {
                MessageBox.Show("Block Updated Succesfully ", "SUCCESS", MessageBoxButton.OK);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Block is not Updated Succesfully ", "ERROR", MessageBoxButton.OK);

            }

        }

        private void BindCombo()
        {
            List<Category> categories = JobService.getCategories();
            cmb_Category.ItemsSource = categories;
        }

    }
}
