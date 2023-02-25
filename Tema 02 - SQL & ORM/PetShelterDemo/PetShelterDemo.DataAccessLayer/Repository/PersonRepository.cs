using Microsoft.EntityFrameworkCore;
using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{

    public PersonRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<Person?> GetByIdNumber(string idNumber)
    {
        return await _context.Persons.SingleOrDefaultAsync(p => p.IdNumber == idNumber);
    }
    public async Task<Person> GetPersonById(int id)
    {
        return await _context.Persons.SingleOrDefaultAsync(p => p.Id == id);
    }
}