namespace UserMgmt.API.Core.Features;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, ErrorOr<AuthenticationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        this._mapper = mapper;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existUser = await _userRepository.GetUserByEmailAsync(request.Email);

        if (existUser != null)
        {
            return Error.Conflict(code: "User.UserExists", "User with defined parameters does exists");
        }

        var user = User.Create(
            request.Email,
            request.Password,
            request.PersonName,
            request.Gender);

        await _userRepository.AddUserAsync(user);

        var authRespoonse = _mapper.Map<AuthenticationResponse>(user) with
        {
            Token = "tok",
            Success = true,
        };

        return authRespoonse;
    }
}
