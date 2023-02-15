
using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class Fundraiser : INamedEntity
{
    public string Name { get; }
    public string Description { get; }
    public int DonationTarget { get; }
    private int totalDonations = 0;
    private List<Person> donorsList { get; set; } = new List<Person>();


    public Fundraiser(string title, string description, int donationTarget)
    {
        Name = title;
        Description = description;
        DonationTarget = donationTarget;
    }

    public void Donate(Person donor, int ammount)
    {
        donorsList.Add(donor);
        totalDonations += ammount;
    }

    public int GetTotalDonations()
    {
        return totalDonations;
    }

    public List<Person> GetAllDonors()
    {
        return donorsList;
    }
}