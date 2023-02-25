using Microsoft.EntityFrameworkCore;
using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public class PetRepository : BaseRepository<Pet>, IPetRepository
{
    public PetRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<Pet?> GetByName(string name)
    {
        return await _context.Pets.FirstOrDefaultAsync(p => p.Name.Equals(name));
    }
}