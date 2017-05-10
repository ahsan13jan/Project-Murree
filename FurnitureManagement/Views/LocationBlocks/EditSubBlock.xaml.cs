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
    /// Interaction logic for EditSubBlock.xaml
    /// </summary>
    public partial class EditSubBlock : Window
    {
        Block obj;
        List<Block> listblocks;
        public EditSubBlock(Block b)
        {
            InitializeComponent();
            listblocks = BlockService.getBlocks();
            txt_name.Text = b.Name;
            CB_Block.SelectedValue = listblocks.Where(x=>x.Id==b.ParentId).First().Id;
            ResetCombo();
            obj = b;
            
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (txt_name.Text != "" && CB_Block.SelectedValue !=null)
            {
                obj.Name = txt_name.Text;
                obj.ParentId = CB_Block.SelectedIndex + 1;
                if (BlockService.UpdateSubBlock(obj))
                {
                    MessageBox.Show("Sub Block Updated Successfully", "INFORMATION", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sub Block Updated Successfully", "ERROR", MessageBoxButton.OK);
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Please Edit Name and Block of the Following Sub Block", "ERROR", MessageBoxButton.OK);

            }
        }

        void ResetCombo()
        {
            CB_Block.ItemsSource = listblocks;
        }


    }
}
