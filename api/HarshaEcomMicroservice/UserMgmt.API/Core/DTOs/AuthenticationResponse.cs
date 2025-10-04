namespace UserMgmt.API.Core.DTOs;

public record AuthenticationResponse
{
    public string UserID { get; set; }
    public string Email { get; set; }
    public string PersonName { get; set; }
    public Gender Gender { get; set; }
    public string Token { get; set; }
    public bool Success { get; set; }
}


public class AuthenticationResponseMapper : Profile
{
    public AuthenticationResponseMapper()
    {
        CreateMap<User, AuthenticationResponse>()
            .ForMember(d => d.UserID, mo => mo.MapFrom(s => s.UserID))
            .ForMember(d => d.Email, mo => mo.MapFrom(s => s.Email))
            .ForMember(d => d.PersonName, mo => mo.MapFrom(s => s.PersonName))
            .ForMember(d => d.Gender, mo => mo.MapFrom(s => s.Gender))
            .ForMember(d => d.Token, mo => mo.Ignore())
            .ForMember(d => d.Success, mo => mo.Ignore());
    }
}