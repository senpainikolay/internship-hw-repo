using System.Diagnostics.CodeAnalysis;

namespace PetShelterDemo.DataAccessLayer.Models
{
    public class Donation : IIdEntity
    {
        public int Id { get; set; }
        public int Ammount { get; set; }
        public string Currency { get; set; }

        public int? FundraiserId { get; set; }
        public int DonorId { get; set; }

        public Person? Donor { get; set; }
        public Fundraiser? Fundraiser { get; set; }


    }

    public class CustomDonation
    {
        private readonly Donation _custom = new Donation();

        public CustomDonation WithAmmountAndCurrency(int ammount, string currency)
        {
            _custom.Ammount = ammount; 
            _custom.Currency = currency;
            return this;
        }

        public CustomDonation ForShelter(Person donor)
        {
            _custom.DonorId = donor.Id;
            _custom.Donor = donor;
            _custom.Donor.Donations.Add(_custom);
            return this;
        }
        public CustomDonation ForFundraiser(Fundraiser fundraiser, Person donor)
        {
            _custom.FundraiserId = fundraiser.Id;
            _custom.Fundraiser = fundraiser;
            _custom.Donor = donor;
            _custom.DonorId = donor.Id;
            _custom.Fundraiser.Donations.Add(_custom); 
            return this;
        }
        public Donation Build()
        { 
            return _custom;
        }
    }

}
