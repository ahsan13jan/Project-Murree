
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

namespace FurnitureManagement
{
    /// <summary>
    /// Interaction logic for AssignFurniture.xaml
    /// </summary>
    public partial class AssignFurniture : Page
    {
        List<Item> unAssignedItemList;
        List<Item> assignedItemList;
        List<Location> locationList;


        string selectedCategory = "";


        List<Item> filteredItemList
        {
            get
            {
                return unAssignedItemList.Where(x => x.JobItem.Job.Category1.Category_DESC == selectedCategory).ToList();
            }
        }

        public AssignFurniture()
        {
            InitializeComponent();
            updateGrid();
            getData();
            BindCombo();

        }

        void getData()
        {
            locationList = LocationService.getLocations();
            unAssignedItemList = ItemService.getUnAssignedItems();
            assignedItemList = ItemService.getAssignedItems();
        }

        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            int locationId = (int)cmblocation.SelectedValue;
            int itemId = (int)cmbfurniture.SelectedValue;
            bool itemAssigned = ItemService.assignItem(locationId, itemId);
            MessageBox.Show("Item Assigned Successfully");
            updateGrid();
            btnAssign.IsEnabled = false;
        }

        void updateGrid()
        {
            getData();
            resetCombo();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = assignedItemList;
        }


        private void cmblocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int selectedIndex = cmblocation.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < locationList.Count)
            {
                var selectedLocation = locationList[selectedIndex];
                selectedCategory = selectedLocation.Category1.Category_DESC;
                BindFurnitureCombo();
            }



        }

        private void BindCombo()
        {
            cmblocation.ItemsSource = locationList;
            cmbfurniture.IsEnabled = false;
        }

        void resetCombo()
        {
            cmblocation.SelectedValue = -1;
            cmbfurniture.SelectedValue = -1;
        }

        private void BindFurnitureCombo()
        {
            var a = filteredItemList;
            cmbfurniture.ItemsSource = a;
            cmbfurniture.IsEnabled = true;
            btnAssign.IsEnabled = true;
        }

        private void UnAssignItem_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex < assignedItemList.Count)
            {
                int selectedId = assignedItemList[dataGrid.SelectedIndex].Id;
                //CB_Item.Items.Remove(CB_Item.SelectedItem);
                ItemService.unAssignItem(selectedId);
                assignedItemList.Remove(assignedItemList[dataGrid.SelectedIndex]);
                updateGrid();
            }

        }



    }
}
