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

namespace FurnitureManagement.Views.Mail
{
    /// <summary>
    /// Interaction logic for SearchMail.xaml
    /// </summary>
    public partial class SearchMail : Window
    {
        List<MailDetail> listMailDetail;
        int mailType;
        DateTime? toDate;
       
        DateTime? fromDate;
        int type;
        List<TypeClass> listType;
        public SearchMail()
        {
            InitializeComponent();
            BindTypeCombo();
            BindCombo();
            var obj = dtp_fromDate;
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            BindDataGrid();
        }

        private void cmb_MailType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mailType = (int)cmb_MailType.SelectedValue;
             toDate = null;
            fromDate = null;
            dtp_toDate.SelectedDate = new DateTime(); ;
            dtp_fromDate.SelectedDate = new DateTime();
            cmb_Replied.SelectedValue = 0;
           
             type=0;
            BindDataGrid();

        }

        private void btn_scannedPath(object sender, RoutedEventArgs e)
        {
            var obj = (MailDetail)dataGrid.SelectedItem;
            if (obj.ScannedCopy != null)
            {
                ImageViewer img = new ImageViewer(obj.ScannedCopy);
                img.Show();
            }
            
        }

        private void cmb_Replied_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            type = (int)cmb_Replied.SelectedValue;
            BindDataGrid();
        }

        private void BindCombo()
        {
           List<MailType>list=  MailTypeService.getMailTypes();
            MailType m=new MailType();
            m.Id=0;
            m.Type="All Types";
            list.Add(m);

            cmb_MailType.ItemsSource = list;
        }

        private void dtp_selectedDate_changed(object sender, SelectionChangedEventArgs e)
        {
            if (dtp_toDate.SelectedDate.ToString() != "1/1/0001 12:00:00 AM")
            {
                toDate = (DateTime)dtp_toDate.SelectedDate;
                BindDataGrid();
            }
            else
            {
                dtp_toDate.DisplayDate = DateTime.Now;
                toDate = null;
            }
        }

        private void dtp_fromDate_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtp_fromDate.SelectedDate.ToString() != "1/1/0001 12:00:00 AM")
            {
                fromDate = (DateTime)dtp_fromDate.SelectedDate;
                BindDataGrid();
            }
            else
            {
                dtp_fromDate.DisplayDate = DateTime.Now;
                fromDate = null;
            }
        }

         private void BindDataGrid()
        {

            listMailDetail= MailDetailService.filterMails(mailType,toDate,fromDate,type);

            dataGrid.ItemsSource = listMailDetail;
        }

        private void BindTypeCombo()
        {
            listType = new List<TypeClass>();
            TypeClass t1 = new TypeClass()
            {
                Id = 1,
                Type = "Replied"
            };
            TypeClass t2 = new TypeClass()
            {
                Id = 2,
                Type = "Pending Replied"
            };
            listType.Add(t1);

            listType.Add(t2);

            cmb_Replied.ItemsSource = listType;


        }

    }

    public class TypeClass
    {

        public int Id
        {
            get;
            set;
        }

        public String Type
        {
            get;
            set;
        }
    }
}
