
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

namespace FurnitureManagement.Views.MaterialView
{
    /// <summary>
    /// Interaction logic for MaterialAddEditxaml.xaml
    /// </summary>
    public partial class MaterialAddEdit : Page
    {
        List<Material> listMaterial;
        public MaterialAddEdit()
        {
            InitializeComponent();
            listMaterial = MaterialService.getMaterialModels();
            listMaterial.ForEach(x => x.Quantity = 0);
            updateGrid();
        }


        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listMaterial;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (Input_IndentNo.Text == "" || Input_Date.SelectedDate == null  || listMaterial.Where(x=> x.Quantity != 0).ToList().Count == 0 )
            {
                MessageBox.Show("Please Enter Indent No ,  Date and Quantity");
                return;
            }

            var Indent = new Indent();
            Indent.IndentNo = Input_IndentNo.Text;
            Indent.Date = Input_Date.SelectedDate;
            IndentService.addIndent(Indent, listMaterial.Where(x => x.Quantity > 0).ToList());

            MessageBox.Show("Indent Added Successfuly");
            Input_IndentNo.Text = "";
            Input_Date.SelectedDate = null;

        }
    }
}
