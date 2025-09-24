namespace UserMgmt.API.Core.Entities;

public class User
{
    public Guid UserID { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string PersonName { get; private set; }
    public Gender Gender { get; private set; }

    private User() { }

    public static User Create(
        string email,
        string hashedPassword,
        string personName,
        Gender gender)
    {
        return new User
        {
            UserID = Guid.NewGuid(),
            Email = email.Trim().ToLowerInvariant(),
            Password = hashedPassword,
            PersonName = personName.Trim(),
            Gender = gender
        };
    }
}
