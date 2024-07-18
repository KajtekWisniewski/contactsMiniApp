namespace ContactsMicroservice.Entities
{
    public class Contact
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
            public int? SubcategoryId { get; set; }
            public Subcategory Subcategory { get; set; }
            public string Phone { get; set; }
            public string DateOfBirth { get; set; }
        }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<Contact> Contacts { get; set; }
        }

        public class Subcategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
            public ICollection<Contact> Contacts { get; set; }
        }

}
