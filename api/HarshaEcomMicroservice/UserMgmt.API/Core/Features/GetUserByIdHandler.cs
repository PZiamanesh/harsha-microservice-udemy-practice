namespace UserMgmt.API.Core.Features;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserID);

        if (user is null)
        {
            return Error.NotFound(description: $"User with id: {request.UserID} was not found.");
        }

        var userResponse = _mapper.Map<UserResponse>(user);

        return userResponse;
    }
}
