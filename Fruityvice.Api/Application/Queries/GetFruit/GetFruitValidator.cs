using FluentValidation;
using Fruityvice.Api.Application.Commands.UpdateMetadata;

namespace Fruityvice.Api.Application.Queries.GetFruit
{
    public class GetFruitValidator : AbstractValidator<GetFruiteRequest>
    {
        public GetFruitValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RuleFor(x => x)
                 .NotNull()
                 .WithMessage("Empty Request Sent");

            When(x => x != null, () =>
            {
                RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty()
                   .WithMessage("Fruit Name Required");               
            });
        }
    }
}
