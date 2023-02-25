using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public interface IDonationRepository: IBaseRepository<Donation> 
{
    Task<ICollection<Donation>?> GetTotalDonationForShelter();
    
}