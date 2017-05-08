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
using FurnitureManagement.Service;
using FurnitureManagement.Views.ItemViews;
using FurnitureManagement.Views.JobNo;
using FurnitureManagement.Views;
using FurnitureManagement.Views.MaterialView;
using FurnitureManagement.Views.Warehouse;
using FurnitureManagement.Views.LocationN;
using FurnitureManagement.Views.LocationBlocks;
using FurnitureManagement.Views.Mail;
//
namespace FurnitureManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Frame.Content = new AddItem();

        }


        private void AddFurniture_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            AddItem ViewNextForm = new AddItem();
            Frame.Content = ViewNextForm;
        }
        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            AddLocation ViewNextForm = new AddLocation();
            Frame.Content = ViewNextForm;
        }

        private void AssignFurniture_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            AssignFurniture ViewNextForm = new AssignFurniture();
            Frame.Content = ViewNextForm;
            //Closing current form
        }

        private void ViewFurniture_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            ItemsView ViewNextForm = new ItemsView();
            Frame.Content = ViewNextForm;
            //Closing current form
        }

        private void AddJob_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            JobAddView ViewNextForm = new JobAddView();
            Frame.Content = ViewNextForm;
        }
        private void Material_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            MaterialViewP ViewNextForm = new MaterialViewP();
            Frame.Content = ViewNextForm;
        }

        private void MaterialAddEdit_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            MaterialAddEdit ViewNextForm = new MaterialAddEdit();
            Frame.Content = ViewNextForm;
        }

        private void Warehouse_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            WarehouseView ViewNextForm = new WarehouseView();
            Frame.Content = ViewNextForm;
        }
        private void LocationList_Click(object sender, RoutedEventArgs e)
        {
            //Opening requred form
            LocationListView ViewNextForm = new LocationListView();
            Frame.Content = ViewNextForm;
        }

        private void MaterialRateChange_Click(object sender, RoutedEventArgs e)
        {
            MaterialRateEdit ViewNextForm = new MaterialRateEdit();
            Frame.Content = ViewNextForm;
        }

        private void AddBlock_Click(object sender, RoutedEventArgs e)
        {
            AddEditBlock ViewNextForm = new AddEditBlock();
            Frame.Content = ViewNextForm;
        }

        private void AddSubBlock_Click(object sender, RoutedEventArgs e)
        {
            AddEditSubBlock ViewNextForm = new AddEditSubBlock();
            Frame.Content = ViewNextForm;
        }

        private void MailView_Click(object sender, RoutedEventArgs e)
        {
            AddMail ViewNextForm = new AddMail();
            Frame.Content = ViewNextForm;
        }

        private void MaterialAssignView_Click(object sender, RoutedEventArgs e)
        {
            MaterialAssignView ViewNextForm = new MaterialAssignView();
            Frame.Content = ViewNextForm;
        }
    }
}
