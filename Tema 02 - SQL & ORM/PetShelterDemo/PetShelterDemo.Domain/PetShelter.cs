
// using PetShelterDemo.DAL;
using PetShelterDemo.DataAccessLayer;
using PetShelterDemo.DataAccessLayer.Repository;
using PetShelterDemo.DataAccessLayer.Models;
using PetShelterDemo.DataAccessLayer.Helper;
using System.Collections;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly PetShelterContext _context = new();
    private readonly IPetRepository petRepo;
    private readonly IPersonRepository donorRepo;
    private readonly IFundraiserRepository fundraiserRepo;
    private readonly IDonationRepository donationsRepo;

    public PetShelter()
    {
        petRepo = new PetRepository(_context);
        donorRepo = new PersonRepository(_context);
        fundraiserRepo = new FundraiserRepository(_context);
        donationsRepo = new DonationRepository(_context);
    }

    public async void RegisterPet(Pet pet)
    { 
        await petRepo.Add(pet); 
    }

    public async void RegisterDonor(Person donor)
    {
        await donorRepo.Add(donor);
    }

    public  async Task<IReadOnlyList<Pet>> GetAllPets() 
    {
      return await petRepo.GetAll(); 
        
    } 



    public async Task<Pet?> GetByName(string name)
    {
        return await petRepo.GetByName(name);
    }

    public async Task<Person?> GetPersonById(string id)
    {
        return await donorRepo.GetByIdNumber(id);
    }

    public  void Donate(Person donor, Donation donation ) 
    {
        var d = donorRepo.GetByIdNumber(donor.IdNumber);
        if (d == null)
        {
            donorRepo.Add(donor);
        }
         donationsRepo.Add(donation);


    }

    public async Task <string> GetTotalDonations()
    {
        var donationList =  await donationsRepo.GetTotalDonationForShelter();
        return   DonationManager.GetTotalDonations(donationList);
    }

   

    public async Task<IReadOnlyList<Person>?> GetAllDonors()
    {
        return await donorRepo.GetAll();
    }

    public async Task<Person> GetPersonDetailsById(int id)
    {
        return await donorRepo.GetPersonById(id);
    }




    // FUNDRAISER

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
  
            fundraiserRepo.Add(fundraiser);

    }
    public async Task<IReadOnlyList<Fundraiser>?> GetAllFundraisers()
    {

        return await fundraiserRepo.GetAll();
    }

    public async Task<double> GetConvertedTotalSumOfFundraiser(string target, int id)
    {

        return await fundraiserRepo.GetConvertedTotalOutOfDonations(target,id);
    }


}