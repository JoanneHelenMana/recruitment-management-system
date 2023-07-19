using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Recruitment_system;
using System.Collections.Generic;


namespace UnitTest_RecruitmentSystem
{
    [TestClass]
    public class TestingClass
    {
        RecruitmentSystem recruitment;

        [TestInitialize]
        public void Initialise()
        {
            recruitment = new RecruitmentSystem();
        }


        [TestMethod]
        public void AddContractor_NullContractorNotAdded()
        {
            Contractor contractor = new Contractor();
            List<Contractor> contractors = new List<Contractor>();

            recruitment.AddContractor(contractor);

            Assert.IsFalse(contractors.Contains(contractor));     
        }


        [TestMethod]
        public void AddContractor_ContractorListCountIsThree()
        {
            List<Contractor> contractors = new List<Contractor>();
            Contractor contractor1 = new Contractor("Jenna", "Smith", new DateTime(2023, 04, 12), 70);
            Contractor contractor2 = new Contractor("Marion", "Chang", new DateTime(2023, 07, 23), 76);
            Contractor contractor3 = new Contractor("Anastasia", "Tresor", new DateTime(2023, 08, 01), 80);

            contractors.Add(contractor1);
            contractors.Add(contractor2);
            contractors.Add(contractor3);

            Assert.IsTrue(contractors.Count == 3);
        }


        [TestMethod]
        public void AddContractor_AllContractorsAreOfTypeContractor()
        {
            List<Contractor> contractors = recruitment.GetAllContractors();
            Contractor contractor1 = new Contractor("Jenna", "Smith", new DateTime(2023, 04, 12), 70);
            Contractor contractor2 = new Contractor("Marion", "Chang", new DateTime(2023, 07, 23), 76);
            Contractor contractor3 = new Contractor("Anastasia", "Tresor", new DateTime(2023, 08, 01), 80);

            recruitment.AddContractor(contractor1);
            recruitment.AddContractor(contractor2);
            recruitment.AddContractor(contractor3);

            CollectionAssert.AllItemsAreInstancesOfType(contractors, typeof(Contractor));
        }


        /// <summary>
        /// Contractor 'Jane Harris' is the first Contractor initialised in the Recruitment System (Index [0]).
        /// </summary>
        [TestMethod]
        public void RemoveContractor_JaneHarrisRemoved()
        {
            List<Contractor> contractors = recruitment.GetAllContractors();
            Contractor janeHarris = contractors[0];

            recruitment.RemoveContractor(janeHarris);

            CollectionAssert.DoesNotContain(contractors, janeHarris);
        }


        [TestMethod]
        public void RemoveContractor_RemoveContractorNotInList()
        {
            Contractor newContractor = new Contractor("Karen", "Hunt", new DateTime(2023, 10, 22), 66); // contractor is not added to list.
            List<Contractor> contractorsBefore = recruitment.GetAllContractors();

            recruitment.RemoveContractor(newContractor);
            List<Contractor> contractorsAfter = recruitment.GetAllContractors();

            CollectionAssert.AreEquivalent(contractorsBefore, contractorsAfter);
        }


        [TestMethod]
        public void RemoveContractor_ContractorsListIsEmpty_ContractorNotRemoved()
        {
            List<Contractor> contractors = new List<Contractor>();
            Contractor contractor = new Contractor("Karen", "Hunt", new DateTime(2023, 10, 22), 66);

            recruitment.RemoveContractor(contractor);

            Assert.IsFalse(recruitment.RemoveContractor(contractor));
        }


        [TestMethod]
        public void AddJob_JobListIncreasedByOne()
        {
            List<Job> jobs = recruitment.GetAllJobs();
            int count = jobs.Count;
            Job newJob = new Job("Chopping wood", new DateTime(2023, 12, 03), 4000);

            recruitment.AddJob(newJob);
            int newCount = jobs.Count;

            Assert.IsTrue(newCount == ++count);
        }


