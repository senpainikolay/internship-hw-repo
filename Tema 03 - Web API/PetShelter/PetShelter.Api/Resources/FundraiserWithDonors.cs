using System.Collections.Immutable;

namespace PetShelter.Api.Resources
{
    public class FundraiserWithDonors : Fundraiser { 
        public ImmutableArray<Person> Donors { get; set; }
        public FundraiserWithDonors(string name, DateTime dueDate, decimal goalAmount, ICollection<Person> donors )
        {
            Name = name;
            DueDate = dueDate;
            GoalAmount = goalAmount;
            Donors = donors.ToImmutableArray();   
        }
    }

}
