using ContactsMicroservice.Entities;

namespace ContactsMicroservice.Repository.Contracts
{
    public interface IContactRepository
    {   
        //interfejs metod potrzebnych do komunikacji z baza danych
        Task<IEnumerable<T>> GetAll<T>(); //wszystkie rekordy z bazy 
        Task<Contact> GetById(int id); //1 rekord typu Contact po id
        Task<T?> GetById<T>(int id); //1 rekord typu Generycznego po id
        void Add(Contact contact); //Dodanie jednego kontaktu do bazy danych
        void Delete(Contact contact); //Usuniecie jednego kontaktu z bazy danych
        void Edit(Contact contact); //Edytowanie jednego kontaktu z bazy danych
        Task<bool> SafeChangesAsync(); //metoda na potwierdzenie zapisania zmian
    }
}
