using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Models.BookingModels;
using ZudBron.Domain.Models.FieldCategories;
using ZudBron.Domain.Models.Media;
using ZudBron.Domain.Models.NotificationModels;
using ZudBron.Domain.Models.PaymentModels;
using ZudBron.Domain.Models.Reviews;
using ZudBron.Domain.Models.SportFieldModels;
using ZudBron.Domain.Models.UserModel;

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
    public DbSet<User> Users { get; set; }
    public DbSet<TempUser> TempUsers { get; set; }
    public DbSet<ForgotPassword> ForgotPasswords { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Location> Locations { get; set; }
}



public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Bu yerda connection string ni belgilang
        optionsBuilder.UseNpgsql("Host=interchange.proxy.rlwy.net;Port=34711;Database=railway;Username=postgres;Password=JpRlcfiGedaCAyNfEWTeMBBaWltNMZqd");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}