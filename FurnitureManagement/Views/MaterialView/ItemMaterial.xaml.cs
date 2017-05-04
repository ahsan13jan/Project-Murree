
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
    public partial class ItemMaterial : Page
    {
        List<Item> listItems;
        public ItemMaterial( List<Item> list  )
        {
            InitializeComponent();
            listItems = list;
            updateGrid();
        }

        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listItems;
        }

        private void AssignMaterial_Click(object sender, RoutedEventArgs e)
        {
            int ID = listItems[dataGrid.SelectedIndex].Id;
            var nextView = new AssignMaterial(ID);

            Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
            frame.Content = nextView;


        }

        private void MaterialDetails_Click(object sender, RoutedEventArgs e)
        {
            int ID = listItems[dataGrid.SelectedIndex].Id;
            var nextView = new ItemMaterialDetail(ID);
            Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
            frame.Content = nextView;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
            frame.NavigationService.GoBack();

        }
    }
}
