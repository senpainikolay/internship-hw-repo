using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundariserRegistry;
    private int donationsInRon = 0;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        fundariserRegistry = new Registry<Fundraiser>(new Database());
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, int amountInRON)
    {
        donorRegistry.Register(donor);
        donationsInRon += amountInRON;
    }

    public int GetTotalDonationsInRON()
    {
        return donationsInRon;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }


    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundariserRegistry.Register(fundraiser);
    }
    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundariserRegistry.GetAll().Result;
    }
    public Fundraiser GetFundraiserByName(string name)
    {
        return fundariserRegistry.GetByName(name).Result;
    }

}