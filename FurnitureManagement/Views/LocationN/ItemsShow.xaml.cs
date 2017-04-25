using FurnitureManagement.Service;
using FurnitureManagement.Views.ItemViews;
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

namespace FurnitureManagement.Views.LocationN
{
    /// <summary>
    /// Interaction logic for ItemsShow.xaml
    /// </summary>
    public partial class ItemsShow : Window
    {
        public ItemsShow( int locId)
        {
            InitializeComponent();
            BindDataGrid(locId);
        }

        private void BindDataGrid(int locationId)
        {
           
            dg_ItemsShow.ItemsSource = LocationService.getAllItemByLocationId(locationId);
        }
        public ItemsShow (int jobItemId , bool val)
        {
            InitializeComponent();
            BindDataGridJobItem(jobItemId);
        }

        private void BindDataGridJobItem(int id)
        {
            List<Item> list = ItemService.getItemsByJobItemId(id);
            list.ForEach(x=> {
                if(x.Location==null)
                    x.Location = new Location(){ Name = "Warehouse" };
            });
            dg_ItemsShow.ItemsSource = list;
           
            dg_ItemsShow.Columns[1].Visibility = Visibility.Collapsed;
           
        }

        private void ItemHistory_Click(object sender, RoutedEventArgs e)
        {
            Item item = ((Item)dg_ItemsShow.SelectedItem);
            ShowItemHistory ih = new ShowItemHistory(item.Id);
            ih.Show();
        }
    }
}
