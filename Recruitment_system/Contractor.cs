using System;


namespace Recruitment_system
{
    /// <summary>
    /// Contains the contractors in the recruitment system.
    /// </summary>
    public class Contractor
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartDate { get; set; }

        public double HourlyWage { get; set; }

        public bool IsAssigned { get; set; }

        public Contractor()
        {

        }

        public Contractor(string firstName, string lastName, DateTime startDate, double hourlyWage, bool isAssigned = false)
        {
            FirstName = firstName.ToUpper();
            LastName = lastName.ToUpper();
            StartDate = startDate;
            HourlyWage = hourlyWage;
            IsAssigned = isAssigned;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} (${HourlyWage}/h) | Start from: {StartDate.ToShortDateString()}";
        }

        public static string[] contractorViews = new string[] {
            "All",
            "Available",
            "Assigned"
        };
    }
}