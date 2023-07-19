using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Recruitment_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecruitmentSystem recruitment = new RecruitmentSystem();

        public MainWindow()
        {
            InitializeComponent();
            comboboxContractorsView.ItemsSource = Contractor.contractorViews;
            comboboxJobsView.ItemsSource = Job.jobViews;
            comboboxCost.ItemsSource = Job.costRange;
        }


        /// <summary>
        /// This is called when the combobox selection for the Contractors View is changed.
        /// It filters and displays contractors according to the view option selected - All, Available, or Assigned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboboxContractorsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxContractorsView.SelectedItem.ToString() == Contractor.contractorViews[0])
                listboxContractorsView.ItemsSource = recruitment.GetAllContractors();    

            else if (comboboxContractorsView.SelectedItem.ToString() == Contractor.contractorViews[1]) 
                listboxContractorsView.ItemsSource = recruitment.GetAvailableContractors();

            else if (comboboxContractorsView.SelectedItem.ToString() == Contractor.contractorViews[2])
                listboxContractorsView.ItemsSource = recruitment.GetAssignedContractors();
        }


        /// <summary>
        /// This is called when the combobox selection for the Jobs View is changed 
        /// It filters and displays jobs according to the view option selected 
        /// - All, Assigned, Unassigned, Completed, Not Completed, or Search Results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboboxJobsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxJobsView.SelectedItem.ToString() == Job.jobViews[0])
                listboxJobsView.ItemsSource = recruitment.GetAllJobs();

            else if (comboboxJobsView.SelectedItem.ToString() == Job.jobViews[2])
                listboxJobsView.ItemsSource = recruitment.GetAssignedJobs();

            else if (comboboxJobsView.SelectedItem.ToString() == Job.jobViews[1])
                listboxJobsView.ItemsSource = recruitment.GetUnassignedJobs();

            else if (comboboxJobsView.SelectedItem.ToString() == Job.jobViews[3])
                listboxJobsView.ItemsSource = recruitment.GetCompletedJobs();

            else if (comboboxJobsView.SelectedItem.ToString() == Job.jobViews[4])
                listboxJobsView.ItemsSource = recruitment.GetNotCompletedJobs();

            else if (comboboxJobsView.SelectedItem.ToString() == Job.jobViews[5])
                listboxJobsView.ItemsSource = recruitment.GetJobsByCost();
        }


        /// <summary>
        /// This function is triggered when the 'Remove Contractor' button is clicked.
        /// It deletes the contractor selected by the user from the pool of contractors.
        /// The system displays a warning message to the user to confirm that the contractor is to be removed.
        /// Contractors assigned to a job cannot be removed from the pool of contractors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonContractorRemove_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxTextCannotRemove = "Assigned contractors cannot be removed. Please, first mark their job as 'completed' and try again.";
            MessageBoxButton buttonCannotRemove = MessageBoxButton.OK;
            string messageBoxTextWarning = "This contractor will be removed. Do you wish to continue?";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            string messageBoxTextNoSelection = "No contractor is selected.";

            Contractor selectedContractor = listboxContractorsView.SelectedItem as Contractor;

            if (selectedContractor == null)
            {
                MessageBox.Show(messageBoxTextNoSelection, "", buttonCannotRemove);
                return;
            }

            else if (selectedContractor != null)
            {
                if (selectedContractor.IsAssigned == true)
                {
                    MessageBox.Show(messageBoxTextCannotRemove, "", buttonCannotRemove);
                    return;
                }

                else if (selectedContractor.IsAssigned == false)
                {
                    if (MessageBox.Show(messageBoxTextWarning, "", buttonWarning) == MessageBoxResult.Yes)
                    {
                        recruitment.RemoveContractor(selectedContractor);
                        RefreshContractorView();
                    }
                }
            }
        }


        /// <summary>
        /// This function is triggered when the 'Remove Job' button is clicked.
        /// The function removes the job selected by the user from the list of jobs in the system.
        /// A warning is displayed to get user confirmation to delete the job.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonJobRemove_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "This job will be deleted. Do you wish to continue?";
            MessageBoxButton button = MessageBoxButton.YesNo;

            Job selectedJob = listboxJobsView.SelectedItem as Job;

            if (selectedJob != null)
            {  
                if (MessageBox.Show(messageBoxText, "", button) == MessageBoxResult.Yes)
                {
                    recruitment.RemoveJob(selectedJob);
                    RefreshJobView();
                }
            } 
        }


        /// <summary>
        /// This is triggered when the 'Add' button in the Contractors section is clicked.
        /// It retrieves the information input by user in the appropriate fields and adds a new contractor to the system.
        /// Input is handled to allow only letters for fields 'First name' and 'Last name', only numbers for field 'Hourly wage', and empty date selection is prevented.
        /// Repeated contractors with the same first name and last name are not allowed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonContractorAdd_Click(object sender, RoutedEventArgs e)
        {
            // User input validation starts

            string onlyLetters = "^[a-zA-Z]+$";
            string error = "Error";
            MessageBoxButton buttonOK = MessageBoxButton.OK;

            string firstName = textboxContractorFirstName.Text;
            if (!Regex.Match(firstName, onlyLetters).Success)
            {
                if (firstName == string.Empty)
                {
                    MessageBox.Show("First name cannot be empty.", error, buttonOK);
                    textboxContractorFirstName.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid first name.", error, buttonOK);
                    textboxContractorFirstName.Focus();
                    return;
                }
            }
            else if (firstName.Length < 2)
            {
                MessageBox.Show("First name must have at least two letters.", error, buttonOK);
                textboxContractorFirstName.Focus();
                return;
            }

            string lastName = textboxContractorLastName.Text;
            if (!Regex.Match(lastName, onlyLetters).Success)
            {
                if (lastName == string.Empty)
                {
                    MessageBox.Show("Last name cannot be empty.", error, buttonOK);
                    textboxContractorLastName.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid last name.", error, buttonOK);
                    textboxContractorLastName.Focus();
                    return;
                }
            }
            else if (lastName.Length < 2)
            {
                MessageBox.Show("Last name must have at least two letters.", error, buttonOK);
                textboxContractorLastName.Focus();
                return;
            }

            DateTime date;
            bool parsedDate = DateTime.TryParse(datepickerContractorStartDate.Text, out date);
            if (parsedDate == false)
            {
                MessageBox.Show("No start date selected.", error, buttonOK);
                datepickerContractorStartDate.Focus();
                return;
            }

            double wage;
            bool parsedWage = Double.TryParse(textboxContractorHourlyWage.Text, out wage);
            if (textboxContractorHourlyWage.Text == string.Empty)
            {
                MessageBox.Show("Wage cannot be empty.", error, buttonOK);
                textboxContractorHourlyWage.Focus();
                return;
            }
            else if (parsedWage == false)
            {
                MessageBox.Show("Invalid wage.", error, buttonOK);
                textboxContractorHourlyWage.Focus();
                return;
            }
            else
            {
                double inputWage = Convert.ToDouble(textboxContractorHourlyWage.Text);
                if (inputWage <= 0)
                {
                    MessageBox.Show("Wage out of range.", error, buttonOK);
                    textboxContractorHourlyWage.Focus();
                    return;
                }
            }

            //User input validation ends


            string messageBoxTextCannotAdd = "The contractor already exists. Please, try again.";
            MessageBoxButton buttonCannotAdd = MessageBoxButton.OK;

            Contractor newContractor = new Contractor(firstName, lastName, date, wage);
            int repeatedContractor = 0;

            if (newContractor != null)
            {
                foreach (Contractor contractor in recruitment.contractors)
                {
                    if (contractor.FirstName == newContractor.FirstName &&
                        contractor.LastName == newContractor.LastName)
                    {
                        MessageBox.Show(messageBoxTextCannotAdd, "", buttonCannotAdd);
                        repeatedContractor++;
                        break;
                    }
                }

                if (repeatedContractor == 0)
                {
                    recruitment.AddContractor(newContractor);
                    RefreshContractorView();
                    ClearContractorFields();
                }
            }
        }


        /// <summary>
        /// This is triggered when the Add button in the Jobs section is clicked.
        /// It retrieves the information input by user in the appropriate fields and 
        /// adds a new job to the system. Input is handled to only allow letters in the field 'Title',
        /// only numbers in the field 'Cost', and empty date selection is prevented.
        /// Duplicated jobs with the same title and cost are not allowed in the system.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonJobAdd_Click(object sender, RoutedEventArgs e)
        {
            // User input validation starts
            
            string onlyLetters = "^[a-zA-Z]+$";
            string error = "Error";
            MessageBoxButton buttonOK = MessageBoxButton.OK;

            string title = textboxJobsTitle.Text;
            if (!Regex.Match(title, onlyLetters).Success)
            {
                if (title == string.Empty)
                {
                    MessageBox.Show("Title cannot be empty.", error, buttonOK);
                    textboxJobsTitle.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid title.", error, buttonOK);
                    textboxJobsTitle.Focus();
                    return;
                }
            }

            DateTime date;
            bool parsedDate = DateTime.TryParse(datepickerJobsDate.Text, out date);
            if (parsedDate == false)
            {
                MessageBox.Show("No date selected.", error, buttonOK);
                datepickerJobsDate.Focus();
                return;
            }

            double cost;
            bool parsedCost = Double.TryParse(textboxJobsCost.Text, out cost);
            if (textboxJobsCost.Text == string.Empty)
            {
                MessageBox.Show("Cost cannot be empty.", error, buttonOK);
                textboxJobsCost.Focus();
                return;
            }
            else if (parsedCost == false)
            {
                MessageBox.Show("Invalid cost", error, buttonOK);
                textboxJobsCost.Focus();
                return;
            }
            else
            {
                double inputCost = Convert.ToDouble(textboxJobsCost.Text);
                if (inputCost <= 0)
                {
                    MessageBox.Show("Cost out of range.", error, buttonOK);
                    textboxContractorHourlyWage.Focus();
                    return;
                }
            }

            //User input validation ends


            string messageTextBoxCannotAdd = "The job already exists. Please, try again.";
            MessageBoxButton buttonCannotAdd = MessageBoxButton.OK;

            Job newJob = new Job(title, date, cost);
            int repeatedJob = 0;

            if (newJob != null)
            {
                foreach (Job job in recruitment.jobs)
                {
                    if (job.Title == newJob.Title && 
                        job.Cost == newJob.Cost)
                    {
                        MessageBox.Show(messageTextBoxCannotAdd, "", buttonCannotAdd);
                        repeatedJob++;
                        break;
                    }
                }
                if (repeatedJob == 0)
                {
                    recruitment.AddJob(newJob);
                    RefreshJobView();
                    ClearJobFields();
                }
            }
        }


        /// <summary>
        /// This is triggered when the 'Assign Job' button is clicked.
        /// Both a contractor and a job have to be selected for the assignment to be performed.
        /// A contractor is assigned to a job as long as they are not already assigned to a job, and the job does not already have an assigned contractor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAssignJob_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxTextJobAssigned = "The job already has a contractor assigned.";
            string messageBoxTextContractorAssigned = "The contractor is already assigned a job.";
            string messageBoxTextNoSelection = "Please, select a job and a contractor to perform assignment.";
            string messageBoxTextSuccess = "Assignment successful!";
            string messageBoxTextJobCompleted = "A completed job cannot be assigned to a contractor. Please, try again.";
            MessageBoxButton buttonOK = MessageBoxButton.OK;            

            Contractor contractor = listboxContractorsView.SelectedItem as Contractor;
            Job job = listboxJobsView.SelectedItem as Job;

            if (job.Status == CompletionStatus.NotCompleted)
            {
                if (listboxContractorsView.SelectedItem == null ||
                    listboxJobsView.SelectedItem == null)
                    MessageBox.Show(messageBoxTextNoSelection, "", buttonOK);

                else if (job.ContractorAssigned != null)
                    MessageBox.Show(messageBoxTextJobAssigned, "", buttonOK);

                else if (contractor.IsAssigned == true)
                    MessageBox.Show(messageBoxTextContractorAssigned, "", buttonOK);

                else
                {
                    recruitment.AssignJob(contractor, job);
                    MessageBox.Show(messageBoxTextSuccess, "", buttonOK);
                    comboboxContractorsView.SelectedItem = Contractor.contractorViews[1];
                    listboxContractorsView.ItemsSource = recruitment.GetAssignedContractors();
                    comboboxJobsView.SelectedItem = Job.jobViews[2];
                    listboxJobsView.ItemsSource = recruitment.GetAssignedJobs();
                }
            }

            else if (job.Status == CompletionStatus.Completed)
                MessageBox.Show(messageBoxTextJobCompleted, "", buttonOK);
        }


        /// <summary>
        /// This function is triggered when the 'Job Completed' button is clicked.
        /// The status of the job is changed to 'Completed' and, if a contractor was assigned,
        /// they return to the pool of available contractors. Only jobs with status 'Not completed' can be set to 'Completed'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonJobCompleted_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxTextJobCompleted = "The job status will change to 'completed' " +
                "and the contractor will return to the pool of available contractors. Do you wish to continue?";
            string messageBoxTextJobCompletedNoContractor = "The job status will change to 'completed'. Do you wish to continue?";
            string messageBoxTextError = "The job is already marked as completed.";
            string messageboxTextNoSelection = "No job selected.";
            MessageBoxButton buttonYesNo = MessageBoxButton.YesNo;
            MessageBoxButton buttonOK = MessageBoxButton.OK;


            Job job = listboxJobsView.SelectedItem as Job;

            if (job == null)
            {
                MessageBox.Show(messageboxTextNoSelection, "", buttonOK);
                return;
            }
            else if (job != null && job.Status == CompletionStatus.NotCompleted)
            {
                if (job.ContractorAssigned == null)
                {
                    if (MessageBox.Show(messageBoxTextJobCompletedNoContractor, "", buttonYesNo) == MessageBoxResult.Yes)
                        CompleteAndRefreshView();
                }

                else if (job.ContractorAssigned != null)
                {
                    if (MessageBox.Show(messageBoxTextJobCompleted, "", buttonYesNo) == MessageBoxResult.Yes)
                    {
                        CompleteAndRefreshView();
                        comboboxContractorsView.SelectedItem = Contractor.contractorViews[1];
                        listboxContractorsView.ItemsSource = recruitment.GetAvailableContractors();
                    }
                }
            }
            else if (job != null && job.Status == CompletionStatus.Completed)
                MessageBox.Show(messageBoxTextError, "", buttonOK);


            void CompleteAndRefreshView()
            {
                recruitment.CompleteJob(job, job.ContractorAssigned);
                comboboxJobsView.SelectedItem = Job.jobViews[3];
                listboxJobsView.ItemsSource = recruitment.GetCompletedJobs();
            }     
        }


        /// <summary>
        /// This is triggered when the 'Search' button in the 'Jobs' section is clicked.
        /// The results of the search are displayed in the jobs view. 
        /// The search feature allows to search jobs by title and cost, or by cost only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonJobSearch_Click(object sender, RoutedEventArgs e)
        {
            string title;
            if (!string.IsNullOrEmpty(textboxJobTitleSearch.Text))
                title = textboxJobTitleSearch.Text.ToUpper();
            else title = null;

            string costRange;
            if (!(comboboxCost.SelectedItem is null))
                costRange = comboboxCost.SelectedItem.ToString();
            else costRange = null;

            List<Job> jobsReturned = recruitment.GetJobsByCost(title, costRange);

            if (title != null && costRange == null)
                MessageBox.Show("Cost range not selected.", "", MessageBoxButton.OK);

            else if (!jobsReturned.Any())
                MessageBox.Show("No results found.", "", MessageBoxButton.OK);

            else
            {
                comboboxJobsView.SelectedItem = Job.jobViews[5];
                listboxJobsView.ItemsSource = jobsReturned;
            }
        }


        /// <summary>
        /// This is triggered when the Reset button in the 'Contractors' section is clicked.
        /// It sets all contractor input fields to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonContractorReset_Click(object sender, RoutedEventArgs e)
        {
            ClearContractorFields();
        }


        /// <summary>
        /// This is triggered when the Reset button in the 'Jobs' section is clicked.
        /// It sets all job input fields to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonJobReset_Click(object sender, RoutedEventArgs e)
        {
            ClearJobFields();
        }


        /// <summary>
        /// This function updates the Contractor View (listbox) to display all contractors.
        /// </summary>
        public void RefreshContractorView()
        {
            listboxContractorsView.ItemsSource = null;
            listboxContractorsView.ItemsSource = recruitment.GetAllContractors();
            comboboxContractorsView.SelectedItem = Contractor.contractorViews[0];
        }


        /// <summary>
        /// This function updates the Jobs View (listbox) to display all jobs.
        /// </summary>
        public void RefreshJobView()
        {
            listboxJobsView.ItemsSource = null;
            listboxJobsView.ItemsSource = recruitment.GetAllJobs();
            comboboxJobsView.SelectedItem = Job.jobViews[0];
        }


        /// <summary>
        /// This function resets all contractor input fields back to default.
        /// </summary>
        public void ClearContractorFields()
        {
            textboxContractorFirstName.Clear();
            textboxContractorLastName.Clear();
            datepickerContractorStartDate.SelectedDate = null;
            textboxContractorHourlyWage.Clear();
        }


        /// <summary>
        /// This function resets all job input fields back to default.
        /// </summary>
        public void ClearJobFields()
        {
            textboxJobsTitle.Clear();
            datepickerJobsDate.SelectedDate = null;
            textboxJobsCost.Clear();
        }
    }
}
