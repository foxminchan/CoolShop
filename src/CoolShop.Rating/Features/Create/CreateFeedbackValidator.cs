namespace CoolShop.Rating.Features.Create;

internal sealed class CreateFeedbackValidator : AbstractValidator<CreateFeedbackRequest>
{
    public CreateFeedbackValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .InclusiveBetween(0, 5);

        RuleFor(x => x.Comment)
            .MaximumLength(DataSchemaLength.Max);
    }
}
