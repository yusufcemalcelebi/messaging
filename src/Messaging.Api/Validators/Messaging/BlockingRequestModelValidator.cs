using FluentValidation;
using Messaging.Api.Models.Messaging;

namespace Messaging.Api.Validators.Messaging
{
    public class BlockingRequestModelValidator : AbstractValidator<BlockingRequestModel>
    {
        public BlockingRequestModelValidator()
        {
            RuleFor(request => request.BlockedId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(request => request.IsActive)
                .NotNull();
        }
    }
}
