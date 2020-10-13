using System;
using FluentValidation;
using Messaging.Api.Models.Authentication;

namespace Messaging.Api.Validators.Authentication
{
    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(20);

            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
