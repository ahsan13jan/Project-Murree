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
    public partial class LocationArticlesListView : Page
    {
        List<Item> itemList;
        IEnumerable<ArticleQuantity> list;
        public LocationArticlesListView( int locationId)
        {
            InitializeComponent();
            itemList = ItemService.getItemsByLocation(locationId);


             list =   itemList
            .GroupBy(x => x.JobItem.Article)
            .Select(x =>
                new ArticleQuantity
                {
                    Article = x.Key,
                    Quantity = x.Count()
                });




            updateGrid();
        }

        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = list;
        }

        private void ItemDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 )
            {
                int index = 0;
                foreach (var a in list)
                {

                    if (index == dataGrid.SelectedIndex)
                    {
                        var listToSent = itemList.Where(x => x.JobItem.Article.Article_Id == a.Article.Article_Id).ToList();
                        ItemMaterial next = new ItemMaterial(listToSent);
                        Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
                        frame.Content = next;
                        break;

                    }

                    index++;
                }
            }
        }
    }

    class ArticleQuantity
    {
        public Article Article { get; set; }
        public decimal Quantity { get; set; }
    }
}
