

namespace PetShelterDemo.DataAccessLayer.Models

{
    public class Fundraiser : IIdEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DonationTarget { get; set;  }
        public string DonationCurrency { get; set; }
        public ICollection<Donation> Donations { get; set; } 

        public Fundraiser(string name, string description, int donationTarget, string donationCurrency)
        { 
            Name = name;  
            Description= description;
            DonationTarget = donationTarget;
            DonationCurrency = donationCurrency; 

        }

    }
}