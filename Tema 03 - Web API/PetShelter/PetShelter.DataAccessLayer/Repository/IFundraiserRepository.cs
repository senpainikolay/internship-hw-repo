using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IFundraiserRepository: IBaseRepository<Fundraiser>
{
    Task<ICollection<Person>?> GetDonorsForFundraiserId(int fundraiserId);
    Task DeleteFundraiser(Fundraiser entity); 

}