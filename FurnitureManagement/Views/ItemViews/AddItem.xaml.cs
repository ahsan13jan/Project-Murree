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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FurnitureManagement.Views.ItemViews
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Page
    {
        List<Job> jobList;
        List<Article> articles;


        List<Article> filteredArticles
        {
            get
            {
                if (CB_Job.SelectedIndex != -1)
                    return articles.Where(x => x.Category1.Category_Id == jobList[CB_Job.SelectedIndex].Category1.Category_Id).ToList();
                else
                    return new List<Article>();
            }
        }

        public AddItem()
        {
            InitializeComponent();
            setUp();
        }

        void setUp()
        {
            jobList = JobService.getJobs();
            CB_Job.ItemsSource = null;
            CB_Job.ItemsSource = jobList;
            articles = ArticleService.getArticles();

        }

        int itemCounter = 0;

        void addNewEntry()
        {
            itemCounter++;
            int margin = 5;
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(10, 10, 0, 0);
            sp.Height = 35;
            sp.Name = "sp" + itemCounter;
            RegisterName("sp" + itemCounter, sp);
            sp.Orientation = Orientation.Horizontal;
            //
            ComboBox article = new ComboBox();
            article.Width = 100;
            
            article.Margin = new Thickness(margin);
            article.ItemsSource = filteredArticles;
            article.SelectedValue = "Article_Id";
            article.DisplayMemberPath = "Article_DESC";
            article.SelectionChanged += cmbArticle_SelectionChange;
            RegisterName("Item0_" + itemCounter, article);
            article.Name = "Item0_" + itemCounter;
            sp.Children.Add(article);
            //
            TextBox quantity = new TextBox();
            quantity.Width = 110;
            quantity.Margin = new Thickness(margin);
            quantity.Name = "Item1_" + itemCounter;
            RegisterName("Item1_" + itemCounter, quantity);
            sp.Children.Add(quantity);

            Style style = Application.Current.FindResource("RoundedTextBox") as Style;
            quantity.Style = style;
            
            //
            Label rate = new Label();
            rate.Width = 100;
            rate.Margin = new Thickness(margin);
            rate.Name = "Item2_" + itemCounter;
            rate.FontSize = 9;
            RegisterName("Item2_" + itemCounter, rate);
            sp.Children.Add(rate);
            //
            Label accUnit = new Label();
            accUnit.Width = 100;
            accUnit.FontSize = 9;
            accUnit.Margin = new Thickness(margin);
            accUnit.Name = "Item3_" + itemCounter;
            RegisterName("Item3_" + itemCounter, accUnit);
            sp.Children.Add(accUnit);
            //

            ItemsStackPanel.Children.Add(sp);

        }

        private void addEntry_Click(object sender, RoutedEventArgs e)
        {
            addNewEntry();
        }
        private void lessEntry_Click(object sender, RoutedEventArgs e)
        {

            if (itemCounter > 0 )
            { 
                var sp = ((StackPanel)FindName("sp" + itemCounter));
                var articleCB = ((ComboBox)FindName("Item0_" + itemCounter));

                sp.Children.Remove(articleCB);
                UnregisterName("Item0_" + itemCounter);

                var quantity = ((TextBox)FindName("Item1_" + itemCounter));
                sp.Children.Remove(quantity);
                UnregisterName("Item1_" + itemCounter);

                var rate  =  ((Label)FindName("Item2_" + itemCounter));
                sp.Children.Remove(rate);
                UnregisterName("Item2_" + itemCounter);

                var acccUnit =  ((Label)FindName("Item3_" + itemCounter));
                sp.Children.Remove(acccUnit);
                UnregisterName("Item3_" + itemCounter);

                ItemsStackPanel.Children.Remove(sp);
                UnregisterName("sp" + itemCounter);

                itemCounter--;
            }

        }
        List<JobItem> getItemsFromUI()
        {
            var jobItemsList = new List<JobItem>();
            for (int a = 0; a <= itemCounter; a++)
            {
                var jobItem = new JobItem();

                var selectedIndex =   ((ComboBox)FindName("Item0_" + a)).SelectedIndex   ;

                jobItem.ArticleId = filteredArticles[selectedIndex].Article_Id;
                jobItem.Quantity = Convert.ToInt32(((TextBox)FindName("Item1_" + a)).Text );
                jobItem.JobId = jobList[CB_Job.SelectedIndex].Id;
                jobItemsList.Add(jobItem);
            }
            return jobItemsList;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (validate())
            {
                var jobItems = getItemsFromUI();

                var retJobItems = JobItemService.addJobItems(jobItems);

                string str = "";

                retJobItems.ForEach(x=> {

                    str += x.Article.Article_DESC + " :  " + x.Items.First().UIN + " - " + x.Items.Last().UIN + "\n";
                });


                str += "Saved Successfully";
                resetView();
                MessageBox.Show(str);
                resetView();
            }
            else
                MessageBox.Show("Please Enter required data");
        }

        private void CB_Job_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Job.SelectedIndex != -1)
                resetArticlesCB();
        }


        void resetArticlesCB()
        {
            for (int a = 0; a <= itemCounter; a++)
            {
               var articleCB =  ((ComboBox)FindName("Item0_" + a));
                articleCB.ItemsSource = null;
                articleCB.ItemsSource = filteredArticles;

                ((TextBox)FindName("Item1_" + a)).Text = "";
                ((Label)FindName("Item2_" + a)).Content = "";
                ((Label)FindName("Item3_" + a)).Content = "";
            }
        }

        private void cmbArticle_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var selectedIndex = Convert.ToInt32( cb.Name.Split('_')[1]);
            int selectedArticleIndex = cb.SelectedIndex;


            if (selectedIndex != -1 && selectedArticleIndex != -1)
            {
                ((Label)FindName("Item2_" + selectedIndex)).Content = filteredArticles[selectedArticleIndex].Rate.ToString();
                ((Label)FindName("Item3_" + selectedIndex)).Content = filteredArticles[selectedArticleIndex].Acc_Unit.ToString();
            }
        }

        bool validate()
        {

            if (CB_Job.SelectedIndex == -1)
                return false;

            for (int a = 0; a <= itemCounter; a++)
            {
                int q; 
                if (((ComboBox)FindName("Item0_" + a)).SelectedIndex == -1 || 
                    ((TextBox)FindName("Item1_" + a)).Text == ""  ||
                    !int.TryParse(((TextBox)FindName("Item1_" + a)).Text, out q)
                    )
                    return false;

            }

            return true;
        }
        void resetView()
        {
            ((ComboBox)FindName("Item0_0")).SelectedIndex = -1;
            ((TextBox)FindName("Item1_0")).Text = "";
            ((Label)FindName("Item2_0")).Content = "";
            ((Label)FindName("Item3_0")).Content = "";

            CB_Job.SelectedIndex = -1;
            while ( itemCounter > 0)
                lessEntry_Click(null, null);
        }

    }
}
