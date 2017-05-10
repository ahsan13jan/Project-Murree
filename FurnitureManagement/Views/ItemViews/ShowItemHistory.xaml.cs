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
    /// Interaction logic for ShowItemHistory.xaml
    /// </summary>
    public partial class ShowItemHistory : Window
    {
        public ShowItemHistory(Item item)
        {
            InitializeComponent();
            Label_UIN.Content += item.UIN;
            BindDataGrid(item.Id);
        }

        private void BindDataGrid(int itemId)
        {
            dg_ShowItemHistory.ItemsSource = FurnitureManagement.Service.ItemLocationService.getItemHistoryByItemId(itemId);
            
            
        }
    }
}
