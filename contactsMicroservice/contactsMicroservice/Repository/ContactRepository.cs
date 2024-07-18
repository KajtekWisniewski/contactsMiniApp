
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ContactsMicroservice.Data;
using ContactsMicroservice.DTOs;
using ContactsMicroservice.Entities;
using ContactsMicroservice.Repository.Contracts;

namespace ContactsMicroservice.Repository
{
    public class ContactRepository(ApplicationDbContext context, IMapper mapper) : IContactRepository
    {
        //implemenetacje metod interfejsu. metody zostaly opisane w interfejsie
        public void Add(Contact contact)
        {
            context.Contacts.Add(contact);
        }
        public void Delete(Contact contact)
        {
            context.Contacts.Remove(contact);
        }
        public void Edit(Contact contact)
        {
            context.Contacts.Update(contact);
        }
        
        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await context
                .Contacts
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Contact?> GetById(int id)
        {
            return await context.Contacts.FindAsync(id);
        }
        public async Task<T?> GetById<T>(int id)
        {
           return await context
               .Contacts
               .Where(p => p.Id == id) //linq
               .ProjectTo<T>(mapper.ConfigurationProvider)
               .SingleOrDefaultAsync();
        }

        public async Task<bool> SafeChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }


    }
}
