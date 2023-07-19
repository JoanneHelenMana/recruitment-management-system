using System;
using System.Collections.Generic;


namespace Recruitment_system
{
    /// <summary>
    /// This class keeps track and manages all contractors and jobs in the recruitment system.
    /// The systems allows to add, remove, search, view and filter contractors and jobs. 
    /// It also performs actions such as assigning a contractor to a job and completing a job.
    /// </summary>
    public class RecruitmentSystem
    {
        public List<Contractor> contractors = new List<Contractor>();
        public List<Job> jobs = new List<Job>();

        public RecruitmentSystem() 
        {
            // Initial sample contractors
            DateTime janeDate = new DateTime(2023, 7, 23);
            DateTime mariaDate = new DateTime(2023, 6, 28);
            DateTime helenDate = new DateTime(2023, 6, 2);
            DateTime tingDate = new DateTime(2023, 7, 11);
            DateTime katiaDate = new DateTime(2023, 6, 15);

            contractors.Add(new Contractor("Jane", "Harris", janeDate, 50.5));
            contractors.Add(new Contractor("Maria", "Guevara", mariaDate, 62, true));
            contractors.Add(new Contractor("Helen", "Polinsky", helenDate, 71.5));
            contractors.Add(new Contractor("Ting", "Hue", tingDate, 84, true));
            contractors.Add(new Contractor("Katia", "Stirling", katiaDate, 68));

            // Initial sample jobs
            DateTime fencing = new DateTime(2024, 7, 6);
            DateTime landscaping = new DateTime(2023, 8, 7);
            DateTime gardening = new DateTime(2023, 6, 27);
            DateTime carpentry = new DateTime(2023, 7, 1);
            DateTime plumbing = new DateTime(2023, 6, 18);

            jobs.Add(new Job("fencing", fencing, 6500));
            jobs.Add(new Job("landscaping", landscaping, 800, CompletionStatus.Completed));
            jobs.Add(new Job("gardening", gardening, 4800, CompletionStatus.NotCompleted, contractors.Find(x => x.FirstName.Equals("JANE"))));
            jobs.Add(new Job("carpentry", carpentry, 10050, CompletionStatus.NotCompleted, contractors.Find(x => x.FirstName.Equals("HELEN"))));
            jobs.Add(new Job("plumbing", plumbing, 2240));
        }


        /// <summary>
        /// This function adds a new contractor to the pool of existing contractors.
        /// </summary>
        /// <param name="contractor"></param>
        public void AddContractor(Contractor contractor)
        {
            contractors.Add(contractor);
        }


