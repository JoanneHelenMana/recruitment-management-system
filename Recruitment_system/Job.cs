using System;


namespace Recruitment_system
{
    /// <summary>
    /// Contains the jobs in the recruitment system.
    /// </summary>
    public class Job
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public CompletionStatus Status { get; set; }
        public Contractor ContractorAssigned { get; set; }

        public Job()
        { 

        }

        public Job(string title, DateTime date, double cost, CompletionStatus status=CompletionStatus.NotCompleted, Contractor contractorAssigned=null)
        {
            Title = title.ToUpper();
            Date = date;
            Cost = cost;
            Status = status;
            ContractorAssigned = contractorAssigned;
        }

        public static string[] costRange = new string[] {
            "0 - 499",
            "500 - 999",
            "1000 - 4999",
            "5000 - 9999",
            "10000 +"};


        public override string ToString()
        {
            return $"{Title} (${Cost}) | Start: {Date.ToShortDateString()} | Status: {Status} | Contractor assigned: {ContractorAssigned}";
        }

        public static string[] jobViews = new string[] {
            "All",
            "Unassigned",
            "Assigned",
            "Completed",
            "Not Completed",
            "Search Results"};
    }

    public enum CompletionStatus
    {
        NotCompleted,
        Completed,
    }
}
