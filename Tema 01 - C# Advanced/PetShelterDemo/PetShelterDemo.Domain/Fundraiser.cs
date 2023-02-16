

namespace PetShelterDemo.Domain;

public class Fundraiser : INamedEntity
{
    public string Name { get; }
    public string Description { get; } 
    public int DonationTarget { get; }
    public string DonationCurrency { get; }
    private List<Person> donorsList { get; set; } = new List<Person>();
    private Donations donations; 


    public Fundraiser(string title, string description, string donationCurrency, int donationTarget)
    {
        Name = title;
        Description = description;
        DonationTarget = donationTarget;
        DonationCurrency = donationCurrency; 
        donations = new Donations(); 
    }

    public void Donate(Person donor, int ammount, string currency)
    {
        donorsList.Add(donor);
        donations.RegisterDonation(ammount, currency); 
    }

    public string GetTotalDonations()
    {
        return donations.GetTotalDonations();
    }

    public List<Person> GetAllDonors()
    {
        return donorsList;
    }

    public double  GetConvertedAmmountInTargetCurrency()
    {
        return donations.ConvertDonationsToCurrency(DonationCurrency);
    }
}