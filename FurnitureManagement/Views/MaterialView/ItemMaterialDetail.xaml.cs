
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
    /// <summary>
    /// Interaction logic for ItemMaterialDetail.xaml
    /// </summary>
    public partial class ItemMaterialDetail : Page
    {

        List<MaterialItem> list;
        public ItemMaterialDetail( int Id )
        {
            InitializeComponent();
            list = MaterialItemService.getMaterialItemDetails(Id);
            updateGrid();
        }

        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = list;
        }
    }
}
