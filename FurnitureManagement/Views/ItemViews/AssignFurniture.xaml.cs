
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
        List<Job> jobList;
        List<JobItem> jobItemList;

        string selectedCategory = "";



        List<Job> filteredJobList
        {
            get
            {
                return jobList.Where(x => x.Category1.Category_DESC == selectedCategory).ToList();
            }
        }

        public AssignFurniture()
        {
            InitializeComponent();
            jobList = JobService.getJobs();
            updateGrid();
            BindCombo();


        }

        void getData()
        {
            
            locationList = LocationService.getLocations();
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




        private void BindCombo()
        {
            cmblocation.ItemsSource = locationList;
        }

        void resetCombo()
        {
            cmblocation.SelectedValue = -1;
            cmbJob.SelectedValue = -1;
            cmbjobItem.SelectedValue = -1;
            cmbfurniture.SelectedValue = -1;
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

        private void cmblocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int selectedIndex = cmblocation.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < locationList.Count)
            {
                var selectedLocation = locationList[selectedIndex];
                selectedCategory = selectedLocation.Category1.Category_DESC;

                cmbJob.ItemsSource = null;
                cmbJob.ItemsSource = filteredJobList;

            }
        }

        private void Job_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( cmbJob.SelectedIndex != -1)
            {
                jobItemList = JobItemService.getJobItems(jobList[cmbJob.SelectedIndex].Id);
                cmbjobItem.ItemsSource = null;
                cmbjobItem.ItemsSource = jobItemList;
            }
        }

        private void JobItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbjobItem.SelectedIndex != -1)
            {
                unAssignedItemList = ItemService.getUnAssignedItemsWithJobItemId( jobItemList[cmbjobItem.SelectedIndex].Id );
                cmbfurniture.ItemsSource = null;
                cmbfurniture.ItemsSource = unAssignedItemList;
            }
        }
    }
}
