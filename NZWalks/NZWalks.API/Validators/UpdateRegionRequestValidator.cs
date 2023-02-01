using FluentValidation;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<Models.DTO.UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Lat).LessThanOrEqualTo(90)
                .GreaterThanOrEqualTo(-90);
            RuleFor(x => x.Long).LessThanOrEqualTo(180)
                .GreaterThanOrEqualTo(-180);
        }
    }
}
