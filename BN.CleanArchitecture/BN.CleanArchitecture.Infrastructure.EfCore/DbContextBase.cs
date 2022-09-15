using System.Reflection;
using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Core.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BN.CleanArchitecture.Infrastructure.EfCore;

public class DbContextBase : DbContext
{
    private readonly IMediator? _mediator;

    public DbContextBase()
    {

    }

    public DbContextBase(DbContextOptions options) : base(options)
    {
    }

    public DbContextBase(DbContextOptions options, IMediator? mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_mediator == null)
        {
            return result;
        }

        // dispatch events only if save was successful
        AggregateRoot[]? entitiesWithEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        foreach (AggregateRoot? entity in entitiesWithEvents)
        {
            DomainEvent[] events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (DomainEvent domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}