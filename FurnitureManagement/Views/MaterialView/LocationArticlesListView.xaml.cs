﻿using FurnitureManagement.Helper;
using FurnitureManagement.Service;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ItemMaterial.xaml
    /// </summary>
    public partial class LocationArticlesListView : Page
    {
        int natureOfWorkIndex = -1;
        List<Item> itemList;
        IEnumerable<ArticleQuantity> list;
        public LocationArticlesListView( int locationId ,int  natureOfWorkIndex)
        {
            this.natureOfWorkIndex = natureOfWorkIndex;
            InitializeComponent();

            if ( natureOfWorkIndex == 0)
                itemList = ItemService.getItemsByLocation(locationId, NatureOfWork.Upholstery.combineIds);
            else if ( natureOfWorkIndex == 2)
                itemList = ItemService.getItemsByLocation(locationId, NatureOfWork.ConversionofCotNawarintoHardBed.combineIds);
            else if (natureOfWorkIndex == 3)
                itemList = ItemService.getItemsByLocation(locationId, NatureOfWork.ReplacementofTops.combineIds);
            else
                itemList = ItemService.getItemsByLocation(locationId);

            list =   itemList
            .GroupBy(x => x.JobItem.Article)
            .Select(x =>
                new ArticleQuantity
                {
                    Article = x.Key,
                    Quantity = x.Count()
                });




            updateGrid();
        }

        void updateGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = list;
        }

        private void ItemDetails_Click(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 )
            {
                int index = 0;
                foreach (var a in list)
                {

                    if (index == dataGrid.SelectedIndex)
                    {
                        var listToSent = itemList.Where(x => x.JobItem.Article.Article_Id == a.Article.Article_Id).ToList();
                        ItemMaterial next = new ItemMaterial(listToSent);
                        Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
                        frame.Content = next;
                        break;

                    }

                    index++;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Application.Current.MainWindow.FindName("Frame") as Frame;
            frame.NavigationService.GoBack();

        }
    }

    class ArticleQuantity
    {
        public Article Article { get; set; }
        public decimal Quantity { get; set; }
    }
}
