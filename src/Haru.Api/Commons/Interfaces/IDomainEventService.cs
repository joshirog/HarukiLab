namespace Haru.Api.Commons.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}