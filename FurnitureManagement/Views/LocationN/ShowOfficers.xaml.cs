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

namespace FurnitureManagement.Views.LocationN
{
    /// <summary>
    /// Interaction logic for ShowOfficers.xaml
    /// </summary>
    public partial class ShowOfficers : Window
    {
        List<Officer> listOfOfficers = null;
        public ShowOfficers( int id)
        {
            
            InitializeComponent();
            bindDataGrid(id);
        }

        void bindDataGrid(int id)
        {
            
            listOfOfficers = LocationService.getOfficersByLocationId(id);
            //var presentOfficer = listOfOfficers.Where(x => x.Date_Marchout == null).First();
            //listOfOfficers.Remove(presentOfficer);
            //presentOfficer.Date_Marchout = DateTime.Now;
            //listOfOfficers.Add(presentOfficer);
            dg_Officer.ItemsSource = listOfOfficers;
        }
    }
}
