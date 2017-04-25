using FurnitureManagement.Views;
using FurnitureManagement.Views.ItemViews;
using FurnitureManagement.Views.JobNo;
using FurnitureManagement.Views.LocationN;
using FurnitureManagement.Views.MaterialView;
using FurnitureManagement.Views.Warehouse;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FurnitureManagement
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void AddFurniture_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //MainWindow ViewNextForm = new MainWindow();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }
        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //AddLocation ViewNextForm = new AddLocation();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }

        private void AssignFurniture_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            //AssignFurniture ViewNextForm = new AssignFurniture();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }



        private void ViewFurniture_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //ItemsView ViewNextForm = new ItemsView();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }

        private void AddJob_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            //JobAddView ViewNextForm = new JobAddView();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }
        private void Material_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //MaterialViewP ViewNextForm = new MaterialViewP();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }

        private void MaterialAddEdit_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //MaterialAddEdit ViewNextForm = new MaterialAddEdit();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }

        private void Warehouse_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //WarehouseView ViewNextForm = new WarehouseView();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }
        private void LocationList_Click(object sender, RoutedEventArgs e)
        {
            ////Opening requred form
            //LocationListView ViewNextForm = new LocationListView();
            //ViewNextForm.Show();
            ////Closing current form
            //closeCurrentForm();
        }
    }

}
