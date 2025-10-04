namespace OrderMgmt.API.Core.DTOs;

public class UserResponse
{
    public required Guid UserID { get; init; }
    public required string Email { get; init; }
    public required string PersonName { get; init; }
    public required string Gender { get; init; }
}
