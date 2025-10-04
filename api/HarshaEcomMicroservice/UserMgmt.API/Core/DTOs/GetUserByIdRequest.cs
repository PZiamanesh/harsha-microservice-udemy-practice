namespace UserMgmt.API.Core.DTOs;

public struct GetUserByIdRequest : IRequest<ErrorOr<UserResponse>>
{
    public Guid UserID { get; set; }
}