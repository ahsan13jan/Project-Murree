
using FurnitureManagement.Service;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// <summary>
    /// Interaction logic for ItemMaterial.xaml
    /// </summary>
    public partial class LocationListView : Page
    {
        List<Location> listLocation;
        public LocationListView()
        {
            InitializeComponent();
            listLocation = LocationService.getLocations();
            listLocation.Insert(0, new Location() { Id = 0, Name = "Warehouse", Items = ItemService.getUnAssignedItems().Where(x=> !x.IsDeleted).ToList() });
            updateGrid();
        }

        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listLocation;
        }

        private void LocationDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 && dataGrid.SelectedIndex < listLocation.Count)
            {
                LocationArticlesListView next = new LocationArticlesListView(listLocation[dataGrid.SelectedIndex].Id);
                Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
                frame.Content = next;
            }
        }
    }
}
