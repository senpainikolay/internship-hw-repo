using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Repository
{
    public interface IBaseRepository<T> where T : IIdEntity
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);

        Task<IReadOnlyList<T>> Find(Func<T, bool> filter);

        Task Update(T entity);
    }
}