using FluentValidation;
using Messaging.Api.Models.Messaging;

namespace Messaging.Api.Validators.Messaging
{
    public class SendMessageRequestModelValidator : AbstractValidator<SendMessageRequestModel>
    {
        public SendMessageRequestModelValidator()
        {
            RuleFor(request => request.ReceiverId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(request => request.Text)
                .MaximumLength(1000);
        }
    }
}
