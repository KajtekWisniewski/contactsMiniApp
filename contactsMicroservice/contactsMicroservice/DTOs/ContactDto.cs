namespace ContactsMicroservice.DTOs
{
    public class ContactDto
    {

            public int Id { get; init; }
            public string FirstName { get; init; } ="";
            public string LastName { get; init; } ="";
            public string Email { get; init; } ="";
            public string Phone { get; init; } ="";
            public DateTime DateOfBirth { get; init; }
            public string Category { get; init; } ="";
            public string Subcategory { get; init; } ="";
    }
}
