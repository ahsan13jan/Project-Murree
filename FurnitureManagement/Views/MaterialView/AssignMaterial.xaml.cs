using FurnitureManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace FurnitureManagement.Views.MaterialView
{
    /// <summary>
    /// Interaction logic for AssignMaterial.xaml
    /// </summary>
    public partial class AssignMaterial : Page
    {
        Item item;
        List<Material> materialList;
        List<MaterialBundle> bundleList;

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

            bundleList = MaterialBundleService.MaterialBundles();
            CB_Bundle.ItemsSource = null;
            CB_Bundle.ItemsSource = bundleList;
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

            if ( CB_Bundle.SelectedIndex != -1 )
            {
                    var selectedBundle = bundleList[CB_Bundle.SelectedIndex];

                if (MaterialBundleService.IsBundleAvailable(selectedBundle.Id))
                {

                    selectedBundle.MaterialBundleItems.ToList().ForEach(x =>
                    {

                        MaterialItemService.assignMaterialToItem(item.Id, (int)x.MaterialId, (decimal)x.Quantity, (int)x.Material.Rate);

                    });
                    MessageBox.Show("Bundle Assigned ");
                    getData();
                }
                else
                {
                    MessageBox.Show("No Enough Material");
                }
            }

        }
    }
}
