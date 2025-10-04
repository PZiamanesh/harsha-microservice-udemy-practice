namespace UserMgmt.API.Core.Features;

public class LoginUserHandler : IRequestHandler<LoginRequest, ErrorOr<AuthenticationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public LoginUserHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, request.Password);

        if (user is null)
        {
            return Error.NotFound(description: "User with provided credentials was not found.");
        }

        var authRespoonse = _mapper.Map<AuthenticationResponse>(user) with
        {
            Token = "tok",
            Success = true,
        };

        return authRespoonse;
    }
}
