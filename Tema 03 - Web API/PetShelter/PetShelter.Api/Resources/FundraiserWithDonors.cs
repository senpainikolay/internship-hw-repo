using System.Collections.Immutable;

namespace PetShelter.Api.Resources
{
    public class FundraiserWithDonors : Fundraiser {
        public string Status { get; set; }  
        public ImmutableArray<Person> Donors { get; set; } 

        public decimal CurrentAmount { get; set; }
        public FundraiserWithDonors(string name, DateTime dueDate, decimal goalAmount, ICollection<Person> donors, string status, decimal currentAmount)
        {
            Name = name;
            DueDate = dueDate;
            GoalAmount = goalAmount;
            Donors = donors.ToImmutableArray();
            Status = status;
            CurrentAmount = currentAmount;
        }
    }

}
