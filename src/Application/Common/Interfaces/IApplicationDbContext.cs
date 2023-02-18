using OkoulChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OkoulChallenge.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Quote> Quotes { get; }
    DbSet<Author> Authors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
