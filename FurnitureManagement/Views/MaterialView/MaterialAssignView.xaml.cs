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

namespace FurnitureManagement.Views.MaterialView
{

    public partial class MaterialAssignView : Page
    {

        List<Article> listArticles;
        List<MaterialItem> listMaterialItems;
        List<Location> listLocations;
        List<Material> listMaterials;


        public MaterialAssignView()
        {
            InitializeComponent();
            
            setupView();
        }
        void setupView()
        {
            listArticles = ArticleService.getArticles();
            listLocations = LocationService.getLocations();
            listMaterials = MaterialService.getMaterials();
            listLocations.Add( new Location() { Id = 0 }  );
            CB_Articles.ItemsSource = null;
            CB_Articles.ItemsSource = listArticles;
            CB_Locations.ItemsSource = null;
            CB_Locations.ItemsSource = listLocations;
            CB_Material.ItemsSource = null;
            CB_Material.ItemsSource = listMaterials;
        }





        private void Articles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGrid();
        }



        private void Update_Click(object sender, RoutedEventArgs e)
        {
            updateGrid();
        }
        private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGrid();
        }

        void updateGrid()
        {
            listMaterialItems = MaterialItemService.getMaterialItemFilter(Input_From.SelectedDate , Input_To.SelectedDate , (int?)CB_Articles.SelectedValue , (int?)CB_Locations.SelectedValue , (int?)CB_Material.SelectedValue);

            listMaterialItems.ForEach( x=> {
                if (x.Location == null)
                    x.Location = new Location();
            });

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listMaterialItems;
        }

        private void Location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGrid();
        }

        private void CheckBoxChangedInputFrom(object sender, RoutedEventArgs e)
        {
            if ((bool)C_InputFrom.IsChecked)
            {
                Input_From.IsEnabled = true;
            }
            else
            {
                Input_From.IsEnabled = false;
                Input_From.SelectedDate =null;
            }
        }

        private void CheckBoxChangedInputTo(object sender, RoutedEventArgs e)
        {
            if ((bool)C_InputTo.IsChecked)
            {
                Input_To.IsEnabled = true;
            }
            else
            {
                Input_To.IsEnabled = false;
                Input_To.SelectedDate = null;
            }
        }

        private void CheckBoxChangedArticles(object sender, RoutedEventArgs e)
        {
            if ((bool)C_Articles.IsChecked)
            {
                CB_Articles.IsEnabled = true;
            }
            else
            {
                CB_Articles.IsEnabled = false;
                CB_Articles.SelectedIndex = -1;
            }
        }

        private void CheckBoxChangedLocation(object sender, RoutedEventArgs e)
        {
            if ((bool)C_Locations.IsChecked)
            {
                CB_Locations.IsEnabled = true;
            }
            else
            {
                CB_Locations.IsEnabled = false;
                CB_Locations.SelectedIndex = -1;
            }
        }

     

        private void CheckBoxChangedMaterial(object sender, RoutedEventArgs e)
        {
            if ((bool)C_Material.IsChecked)
            {
                CB_Material.IsEnabled = true;
            }
            else
            {
                CB_Material.IsEnabled = false;
                CB_Material.SelectedIndex = -1;
            }
        }
    }
}
