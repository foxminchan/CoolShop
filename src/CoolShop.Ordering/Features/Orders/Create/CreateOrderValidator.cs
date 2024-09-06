namespace CoolShop.Ordering.Features.Orders.Create;

internal sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.PaymentMethod)
            .IsInEnum();

        RuleFor(x => x.Note)
            .MaximumLength(DataSchemaLength.Max);
    }
}
