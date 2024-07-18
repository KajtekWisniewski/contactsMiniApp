namespace ContactsMicroservice.DTOs
{   
    //Dto sluzace do tworzenia nowych obiektow i edycji
    public class UpsertContactMinDto
    {
        public string FirstName { get; init; } ="";
        public string LastName { get; init; } ="";
        public string Email { get; init; } ="";
        public string Password { get; init; } ="";
        public string Phone { get; init; } ="";
        public string DateOfBirth { get; init; }
    }
}
