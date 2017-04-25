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

namespace FurnitureManagement.Views.ItemViews
{
    /// <summary>
    /// Interaction logic for ItemsView.xaml
    /// </summary>
    public partial class ItemsView : Page
    {

        List<Article> listArticles;
        List<Location> listLocations;
        List<Job> listJobs;

        List<Item> listItems;

        int selectedCategoryId = -1;
        List<Article> filteredArticles
        {
            get
            {
                if (selectedCategoryId != -1)
                    return listArticles.Where(x => x.Category1.Category_Id == selectedCategoryId).ToList();
                else
                    return new List<Article>();
            }
        }

        List<Location> filteredLocation
        {
            get
            {
                if (selectedCategoryId != -1)
                    return listLocations.Where(x => x.Category1 == null || x.Category1.Category_Id == selectedCategoryId ).ToList();
                else
                    return new List<Location>();
            }
        }
        public ItemsView()
        {
            InitializeComponent();
            
            setupView();
        }
        void setupView()
        {
            listItems = ItemService.getItems();
            listArticles = ArticleService.getArticles();
            listLocations = LocationService.getLocations();
            listLocations.Insert(0, new Location() { Id = 0, Name = "Warehouse", Items = ItemService.getUnAssignedItems().Where(x => !x.IsDeleted).ToList() });



            listJobs = JobService.getJobs();

            CB_Jobs.ItemsSource = null;
            CB_Jobs.ItemsSource = listJobs;

            CB_Locations.ItemsSource = null;
            CB_Locations.ItemsSource = listLocations;


        }

        void setCombo()
        {
            CB_Articles.ItemsSource = null;
            CB_Articles.ItemsSource = filteredArticles;

            CB_Locations.ItemsSource = null;
            listLocations = filteredLocation;
            CB_Locations.ItemsSource = listLocations;
        }

        private void Jobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ( CB_Jobs.SelectedIndex >= 0)
            { 
                selectedCategoryId = listJobs[CB_Jobs.SelectedIndex].Category1.Category_Id;
                setCombo();
            }
            updateGrid();
        }

        private void Articles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGrid();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            updateGrid();
        }

        void updateGrid()
        {
            listItems = ItemService.filterItems((int?)CB_Jobs.SelectedValue, (int?)CB_Articles.SelectedValue, (int?)CB_Locations.SelectedValue);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listItems;
        }
    }
}
