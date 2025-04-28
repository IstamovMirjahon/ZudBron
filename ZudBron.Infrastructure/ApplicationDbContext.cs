using Microsoft.EntityFrameworkCore;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Models.BookingModels;
using ZudBron.Domain.Models.Media;
using ZudBron.Domain.Models.NotificationModels;
using ZudBron.Domain.Models.PaymentModels;
using ZudBron.Domain.Models.Reviews;
using ZudBron.Domain.Models.SportFieldModels;

namespace ZudBron.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException("Concurrency conflict occurred while saving changes.", ex);
        }
    }

    public DbSet<SportField> SportFields { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
}
