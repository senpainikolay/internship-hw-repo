using Microsoft.EntityFrameworkCore;
using PetShelterDemo.DataAccessLayer.Models; 

using PetShelterDemo.DataAccessLayer.Helper;

namespace PetShelterDemo.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context): base(context)
    {
    }


    public async  Task<double> GetConvertedTotalOutOfDonations(string targetCurrency, int id ) 
    {
        var fundraiser = await GetById(id);
        var donations = await _context.Donations.Where(d => d.FundraiserId == id).ToListAsync();
        fundraiser.Donations = donations;

        // DonationManager from Helper folder
        var DonationsDict = DonationManager.ConvertDonationListToDictionary(donations);

        int[] totalAmmountArr = DonationsDict.Select(elem => elem.Value).ToArray();
        double[] currentCurrencyValues = DonationManager.ConvertTable[targetCurrency];

        var sum = 0.0;
        for (int i = 0; i < totalAmmountArr.Length; i++)
        {
            var num1 = totalAmmountArr[i];
            var num2 = currentCurrencyValues[i];
            sum += num1 * num2;
        }

        return  sum;
    }
}