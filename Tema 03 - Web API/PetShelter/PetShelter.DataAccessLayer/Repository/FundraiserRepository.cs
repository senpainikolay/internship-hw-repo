using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Kerberos;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{

    public FundraiserRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<ICollection<Person>?> GetDonorsForFundraiserId(int  fundraiserId)
    {
        return await _context.Donations
            .Include("Donor")
            .Where(d => d.FundraiserId == fundraiserId)
            .Select(d => d.Donor)
            .ToListAsync(); 
    }

    public async Task DeleteFundraiser(Fundraiser entity)
    {
        entity.Status = "Closed";
        await Update(entity); 
    }
}
