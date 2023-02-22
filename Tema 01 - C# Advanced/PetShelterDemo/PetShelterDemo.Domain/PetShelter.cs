using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundariserRegistry;
    private Donations donations; 


    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        fundariserRegistry = new Registry<Fundraiser>(new Database());
        donations = new Donations();
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public   IReadOnlyList<Pet> GetAllPets() 
    {
   
            return petRegistry.GetAll().Result; // Actually blocks thread until the result is available. 
        
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, int ammount, string currency)
    {
        donorRegistry.Register(donor);
        donations.RegisterDonation(ammount, currency);

    }

    public string GetTotalDonations()
    {
        return   donations.GetTotalDonations(); ;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }



    // FUNDRAISER

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
  
            fundariserRegistry.Register(fundraiser);

    }
    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    { 
       
        return  fundariserRegistry.GetAll().Result;
    }
    public Fundraiser GetFundraiserByName(string name)
    {
        return fundariserRegistry.GetByName(name).Result;
    }

}