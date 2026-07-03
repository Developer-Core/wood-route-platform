using DeveloperCore.WoodRoute.Platform.Shared.Application.Internal.EventHandlers;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Unit of work for the application.
/// </summary>
/// <remarks>
///     This class saves changes to the database context and then dispatches the domain events
///     raised by the tracked aggregate roots. Events are collected and cleared before the events
///     are dispatched, so handlers that persist through this same unit of work do not re-collect
///     and re-dispatch the same events.
/// </remarks>
/// <param name="context">
///     The database context for the application.
/// </param>
/// <param name="dispatcher">
///     The dispatcher that routes domain events to their handlers.
/// </param>
public class UnitOfWork(AppDbContext context, IDomainEventDispatcher dispatcher) : IUnitOfWork
{
    // inheritedDoc
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        // Collect the aggregates that raised events before persisting so we can dispatch afterwards.
        var aggregatesWithEvents = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .Where(aggregate => aggregate.DomainEvents.Count > 0)
            .ToList();

        var domainEvents = aggregatesWithEvents
            .SelectMany(aggregate => aggregate.DomainEvents)
            .ToList();

        // Clear before dispatching so a handler saving through this unit of work does not
        // re-collect and re-dispatch the very events currently being handled.
        foreach (var aggregate in aggregatesWithEvents)
            aggregate.ClearDomainEvents();

        await context.SaveChangesAsync(cancellationToken);

        await dispatcher.DispatchAsync(domainEvents, cancellationToken);
    }
}
