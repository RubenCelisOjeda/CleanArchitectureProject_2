namespace Application.Auth.Users
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int OrganizationId { get; set; }
    }
}
