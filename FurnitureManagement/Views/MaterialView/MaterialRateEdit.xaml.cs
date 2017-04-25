﻿
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
    public partial class MaterialRateEdit : Page
    {
        List<Material> listMaterial;
        public MaterialRateEdit()
        {
            InitializeComponent();
            listMaterial = MaterialService.getMaterials();
            updateGrid();
        }


        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listMaterial;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            MaterialService.updateMaterialRates(listMaterial);

            MessageBox.Show("Rates Updated Successfuly");

        }
    }
}
