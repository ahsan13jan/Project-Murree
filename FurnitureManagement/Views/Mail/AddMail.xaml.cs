﻿using FurnitureManagement.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
// usama change
namespace FurnitureManagement.Views.Mail
{
    //usama change
    /// <summary>
    /// Interaction logic for AddMail.xaml
    /// </summary>
    public partial class AddMail : Page
    {
        string destinationPath;
        string errorMsg;
        List<MailDetail> replyList;
        public AddMail()
        {
            InitializeComponent();
            BindCombo();
            BindDataGrid();
        }

        private void cmb_mailType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((int)cmb_mailType.SelectedIndex != -1)
            {
                if (cmb_mailType.SelectedIndex == 0)
                {
                    cmb_mailDetailType.Items.Clear();
                    cmb_mailDetailType.Items.Add("File");

                    cmb_mailDetailType.Items.Add("Reply");
                }
                else
                {
                    cmb_mailDetailType.Items.Clear();
                    cmb_mailDetailType.Items.Add("Initiated");

                    cmb_mailDetailType.Items.Add("Replied");
                }
            }
            
            cmb_mailDetailType.SelectedIndex = -1;
            txt_pageNo.Text = "";
            txt_subject.Text = "";
            txt_fileIn.Text = "";
            dtp_mail.SelectedDate = new DateTime();
            dtp_mail.DisplayDate = DateTime.Now;
            
        }

        private void BindCombo()
        {
            cmb_mailType.ItemsSource = MailTypeService.getMailTypes();
        }

        private void txt_pageNo_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);

        }

        private void btn_imgEmail_Click(object sender, RoutedEventArgs e)
        {
            string filepath;
        //browse Button
       
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;    
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();

            if (result == true)
            {
                filepath = open.FileName; // Stores Original Path in Textbox    
                ImageSource imgsource = new BitmapImage(new Uri(filepath)); // Just show The File In Image when we browse It
                // Clientimg.Source = imgsource;
                string name = System.IO.Path.GetFileName(filepath);
                destinationPath = GetDestinationPath(name, "PicFolder");
                //txt.Text = destinationPath.ToString();
                File.Copy(filepath, destinationPath, true);
            }

        }
            private static String GetDestinationPath(string filename, string foldername)
        {
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string tem = appStartPath + "\\" + foldername;
            if (!Directory.Exists(tem))
                Directory.CreateDirectory(tem);
            appStartPath = String.Format(appStartPath + "\\{0}\\" + filename, foldername);
            return appStartPath;
        }
        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if(!Validate())
            {
                MessageBox.Show(errorMsg, "ERROR", MessageBoxButton.OK);
                errorMsg = null;
                return;
            }
            MailDetail obj = new MailDetail()
            {
                Subject=txt_subject.Text,
                Date=dtp_mail.SelectedDate,
                FiledIn=txt_fileIn.Text,
                MailType=(int)cmb_mailType.SelectedValue,
                Type=cmb_mailDetailType.SelectedItem.ToString(),
                
                PageNo=Convert.ToInt32 (txt_pageNo.Text),
                ScannedCopy=destinationPath
    
            };
            
            if(obj.Type=="Replied")
            {
                int index = cmb_Replyaginst.SelectedIndex;
                if (replyList.Count==0 && cmb_Replyaginst.SelectedIndex<0)
                {
                    errorMsg = errorMsg + "Please Select Email to whcih you are Replying \n";
                    MessageBox.Show(errorMsg, "ERROR", MessageBoxButton.OK);
                    return;
                }
                obj.RepliedEmailId = replyList[cmb_Replyaginst.SelectedIndex].Id;
            }
            MailDetailService.addMailDetail(obj);
            destinationPath = "";

            cmb_mailType.SelectedIndex = -1;
            cmb_mailDetailType.SelectedIndex = -1;
            txt_pageNo.Text = "";
            txt_subject.Text = "";
            txt_fileIn.Text = "";
            dtp_mail.SelectedDate = new DateTime();
            dtp_mail.DisplayDate = DateTime.Now;
            BindDataGrid();

        }

        private void BindDataGrid()
        {

            List<MailDetail> list=MailDetailService.getAllMailDetails();
            dataGrid.ItemsSource = list;
        }

        private void btn_scannedPath(object sender, RoutedEventArgs e)
        {
            var obj = (MailDetail)dataGrid.SelectedItem;
            ImageViewer img = new ImageViewer(obj.ScannedCopy);
            img.Show();
        }

        private void cmb_mailDetailType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((int)cmb_mailDetailType.SelectedIndex != -1)
            {
                if (cmb_mailDetailType.SelectedItem.ToString() == "Replied" && (int) cmb_mailType.SelectedValue==2)
                {
                    lbl_Replyagainst.Visibility = Visibility.Visible;
                    cmb_Replyaginst.Visibility = Visibility.Visible;
                    BindReplyCombo();
                }
                else
                {
                    lbl_Replyagainst.Visibility = Visibility.Hidden;
                    cmb_Replyaginst.Visibility = Visibility.Hidden;
                }

            }
        }

        private void BindReplyCombo()
        {
            replyList= MailDetailService.getAllIncomingwithNoReplied();
            foreach (var item in replyList)
            {
                item.replyAgaist = item.Id + "-" + item.Subject;
                
            }
            cmb_Replyaginst.ItemsSource = replyList;
        }

        private void btn_SearchClick(object sender, RoutedEventArgs e)
        {
            SearchMail searchMail = new SearchMail();
            searchMail.Show();
        }


        private bool Validate()
        {
            if ((int)cmb_mailType.SelectedIndex <0)
            {
                errorMsg = errorMsg + "Please Input Value in Mail Type \n";
               
            }
            if((int)cmb_mailDetailType.SelectedIndex<0)
            {
                errorMsg = errorMsg + "Please Input Value in Mail Type Detail \n";
              
            }
            if(txt_subject.Text=="")
            {
                errorMsg = errorMsg + "Please Input Value in Subject \n";
               
            }

            if (txt_fileIn.Text == "")
            {
                errorMsg = errorMsg + "Please Input Value in File In \n";
                
            }

            if (txt_pageNo.Text == "")
            {
                errorMsg = errorMsg + "Please Input Value in PageNo \n";
                
            }
            if (errorMsg != null)
                return false;
            else
            return true;

        }
    }
}
