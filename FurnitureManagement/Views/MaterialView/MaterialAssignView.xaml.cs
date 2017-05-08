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

    public partial class MaterialAssignView : Page
    {

        List<Article> listArticles;
        List<MaterialItem> listMaterialItems;


        public MaterialAssignView()
        {
            InitializeComponent();
            
            setupView();
        }
        void setupView()
        {
            listArticles = ArticleService.getArticles();
            CB_Articles.ItemsSource = null;
            CB_Articles.ItemsSource = listArticles;
        }





        private void Articles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGrid();
        }



        private void Update_Click(object sender, RoutedEventArgs e)
        {
            updateGrid();
        }

        void updateGrid()
        {
            listMaterialItems = MaterialItemService.getMaterialItemFilter(Input_From.SelectedDate , Input_To.SelectedDate , (int?)CB_Articles.SelectedValue);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listMaterialItems;
        }
    }
}
