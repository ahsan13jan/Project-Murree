using FurnitureManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FurnitureManagement.Views.MaterialView
{
    /// <summary>
    /// Interaction logic for AssignMaterial.xaml
    /// </summary>
    public partial class AssignMaterial : Page
    {
        Item item;
        List<Material> materialList;

        public AssignMaterial( int Id )
        {
            InitializeComponent();
            item = ItemService.getItemById(Id);
            SetupView();
        }

        void SetupView()
        {
            Input_ID.Content = item.Id.ToString();
            Input_JobNo.Content =  item.JobItem.Job.JobNo;
            Input_Category.Content =  item.JobItem.Job.Category;
            Input_Type.Content = item.JobItem.Article.Article_DESC;
            //Input_ = Input_ID.Content + item.Id.ToString();

            getData();

        }

        void getData()
        {
            Input_Quantity.Text = "";
            Input_RemaingQuantity.Content = "";
            CB_Material.ItemsSource = null;
            CB_Material.SelectedIndex = -1;
            materialList = MaterialService.getMaterialModels();
            CB_Material.ItemsSource = materialList;
        }

        private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = CB_Material.SelectedIndex;
           if (selectedIndex >= 0  && selectedIndex <= materialList.Count )
            {
                Input_RemaingQuantity.Content = materialList[selectedIndex].Quantity;
            }

        }

        private void MaterialAssign_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CB_Material.SelectedIndex;

            float f;
            var text = Input_Quantity.Text;

            if (selectedIndex >= 0  && text != ""  &&
                float.TryParse(text, out f ) && 
                Convert.ToDouble(text) <= (double)materialList[selectedIndex].Quantity &&
                 Convert.ToDouble(text) > 0 )
            {
                MaterialItemService.assignMaterialToItem(item.Id, materialList[selectedIndex].Id, Convert.ToDecimal(text) , (int)materialList[selectedIndex].Rate );
                MessageBox.Show("Material Assigned ");
                getData();
            }
            else
            {
                MessageBox.Show("Please Select Material and Add Quantity ( Less than Remaining Quantity) ");
            }
        }

        private void MaterialBundleAssign_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
