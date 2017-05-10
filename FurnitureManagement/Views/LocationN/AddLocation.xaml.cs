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
        List<Block> listBlock;
        List<Block> listSubBlock;
        FurnitureEntities context = new FurnitureEntities();

        public List<Block> filteredBlocks
        {
            get
            {

                if (cmb_CategoryLocation.SelectedIndex != -1 && listBlock != null)
                    return listBlock.Where(x => x.CategoryId == cmb_CategoryLocation.SelectedIndex + 1).ToList();
                else
                    return new List<Block>();
            }
        }
        public AddLocation()
        {
            InitializeComponent();
            BindCombo();
            refreshGrid();
            listBlock = BlockService.getBlocks();

        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)

        {

            if (cmb_CategoryLocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please Enter all feilds");
                return;
            }



            if (cmb_CategoryLocation.SelectedIndex == 0)
            {
                if (CB_Block.SelectedIndex == -1 || CB_SubBlock.SelectedIndex == -1 || txt_unitNumber.Text == "" )
                {
                    MessageBox.Show("Please Enter all feilds");
                    return;
                }
                else
                {
                    var location = new Location() { BlockId = filteredBlocks[CB_Block.SelectedIndex].Id, SubBlockId = listSubBlock[CB_SubBlock.SelectedIndex].Id, Category = cmb_CategoryLocation.SelectedIndex + 1 };
                    var unit = new Unit()
                    {
                        unit_Number = txt_unitNumber.Text
                    };

                    LocationService.saveLocationUnit(location, unit);
                }
            }

            if (cmb_CategoryLocation.SelectedIndex == 1)
            {
                string validateMessage = ValidateOfficer();
                if (validateMessage != null)
                {
                    MessageBox.Show(validateMessage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {

                    var location = new Location() { BlockId = filteredBlocks[CB_Block.SelectedIndex].Id, SubBlockId = listSubBlock[CB_SubBlock.SelectedIndex].Id, Category = cmb_CategoryLocation.SelectedIndex + 1 };

                    var officer = new Officer()
                    {
                        Location_Name = "",
                        Occupant_Name = txt_OccupantName.Text,
                        ArmyNo = txt_ArmyNo.Text,
                        Rank = txt_Rank.Text,
                        Designation = txt_Designation.Text,
                        Date_MarchIn = dtp_MarchIn.SelectedDate
                    } ;


                    LocationService.saveLocationOfficer(location, officer);


                }
            }
            MessageBox.Show("Location added Successfully");
            refreshGrid();

        }
        void refreshGrid()
        {

            cmb_CategoryLocation.SelectedIndex = -1;
            CB_Block.SelectedIndex = -1;
            CB_SubBlock.SelectedIndex = -1;
            txt_OccupantName.Text = "";
            txt_Rank.Text = "";
            txt_ArmyNo.Text = "";
            txt_Designation.Text = "";
            txt_Designation.Text = "";
            txt_Designation.Text = "";
            dtp_MarchIn.SelectedDate = null;
            txt_unitNumber.Text = "";


            CB_Block.ItemsSource = null;
            CB_SubBlock.ItemsSource = null;

            listOfLocations = LocationService.getLocations();
            listOfLocations.Insert(0, new Location() { Id = 0, Name = "Warehouse", Items = ItemService.getUnAssignedItems().Where(x => !x.IsDeleted).ToList() });

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listOfLocations;
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            // ToDo
      



        }

        private void cmb_CategoryLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetBlockComboBox();
            if (cmb_CategoryLocation.SelectedIndex == 1)
            {
                SP_Unit.Visibility = Visibility.Collapsed;
                SP_Officer.Visibility = Visibility.Visible;
            }
            else if  (cmb_CategoryLocation.SelectedIndex == 0 )
            {
                SP_Unit.Visibility = Visibility.Visible;
                SP_Officer.Visibility = Visibility.Collapsed;
            }
            else
            {
                SP_Unit.Visibility = Visibility.Collapsed;
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

                if ( loc != null && loc.Category == 2)
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


        void resetSubBlockComboBox()
        {
            if (CB_Block.SelectedIndex != -1)
            {
                CB_SubBlock.ItemsSource = null;
                listSubBlock = BlockService.getSubBlocks(filteredBlocks[CB_Block.SelectedIndex].Id).ToList().Where(x=> x.Locations1.Count == 0).ToList();
                CB_SubBlock.ItemsSource = listSubBlock;
            }

        }
        void resetBlockComboBox()
        {
            if (cmb_CategoryLocation.SelectedIndex != -1)
            {
                CB_Block.ItemsSource = null;
                CB_Block.ItemsSource = filteredBlocks;
            }

        }

        private void Block_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetSubBlockComboBox();
        }

    }
}
