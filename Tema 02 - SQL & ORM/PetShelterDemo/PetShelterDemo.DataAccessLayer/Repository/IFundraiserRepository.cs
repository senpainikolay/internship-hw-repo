using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public interface IFundraiserRepository: IBaseRepository<Fundraiser>
{
    Task<double> GetConvertedTotalOutOfDonations(string targetCurrency, int id);

}

