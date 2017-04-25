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

namespace FurnitureManagement.Views.JobNo.Location
{
    /// <summary>
    /// Interaction logic for OfficerEdit.xaml
    /// </summary>
    public partial class OfficerEdit : Window
    {
        Officer ofcr;
      public bool value = false;
      DateTime tempdate;
      int previousofcrId;
      int locationId;
        public OfficerEdit(Officer officer)
        {
            previousofcrId = officer.Officer_id;
            
            this.ofcr = officer;
            this.DataContext = officer;
            InitializeComponent();
            txtLocation.Text = ofcr.Location_Name;
            txtOccupantname.Text = ofcr.Occupant_Name;
            txtRank.Text = ofcr.Rank;
            txtDesignation.Text = ofcr.Designation;
            txtMarchin.SelectedDate = ofcr.Date_MarchIn;
            txtArmyNo.Text = ofcr.ArmyNo;
            locationId =(int) ofcr.Location;
           
           

        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            
            Officer newOfficer = new Officer();
            newOfficer.Location_Name = txtLocation.Text;
            newOfficer.Occupant_Name = txtOccupantname.Text;
            newOfficer.Rank = txtRank.Text;
            newOfficer.Designation = txtDesignation.Text;
            newOfficer.ArmyNo = txtArmyNo.Text;
            newOfficer.Date_MarchIn = txtMarchin.SelectedDate;
            newOfficer.Location = locationId;
            tempdate =(DateTime) txtMarchin.SelectedDate;
            LocationService.saveOfficer(newOfficer);
            bool update = LocationService.updatePreviousOficer(previousofcrId, tempdate);
            if (update != true)
                MessageBox.Show("Officer is not Updated: ");
            else
                MessageBox.Show("Officer updated at this Location ");
            this.Close();
        }

        private void updateOfficer(int id,DateTime date)
        {

        }
    }
}
