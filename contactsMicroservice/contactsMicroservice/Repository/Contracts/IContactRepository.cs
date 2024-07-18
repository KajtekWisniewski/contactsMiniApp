using AuthService.Entities;

namespace AuthService.Repository.Contracts
{
    public interface IContactRepository
    {
        Task<IEnumerable<T>> GetAll<T>();
        Task<Contact> GetById(int id);
        Task<T?> GetById<T>(int id);
        void Add(Contact contact);
        void Delete(Contact contact);
        void Edit(Contact contact);
        Task<bool> SafeChangesAsync();
    }
}
