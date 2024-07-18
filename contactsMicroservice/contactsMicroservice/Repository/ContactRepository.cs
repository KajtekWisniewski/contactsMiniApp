
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AuthService.Data;
using AuthService.DTOs;
using AuthService.Entities;
using AuthService.Repository.Contracts;

namespace AuthService.Repository
{
    public class ContactRepository(ApplicationDbContext context, IMapper mapper) : IContactRepository
    {
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
                .Where(p => p.Id == id)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SafeChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }


    }
}
