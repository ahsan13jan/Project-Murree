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

        public AssignMaterial(int Id)
        {
            InitializeComponent();
            item = ItemService.getItemById(Id);
            SetupView();
        }

        void SetupView()
        {
            Input_ID.Content = item.UIN.ToString();
            Input_JobNo.Content = item.JobItem.Job.JobNo;
            Input_Category.Content = item.JobItem.Job.Category1.Category_DESC;
            Input_Type.Content = item.JobItem.Article.Article_DESC;
            Input_Location.Content = Input_Location.Content + (item.Location == null ? "Warehouse" : item.Location.Display);
            getData();

        }

        void getData()
        {
            Input_Quantity.Text = "";
            Input_RemaingQuantity.Text = "";
            CB_Material.ItemsSource = null;
            CB_Material.SelectedIndex = -1;
            materialList = MaterialService.getMaterials();
            CB_Material.ItemsSource = materialList;
            bundleList = MaterialBundleService.MaterialBundles((int)item.JobItem.ArticleId);
            CB_Bundle.ItemsSource = null;
            CB_Bundle.ItemsSource = bundleList;
        }

        private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = CB_Material.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex <= materialList.Count)
            {
                Input_RemaingQuantity.Text = materialList[selectedIndex].Quantity.ToString();
            }

        }

        private void MaterialAssign_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                int selectedIndex = CB_Material.SelectedIndex;

                float f;
                var text = Input_Quantity.Text;

                if (selectedIndex >= 0 && text != "" &&
                    float.TryParse(text, out f) &&
                    Convert.ToDouble(text) <= (double)materialList[selectedIndex].Quantity &&
                     Convert.ToDouble(text) > 0)
                {
                    MaterialItemService.assignMaterialToItem(item.Id, materialList[selectedIndex].Id, Convert.ToDecimal(text), (int)materialList[selectedIndex].Rate);
                    MessageBox.Show("Material Assigned ");
                    getData();
                }
                else
                {
                    MessageBox.Show("Please Select Material and Add Quantity ( Less than Remaining Quantity) ");
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("ERROR :" + err.ToString());
            }
        }

        private void MaterialBundleAssign_Click(object sender, RoutedEventArgs e)
        {
            try {

                if (CB_Bundle.SelectedIndex != -1)
                {
                    var selectedBundle = bundleList[CB_Bundle.SelectedIndex];

                    if (MaterialBundleService.IsBundleAvailable(selectedBundle.Id, (int)item.JobItem.ArticleId))
                    {

                        AssignBundle(selectedBundle);
                        MessageBox.Show("Bundle Assigned ");
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("No Enough Material");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("ERROR :" + err.ToString());
            }

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
            frame.NavigationService.GoBack();

        }

        void resetMaterialBundleGrid()
        {
            if (CB_Bundle.SelectedIndex != -1)
            {
                dataGrid.ItemsSource = null;
                var list  = bundleList[CB_Bundle.SelectedIndex].MaterialBundleItems;

                if (bundleList[CB_Bundle.SelectedIndex].Id <= 3)
                    list.ToList().ForEach(x => x.Quantity = x.Quantity * (decimal)item.JobItem.Article.Multiple);

                dataGrid.ItemsSource = list;
            }
            else
                dataGrid.ItemsSource = null;

        }

        private void BundleMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetMaterialBundleGrid();
        }

        void AssignBundle( MaterialBundle bundle , decimal Multiplier = 1 )
        {

                if (bundle.Id == 7)
                {
                    AssignBundle(MaterialBundleService.getMaterialBundleById(5), (decimal)(3.0 / 2.0));
                }
                if (bundle.Id == 8)
                {
                    AssignBundle(MaterialBundleService.getMaterialBundleById(5), (decimal)(5.0 / 4.0));
                }

                if (bundle.Id <= 3)
                {
                    bundle.MaterialBundleItems.ToList().ForEach(x =>
                    {

                        MaterialItemService.assignMaterialToItem(item.Id, (int)x.MaterialId, (decimal)x.Quantity * Multiplier * (decimal)item.JobItem.Article.Multiple, (int)x.Material.Rate);

                    });
                }
                else
                {
                    bundle.MaterialBundleItems.ToList().ForEach(x =>
                    {

                        MaterialItemService.assignMaterialToItem(item.Id, (int)x.MaterialId, (decimal)x.Quantity * Multiplier, (int)x.Material.Rate);

                    });
                }

        }
    }
}
