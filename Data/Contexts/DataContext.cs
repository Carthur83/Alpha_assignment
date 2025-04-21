using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<MemberEntity>(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<MemberAddressEntity> MemberAddresses { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<NotificationTargetEntity> TargetGroups { get; set; }
    public DbSet<NotificationDismissEntity> DismissedNotifications { get; set; }
    public DbSet<NotificationTypeEntity> NotificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<StatusEntity>().HasData(
            new StatusEntity { Id = 1, Status = "Started" },
            new StatusEntity { Id = 2, Status = "Completed" }
            );
    }
}
