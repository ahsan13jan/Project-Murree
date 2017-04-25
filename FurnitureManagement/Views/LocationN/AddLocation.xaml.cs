using FurnitureManagement.Service;
using FurnitureManagement.Views.JobNo.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FurnitureManagement.Views.LocationN;

namespace FurnitureManagement.Views.LocationN
{
    /// <summary>
    /// Interaction logic for AddLocation.xaml
    /// </summary>
    public partial class AddLocation : Page
    {
        List<Location> listOfLocations;
        FurnitureEntities context = new FurnitureEntities();
        public AddLocation()
        {
            InitializeComponent();
            BindCombo();
            listOfLocations = LocationService.getLocations();
            listOfLocations.Insert(0, new Location() { Id = 0, Name = "Warehouse", Items = ItemService.getUnAssignedItems().Where(x => !x.IsDeleted).ToList() });
            refreshGrid();



        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)

        {
            if (Input_Name.Text == "" || (int)cmb_CategoryLocation.SelectedIndex <= 0)
            {
                MessageBox.Show("Please enter all feilds :", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((int)cmb_CategoryLocation.SelectedValue == 2)
            {
                string validateMessage = ValidateOfficer();
                if (validateMessage != null)
                {
                    MessageBox.Show(validateMessage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    LocationService.saveLocation(new Location() { Name = Input_Name.Text, Category = (int)cmb_CategoryLocation.SelectedValue });
                    listOfLocations = LocationService.getLocations();
                    string locationName = listOfLocations.Where(x => x.Name == Input_Name.Text).First().Name;
                    LocationService.saveOfficer(new Officer()
                    {
                        Location_Name = txt_LocationName.Text,
                        Occupant_Name = txt_OccupantName.Text,
                        ArmyNo = txt_ArmyNo.Text,
                        Rank = txt_Rank.Text,
                        Designation = txt_Designation.Text,
                        Date_MarchIn = dtp_MarchIn.SelectedDate,
                        Location = LocationService.LocationId(locationName)
                    });
                }
            }
            else
            {
                if (txt_unitNumber == null)
                {
                    MessageBox.Show("Please Enter Unit Number ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    LocationService.saveLocation(new Location() { Name = Input_Name.Text, Category = (int)cmb_CategoryLocation.SelectedValue });
                    listOfLocations = LocationService.getLocations();
                    string locationName = listOfLocations.Where(x => x.Name == Input_Name.Text).First().Name;
                    LocationService.saveunit(new Unit()
                    {
                        unit_Number = txt_unitNumber.Text
                    });
                }
            }
            refreshGrid();

        }
        void refreshGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listOfLocations;
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            // ToDo
      



        }

        private void cmb_CategoryLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((int)cmb_CategoryLocation.SelectedValue == 2)
            {
                SP_Unit.Visibility = Visibility.Collapsed;
                SP_Officer.Visibility = Visibility.Visible;
            }
            else
            {
                SP_Unit.Visibility = Visibility.Visible;
                SP_Officer.Visibility = Visibility.Collapsed;
            }

        }

        private void BindCombo()
        {
            List<Category> categories = JobService.getCategories();
            cmb_CategoryLocation.ItemsSource = categories;
        }

        private void EditOfficer_Click(object sender, RoutedEventArgs e)
        {
            int locationId = ((Location)dataGrid.SelectedItem).Id;
            Officer officer = LocationService.getOfficerByLocationId(locationId);
            if (officer != null)
            {
                OfficerEdit oe = new OfficerEdit(officer);
                oe.Show();
            }
            else
                MessageBox.Show("No officer exist on this Location");
        }

        private void DeleteOfficer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex < listOfLocations.Count)
            {

                int selectedId = listOfLocations[dataGrid.SelectedIndex].Id;
                if (LocationService.isAssigned(selectedId))
                {
                    MessageBox.Show("INVALID OPERATION : Please Unassign it , to make this operation permissable.");
                    return;
                }

                LocationService.deleteLocation(selectedId);
                listOfLocations.Remove(listOfLocations[dataGrid.SelectedIndex]);
                refreshGrid();
            }
        }

        private void DatGridOfficer_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedValue != null)
            {
                int id = ((Location)dataGrid.SelectedValue).Id;
                Location loc = LocationService.getLocationById(id);
                MenuItem removedItem = null;
                foreach (MenuItem item in dataGrid.ContextMenu.Items)
                {
                    if (item.Name == "officer")
                    {
                        removedItem = item;
                        break;
                    }
                }

                if (removedItem != null)
                {
                    dataGrid.ContextMenu.Items.Remove(removedItem);
                }

                if (loc.Category == 2)
                {
                    ContextMenu cm = dataGrid.ContextMenu;
                    MenuItem m = new MenuItem();
                    m.Name = "officer";
                    m.Header = "Officers Data";

                    m.Icon = new System.Windows.Controls.Image { Source = new BitmapImage(new Uri("D:\\aHSAN eNG\\FurnitureManagement\\delete.jpg", UriKind.Relative)) };
                    m.Click += new RoutedEventHandler(DataOfficer_Click);
                    cm.Items.Add(m);
                }
            }


        }

        private void DataOfficer_Click(object sender, RoutedEventArgs e)
        {
            Location loc = ((Location)dataGrid.SelectedItem);
            ShowOfficers so = new ShowOfficers(loc.Id);
            so.Show();

        }

        private string ValidateOfficer()
        {
            string message = null;
            if (txt_OccupantName.Text == "")
                message = message + "Please enter Occupant Name :\n";
            if (txt_LocationName.Text == "")
                message = message + "Please enter Location Name :\n";
            if (txt_Rank.Text == "")
                message = message + "Please enter Rank of Officer :\n";
            if (txt_Designation.Text == "")
                message = message + "Please enter Designation :\n";
            if (txt_ArmyNo.Text == "")
                message = message + "Please enter Army No :\n";
            if (dtp_MarchIn.SelectedDate == null)
                message = message + "Please enter March In Date :\n";

            return message;

        }


        private void Input_Name_SelectionChanged(object sender, RoutedEventArgs e)
        {
            cmb_CategoryLocation.IsEnabled = true;
            btn_add.IsEnabled = true;
        }

        private void Items_Click(object sender, RoutedEventArgs e)
        {
            Location loc = ((Location)dataGrid.SelectedItem);
            ItemsShow itemShow = new ItemsShow(loc.Id);
            itemShow.Show();
        }


    }
}
