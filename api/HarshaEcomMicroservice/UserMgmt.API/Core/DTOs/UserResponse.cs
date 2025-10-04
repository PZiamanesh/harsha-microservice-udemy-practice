namespace UserMgmt.API.Core.DTOs;

public record UserResponse
{
    public required string UserID { get; set; }
    public required string Email { get; set; }
    public required string PersonName { get; set; }
    public Gender Gender { get; set; }
}


public class UserResponseMapper : Profile
{
    public UserResponseMapper()
    {
        CreateMap<User, UserResponse>()
            .ForMember(d => d.UserID, mo => mo.MapFrom(s => s.UserID))
            .ForMember(d => d.Email, mo => mo.MapFrom(s => s.Email))
            .ForMember(d => d.PersonName, mo => mo.MapFrom(s => s.PersonName))
            .ForMember(d => d.Gender, mo => mo.MapFrom(s => s.Gender));
    }
}