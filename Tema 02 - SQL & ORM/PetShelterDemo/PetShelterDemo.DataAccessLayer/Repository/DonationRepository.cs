using Microsoft.EntityFrameworkCore;
using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public class DonationRepository : BaseRepository<Donation>, IDonationRepository
{
    public DonationRepository(PetShelterContext context): base(context)
    {
    }
    public async Task<ICollection<Donation>?> GetTotalDonationForShelter()
    {
        return await _context.Donations.Where(d => d.FundraiserId == null).ToListAsync();
    }
   
}