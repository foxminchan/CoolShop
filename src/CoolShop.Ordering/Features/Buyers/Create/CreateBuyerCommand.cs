using CoolShop.Ordering.Domain.BuyerAggregate;

namespace CoolShop.Ordering.Features.Buyers.Create;

public sealed record CreateBuyerCommand(
    string? PhoneNumber,
    string? Email,
    string? Street,
    string? City,
    string? Province) : ICommand<Result<Guid>>;

public sealed class CreateBuyerHandler(IRepository<Buyer> repository, IIdentityService identityService)
    : ICommandHandler<CreateBuyerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
    {
        var buyerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(buyerId);

        var fullName = identityService.GetFullName();

        Guard.Against.Null(fullName);

        var email = request.Email ?? identityService.GetEmail();

        var buyer = new Buyer(
            Guid.Parse(buyerId),
            fullName,
            request.PhoneNumber,
            email, request.Street,
            request.City,
            request.Province);

        var result = await repository.AddAsync(buyer, cancellationToken);

        return result.Id;
    }
}
