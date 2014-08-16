namespace UniContact.Model
{
    public interface IContact
    {
        string Id { get; set; }
        string Username { get; set; }
        string FullName { get; set; }
        string ProfilePicture { get; set; }
        string ContactId { get; }
        string WebAddress { get; set; }
        string EmailAddress { get; set; }
        string PhoneNumber { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Bio { get; set; }
    }
}
