﻿using Microsoft.EntityFrameworkCore;
using ContactsMicroservice.Entities;

namespace ContactsMicroservice.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .Property(c => c.Password)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Subcategory)
                .WithMany()
                .HasForeignKey(c => c.SubcategoryId);

            modelBuilder.Entity<Category>().HasData(new List<Category>
                {
                    new Category { Id = 1, Name = "Business" },
                    new Category { Id = 2, Name = "Private" },
                    new Category { Id = 3, Name = "Other" }
                });

            modelBuilder.Entity<Subcategory>().HasData(new List<Subcategory>
                {
                    new Subcategory { Id = 1, Name = "Boss", CategoryId = 1 },
                    new Subcategory { Id = 2, Name = "Customer", CategoryId = 1 },
                    new Subcategory { Id = 3, Name = "Friend", CategoryId = 2 },
                    new Subcategory { Id = 4, Name = "Family", CategoryId = 2 }
                });
            
            modelBuilder.Entity<Contact>().HasData(new List<Contact>
                {
                    new Contact 
                    { 
                        Id = 1, 
                        FirstName = "John", 
                        LastName = "Doe", 
                        Email = "john.doe@example.com", 
                        Password = "Password1@123", 
                        CategoryId = 1, 
                        SubcategoryId = 1, 
                        Phone = "1234567890", 
                        DateOfBirth = new DateTime(1985, 1, 15) 
                    },
                    new Contact 
                    { 
                        Id = 2, 
                        FirstName = "Jane", 
                        LastName = "Smith", 
                        Email = "jane.smith@example.com", 
                        Password = "Password2@#!", 
                        CategoryId = 1, 
                        SubcategoryId = 2, 
                        Phone = "0987654321", 
                        DateOfBirth = new DateTime(1990, 2, 20) 
                    },
                    new Contact 
                    { 
                        Id = 3, 
                        FirstName = "Alice", 
                        LastName = "Johnson", 
                        Email = "alice.johnson@example.com", 
                        Password = "asFDma@4jac2@pl", 
                        CategoryId = 2, 
                        SubcategoryId = 3, 
                        Phone = "1122334455", 
                        DateOfBirth = new DateTime(1988, 3, 25) 
                    },
                    new Contact 
                    { 
                        Id = 4, 
                        FirstName = "Bob", 
                        LastName = "Brown", 
                        Email = "bob.brown@example.com", 
                        Password = "verystr0ngP455", 
                        CategoryId = 2, 
                        SubcategoryId = 4, 
                        Phone = "5566778899", 
                        DateOfBirth = new DateTime(1975, 4, 30) 
                    }
                });


            base.OnModelCreating(modelBuilder);
        }
    }
}
