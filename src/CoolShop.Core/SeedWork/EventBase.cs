namespace CoolShop.Core.SeedWork;

public abstract class EventBase : INotification
{
    public DateTime DateOccurred { get; protected set; }
}
