namespace ContactsMicroservice.DTOs
{   
    //Dto wysylane do wysylania niektorych informacji
    public class ContactMinimalDto
    {

            public int Id { get; init; }
            public string FirstName { get; init; } ="";
            public string LastName { get; init; } ="";
            public string Email { get; init; } ="";
    }
}
