
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

        // public async Task<ContactDto> GetById<ContactDto>(int id)
        // {
        //     return await context.Contacts
        //         .Include(c => c.Category)
        //         .Include(c => c.Subcategory)
        //         .Where(c => c.Id == id)
        //         .Select(c => new ContactDto
        //         {
        //             Id = c.Id,
        //             FirstName = c.FirstName,
        //             LastName = c.LastName,
        //             Email = c.Email,
        //             Phone = c.Phone,
        //             DateOfBirth = c.DateOfBirth,
        //             Category = c.Category.Name,
        //             Subcategory = c.Subcategory != null ? c.Subcategory.Name : string.Empty
        //         })
        //         .ProjectTo<T>(mapper.ConfigurationProvider)
        //         .FirstOrDefaultAsync();
        // }


        public async Task<bool> SafeChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }


    }
}