        [TestMethod]
        public void AddJob_AllElementsInListAreOfJobType()
        {
            List<Job> jobs = recruitment.GetAllJobs();
            Job newJob = new Job("Chopping wood", new DateTime(2023, 12, 03), 4000);
            
            recruitment.AddJob(newJob);

            CollectionAssert.AllItemsAreInstancesOfType(jobs, typeof(Job));
        }


        [TestMethod]
        public void AddJob_ListContainsNewJob()
        {
            List<Job> jobs = recruitment.GetAllJobs();
            Job newJob = new Job("Chopping wood", new DateTime(2023, 12, 03), 4000);
            
            recruitment.AddJob(newJob);

            CollectionAssert.Contains(jobs, newJob);
        }


        [TestMethod]
        public void AssignJob_JobIsAlreadyAssignedAContractor()
        {
            Contractor contractor = new Contractor("Karen", "Hunt", new DateTime(2023, 10, 22), 66);
            Job job = new Job("Chopping wood", new DateTime(2023, 12, 03), 4000, CompletionStatus.NotCompleted, 
                new Contractor("Jenna", "Smith", new DateTime(2023, 04, 12), 70));

            recruitment.AssignJob(contractor, job);

            Assert.IsTrue(job.ContractorAssigned != contractor);
        }


        [TestMethod]
        public void GetAllContractors_ListNotEmpty()
        {
            List<Contractor> contractors = recruitment.GetAllContractors();

            Assert.IsTrue(contractors.Count != 0);
        }


        [TestMethod]
        public void GetAllContractors_AllElementsAreOfTypeContractor()
        {
            List<Contractor> contractors = recruitment.GetAllContractors();

            CollectionAssert.AllItemsAreInstancesOfType(contractors, typeof(Contractor));
        }


        [TestMethod]
        public void GetAllContractors_InitialiserIsNotNull()
        {
            List<Contractor> contractors = recruitment.GetAllContractors();

            CollectionAssert.AllItemsAreNotNull(contractors);
        }


        [TestMethod]
        public void GetAvailableContractors_ContractorIsAssigned()
        {
            Contractor assignedContractor = new Contractor("Karen", "Hunt", new DateTime(2023, 10, 22), 66, true);
            recruitment.AddContractor(assignedContractor);
            List<Contractor> availableContractors = recruitment.GetAvailableContractors();

            CollectionAssert.DoesNotContain(availableContractors, assignedContractor);
        }


        [TestMethod]
        public void GetAssignedContractors_AvailableContractorIsNotReturned()
        {
            Contractor availableContractor = new Contractor("Karen", "Hunt", new DateTime(2023, 10, 22), 66);
            recruitment.AddContractor(availableContractor);
            List<Contractor> assignedContractors = recruitment.GetAssignedContractors();

            CollectionAssert.DoesNotContain(assignedContractors, availableContractor);
        }


        [TestMethod]
        public void GetAssignedContractors_AvailableContractorsInListIs0()
        {
            List<Contractor> assignedContractors = recruitment.GetAssignedContractors();
            int availableContractors = 0;

            foreach (Contractor contractor in assignedContractors)
            {
                if (contractor.IsAssigned == false) availableContractors++;
            }

            Assert.IsTrue(availableContractors == 0);
        }


        [TestMethod]
        public void GetJobsByCost_OneMatch()
        {
            string title = "FENCING";
            string range = "5000 - 9999";

            List<Job> results = recruitment.GetJobsByCost(title, range);

            Assert.IsTrue(results.Count == 1);
        }


        [TestMethod]
        public void GetJobsByCost_NoMatch()
        {
            string title = "FENCING";
            string range = "10000 +";

            List<Job> results = recruitment.GetJobsByCost(title, range);

            Assert.IsTrue(results.Count == 0);
        }


        [TestMethod]
        public void GetJobsByCost_NoTitleGiven_TwoMatches()
        {
            string range = "1000 - 4999";

            List<Job> results = recruitment.GetJobsByCost(null, range);

            Assert.IsTrue(results.Count == 2);
        }
    }
}
