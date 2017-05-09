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
    public partial class AddEditSubBlock : Page
    {
        List<Block> listblocks;
        List<Block> listSubBlock;
        public AddEditSubBlock()
        {
            InitializeComponent();
            listblocks = BlockService.getBlocks();
            resetComboBox();
        }

        void resetComboBox()
        {
            CB_Block.ItemsSource = null;
            CB_Block.ItemsSource = listblocks;
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            if ( Input_Name.Text != "" && CB_Block.SelectedIndex != -1 )
            {
                BlockService.addSubBlock(listblocks[CB_Block.SelectedIndex].Id, Input_Name.Text);
                Input_Name.Text = "";
                CB_Block.SelectedIndex = -1;
                resetComboBox();
                MessageBox.Show("Added Successfully");
            }

        }

        void resetSubComboBox()
        {
            if (CB_Block.SelectedIndex != -1)
            {
                CB_SubBlock.ItemsSource = null;
                listSubBlock=BlockService.getSubBlocks( listblocks[CB_Block.SelectedIndex].Id);
                CB_SubBlock.ItemsSource = listSubBlock; 

            }

        }

        private void Block_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetSubComboBox();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Block obj = new Block();
            obj.Name = Input_Name.Text;
            obj.Id = listSubBlock[CB_SubBlock.SelectedIndex].Id;
            obj.ParentId = listSubBlock[CB_SubBlock.SelectedIndex].ParentId;

            EditSubBlock sb = new EditSubBlock(obj);
            sb.Show();

        }
    }
}