        /// <summary>
        /// This function removes a contractor from the pool of existing contractors.
        /// </summary>
        /// <param name="contractor"></param>
        public bool RemoveContractor(Contractor contractor)
        {
            if (contractors.Remove(contractor) == true)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// This function adds a new job to the list of existing jobs.
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(Job job)
        {
            jobs.Add(job);
        }


        /// <summary>
        /// This function removes a job from the list of existing jobs.
        /// </summary>
        /// <param name="job"></param>
        public void RemoveJob(Job job)
        {
            jobs.Remove(job);
        }


        /// <summary>
        /// This function assigns a contractor to a job - the contractor's status is changed to
        /// 'assigned' and the contractor is set as the contractor assigned for the job.
        /// </summary>
        /// <param name="contractor"></param>
        /// <param name="job"></param>
        public void AssignJob(Contractor contractor, Job job)
        {
            if (job.ContractorAssigned == null)
            {
                job.ContractorAssigned = contractor;
                contractor.IsAssigned = true;
            }
        }


        /// <summary>
        /// This function sets the job status as 'completed' and 
        /// the contractor that was assigned to it returns to the pool of available contractors.
        /// </summary>
        /// <param name="contractor"></param>
        /// <param name="job"></param>
        public void CompleteJob(Job job, Contractor contractor=null)
        {
            if (contractor != null) 
                contractor.IsAssigned = false;

            job.ContractorAssigned = null;
            job.Status = CompletionStatus.Completed;
        }


        /// <summary>
        /// This function gets a list of all the contractors in the system.
        /// </summary>
        /// <returns>A list of contractors.</returns>
        public List<Contractor> GetAllContractors()
        {
            return contractors;
        }


        /// <summary>
        /// This function gets a list of all the jobs in the system.
        /// </summary>
        /// <returns>A list of jobs.</returns>
        public List<Job> GetAllJobs()
        {
            return jobs;
        }


        /// <summary>
        /// This function gets all contractors in the system that have not been assigned a job.
        /// </summary>
        /// <returns>A list of all available contractors.</returns>
        public List<Contractor> GetAvailableContractors()
        {
            List<Contractor> availableContractors = new List<Contractor>();

            foreach (Contractor contractor in contractors) 
            {
                if (contractor.IsAssigned == false)
                    availableContractors.Add(contractor);
            }
            return availableContractors;
        }


        /// <summary>
        /// This function gets all contractors assigned to a job in the system.
        /// </summary>
        /// <returns>A list of assigned contractors.</returns>
        public List<Contractor> GetAssignedContractors()
        {
            List<Contractor> assignedContractors = new List<Contractor>();

            foreach (Contractor contractor in contractors)
            {
                if (contractor.IsAssigned == true)
                    assignedContractors.Add(contractor);
            }
            return assignedContractors;
        }


        /// <summary>
        /// This function gets all assigned jobs in the system.
        /// </summary>
        /// <returns>A list of assigned jobs.</returns>
        public List<Job> GetAssignedJobs()
        {
            List<Job> assignedJobs = new List<Job>();

            foreach (Job job in jobs)
            {
                if (job.ContractorAssigned != null)
                    assignedJobs.Add(job);
            }
            return assignedJobs;
        }


        /// <summary>
        /// This function gets all unassigned jobs in the system.
        /// </summary>
        /// <returns>A list of unassigned jobs.</returns>
        public List<Job> GetUnassignedJobs()
        {
            List<Job> unassignedJobs = new List<Job>();

            foreach (Job job in jobs)
            {
                if (job.ContractorAssigned == null)
                    unassignedJobs.Add(job);
            }
            return unassignedJobs;
        }


        /// <summary>
        /// This function gets all jobs in the system that have been completed.
        /// </summary>
        /// <returns>A list of jobs with status 'Completed'.</returns>
        public List<Job> GetCompletedJobs()
        {
            List<Job> completedJobs = new List<Job>();

            foreach (Job job in jobs)
            {
                if (job.Status == CompletionStatus.Completed)
                    completedJobs.Add(job);
            }
            return completedJobs;
        }


        /// <summary>
        /// This function gets all jobs in the system that have not been completed.
        /// </summary>
        /// <returns>A list of jobs with status 'Not Completed'.</returns>
        public List<Job> GetNotCompletedJobs()
        {
            List<Job> notCompletedJobs = new List<Job>();

            foreach (Job job in jobs)
            {
                if (job.Status == CompletionStatus.NotCompleted) 
                    notCompletedJobs.Add(job);
            }
            return notCompletedJobs;
        }


        /// <summary>
        /// This function uses the values input by the user - job title and cost range, or just cost range -
        /// to search for jobs in the system. 
        /// </summary>
        /// <param name="searchTitle"></param>
        /// <param name="costRange"></param>
        /// <returns>A list of jobs that match the search values 'title' and 'cost range'.</returns>
        public List<Job> GetJobsByCost(string searchTitle = null, string costRange = null)
        {
            List<Job> jobsInRange = new List<Job>();

            if (searchTitle == null)
            {
                foreach (Job job in jobs)
                    GetJobsInRange(job);
            }

            else if (searchTitle != null) 
            {
                foreach (Job job in jobs)
                {
                    string jobTitle = job.Title;

                    if (jobTitle == searchTitle)
                    {
                        GetJobsInRange(job);
                    }
                }
            }

            void GetJobsInRange(Job job)
            {
                int range = new int { };
                double jobCost = job.Cost;

                if (costRange == "0 - 499") range = 1;
                else if (costRange == "500 - 999") range = 2;
                else if (costRange == "1000 - 4999") range = 3;
                else if (costRange == "5000 - 9999") range = 4;
                else if (costRange == "10000 +") range = 5;

                switch (range)
                {
                    case 1:
                        {
                            if ((jobCost >= 0) && (jobCost <= 499))
                                jobsInRange.Add(job);
                            break;
                        }
                    case 2:
                        {
                            if ((jobCost >= 500) && (jobCost <= 999))
                                jobsInRange.Add(job);
                            break;
                        }
                    case 3:
                        {
                            if ((jobCost >= 1000) && (jobCost <= 4999))
                                jobsInRange.Add(job);
                            break;
                        }
                    case 4:
                        {
                            if ((jobCost >= 5000) && (jobCost <= 10000))
                                jobsInRange.Add(job);
                            break;
                        }
                    case 5:
                        {
                            if (jobCost > 10000)
                                jobsInRange.Add(job);
                            break;
                        }
                }
            }

            return jobsInRange;
        }
    }
}