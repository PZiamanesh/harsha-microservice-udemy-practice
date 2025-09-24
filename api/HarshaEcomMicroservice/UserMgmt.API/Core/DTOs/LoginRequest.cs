using AutoMapper;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace UserMgmt.API.Core.Dto;

public record LoginRequest : IRequest<ErrorOr<AuthenticationResponse>>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}