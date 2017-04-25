using FurnitureManagement.Service;
using FurnitureManagement.Views.LocationN;
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

namespace FurnitureManagement.Views.JobNo
{
    /// <summary>
    /// Interaction logic for ShowJobItems.xaml
    /// </summary>
    public partial class ShowJobItems : Window
    {
        public ShowJobItems(int jobId)
        {
            InitializeComponent();
            BindDataGrid(jobId);
        
        }

        private void BindDataGrid(int jobId)
        {
            var a = JobItemService.getJobItems(jobId); ;
            dg_ShowItems.ItemsSource = a;
        }

        private void ShowItems_click(object sender, RoutedEventArgs e)
        {
            JobItem jobItem = ((JobItem)dg_ShowItems.SelectedItem);
            ItemsShow itemshow = new ItemsShow((int)jobItem.Id ,true);
            itemshow.Show();
                         
        }
    }
}
