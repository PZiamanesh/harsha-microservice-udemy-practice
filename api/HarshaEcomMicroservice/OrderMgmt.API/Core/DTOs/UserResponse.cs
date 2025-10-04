namespace OrderMgmt.API.Core.DTOs;

public record UserResponse
{
    public Guid UserID { get; init; }
    public string Email { get; init; }
    public string PersonName { get; init; }
    public string Gender { get; init; }
}


public class UserResponseToOrderResponseMapper : Profile
{
    public UserResponseToOrderResponseMapper()
    {
        CreateMap<UserResponse, OrderResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserPersonName, opt => opt.MapFrom(src => src.PersonName));
    }
}