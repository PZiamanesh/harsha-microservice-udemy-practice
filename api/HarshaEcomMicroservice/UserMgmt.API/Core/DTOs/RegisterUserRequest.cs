using ErrorOr;
using FluentValidation;
using MediatR;
using UserMgmt.API.Core.Entities;

namespace UserMgmt.API.Core.Dto;

public record RegisterUserRequest : IRequest<ErrorOr<AuthenticationResponse>>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string PersonName { get; set; }

    public Gender Gender { get; set; }
}

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.PersonName)
            .MinimumLength(3)
            .MaximumLength(50);
    }
}