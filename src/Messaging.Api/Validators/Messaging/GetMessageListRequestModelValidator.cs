using FluentValidation;
using Messaging.Api.Models.Messaging;

namespace Messaging.Api.Validators.Messaging
{
    public class GetMessageListRequestModelValidator : AbstractValidator<GetMessageListRequestModel>
    {
        public GetMessageListRequestModelValidator()
        {
            RuleFor(request => request.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(request => request.Size)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);

            RuleFor(request => request.ReceiverId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
