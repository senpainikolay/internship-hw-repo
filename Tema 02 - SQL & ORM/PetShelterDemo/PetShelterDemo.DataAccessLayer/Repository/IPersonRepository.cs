using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public interface IPersonRepository: IBaseRepository<Person>
{
    Task<Person?> GetByIdNumber(string idNumber);
    Task<Person> GetPersonById(int id); 
}