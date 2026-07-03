using DeveloperCore.WoodRoute.Platform.Shared.Application.Internal.EventHandlers;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.EventHandlers;

/// <summary>
///     Lightweight reflection-based implementation of <see cref="IDomainEventDispatcher" />.
/// </summary>
/// <remarks>
///     For each event the dispatcher closes <see cref="IDomainEventHandler{TEvent}" /> over the
///     event's concrete runtime type, resolves every matching handler from the container, and
///     invokes them sequentially. This keeps the dispatch synchronous and in-process (no outbox).
/// </remarks>
/// <param name="serviceProvider">
///     The service provider used to resolve the registered domain event handlers.
/// </param>
public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    /// <inheritdoc />
    public async Task DispatchAsync(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handleMethod = handlerType.GetMethod(nameof(IDomainEventHandler<IEvent>.HandleAsync))!;

            foreach (var handler in serviceProvider.GetServices(handlerType))
            {
                if (handler is null) continue;
                await (Task)handleMethod.Invoke(handler, [domainEvent, cancellationToken])!;
            }
        }
    }
}
