using FluentValidation;
using Fruityvice.Api.Application.Commands.UpdateMetadata;

namespace Fruityvice.Api.Application.Commands.DeleteMetadata;


public class DeleteMetadataValidator : AbstractValidator<UpdateMetadataRequest>
{
    public DeleteMetadataValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleFor(x => x)
             .NotNull()
             .WithMessage("No Metadata Sent");

        When(x => x != null, () =>
        {
            RuleFor(x => x.Fruit.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("Fruit Name Required");

            RuleFor(x => x.Fruit.Metadata)
               .NotNull()
               .WithMessage("Metadata is required");


            RuleFor(x => x.Fruit.Metadata.Key)
               .NotNull()
               .NotEmpty()
               .WithMessage("Metadata Key is required");
          
        });
    }
}
