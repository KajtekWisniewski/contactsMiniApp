namespace ContactsMicroservice.DTOs
{
    public class ContactMinimalDto
    {

            public int Id { get; init; }
            public string FirstName { get; init; } ="";
            public string LastName { get; init; } ="";
            public string Email { get; init; } ="";
    }
}
