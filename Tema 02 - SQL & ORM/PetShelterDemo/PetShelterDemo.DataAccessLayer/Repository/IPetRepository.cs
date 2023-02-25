using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository;

public interface IPetRepository: IBaseRepository<Pet>
{
    Task<Pet?> GetByName(string name);

}