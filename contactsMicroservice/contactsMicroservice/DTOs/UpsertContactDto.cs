namespace ContactsMicroservice.DTOs
{
    public class UpsertContactDto
    {
        public string FirstName { get; init; } ="";
        public string LastName { get; init; } ="";
        public string Email { get; init; } ="";
        public string Password { get; init; } ="";
        public int CategoryId { get; init; }
        public int? SubcategoryId { get; init; }
        public string Phone { get; init; } ="";
        public DateTime DateOfBirth { get; init; }
    }
}
