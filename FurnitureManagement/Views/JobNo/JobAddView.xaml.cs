using FurnitureManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FurnitureManagement.Views.JobNo
{
    /// <summary>
    /// Interaction logic for JobAddView.xaml
    /// </summary>
    public partial class JobAddView : Page
    {
        List<Job> listOfJobs;
        public JobAddView()
        {
            InitializeComponent();
            listOfJobs = JobService.getJobs();

           // Input_FinancialYear.for
            refreshGrid();
            BindCombo();


        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {

            string validate = Validate();
            if (validate == null)
            {
                var newAddedJob = new Job()
                {
                    AmountApproval = Convert.ToInt32(Input_AmountApproval.Text),
                    AmountContract = Convert.ToInt32(Input_AmountContract.Text),
                    CompletionDate = Input_CompletionDate.SelectedDate,
                    ContractorName = Input_ContractorName.Text,
                    FinancialYear = Input_FinancialYear.SelectedDate,
                    CANo = Input_CANo.Text,
                    JobDescription = Input_Description.Text,
                    JobNo = Input_JobNo.Text,
                    Category = (int)cmb_Category.SelectedValue,
                };

                var added = JobService.addJob(newAddedJob);
                int id = (int)added.Category;
                added.CATEGORYDESC = JobService.CategoryDescription(id);
                listOfJobs.Add(added);
                refreshGrid();
            }
            else
            {
                MessageBox.Show(validate, "Input DATA", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        void refreshGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listOfJobs;
        }




        private void BindCombo()
        {
            List<Category> categories = JobService.getCategories();
            cmb_Category.ItemsSource = categories;
        }

        private void Input_FinancialYear_CalendarOpened(object sender, RoutedEventArgs e)
        {
            // Finding the calendar that is child of stadart WPF DatePicker
            DatePicker datepicker = (DatePicker)sender;
            Popup popup = (Popup)datepicker.Template.FindName("PART_Popup", datepicker);
            System.Windows.Controls.Calendar cal = (System.Windows.Controls.Calendar)popup.Child;
            cal.DisplayMode = System.Windows.Controls.CalendarMode.Decade;

        }

        private string Validate()
        {
            string message = null;

            if (Input_JobNo.Text == "")
                message = message + "Input JobNo :\n";
            if (Input_CANo.Text == "")
                message = message + "Input CANNo :\n";
            if (Input_ContractorName.Text == "")
                message = message + "Input Contractor :\n";
            if (Input_AmountApproval.Text == "")
                message = message + "Input Amount Approval :\n";

            if (Input_FinancialYear.SelectedDate == null)
                message = message + "Input Financial Year :\n";
            if (Input_Description.Text == "")
                message = message + "Input Description :\n";
            if (Input_AmountContract.Text == "")
                message = message + "Input Amount Contract :\n";
            if (Input_CompletionDate.SelectedDate == null)
                message = message + "Input Complettion Date :\n";
            if ((int)cmb_Category.SelectedIndex < 0)
                message = message + "Select Category :\n";


            return message;
        }

        private void DeleteJob_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex < listOfJobs.Count)
            {

                int selectedId = listOfJobs[dataGrid.SelectedIndex].Id;
                //if (LocationService.isAssigned(selectedId))
                //{
                //    MessageBox.Show("INVALID OPERATION : Please Unassign it , to make this operation permissable.");
                //    return;
                //}

                JobService.deleteJob(selectedId);
                listOfJobs.Remove(listOfJobs[dataGrid.SelectedIndex]);
                MessageBox.Show("The Following Job is successfully Deleted", "JOB Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                refreshGrid();
            }
        }

        private void ShowItems_Click(object sender, RoutedEventArgs e)
        {
            Job job = ((Job)dataGrid.SelectedItem);
            ShowJobItems showJI = new ShowJobItems(job.Id);
            showJI.Show();
        }
    }
}
