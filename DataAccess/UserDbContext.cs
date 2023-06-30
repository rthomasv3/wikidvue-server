using Microsoft.EntityFrameworkCore;
using WikidVueServer.DataAccess.Data;

namespace WikidVueServer.DataAccess;

public partial class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
        
        if (ChangeTracker != null)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }

    public virtual DbSet<UserData> Users { get; set; }

    public virtual DbSet<UserSessionData> UserSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserData>(entity =>
        {
            entity.ToTable("User", "wikid");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Email).HasDatabaseName("IX_User_Email").IsUnique();

            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.Created).HasPrecision(2);
            entity.Property(e => e.Email).HasMaxLength(1024);
            entity.Property(e => e.LastLogin).HasPrecision(2);
            entity.Property(e => e.Password).HasMaxLength(1024);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<UserSessionData>(entity =>
        {
            entity.ToTable("UserSession", "wikid");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.IsLoggedIn).HasDatabaseName("IX_UserSession_IsLoggedIn");
            entity.HasIndex(e => e.RefreshToken).HasDatabaseName("IX_UserSession_RefreshToken");
            entity.HasIndex(e => e.RefreshTokenExpiration).HasDatabaseName("IX_UserSession_RefreshTokenExpiration");

            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.UserDevice).HasMaxLength(2048);
            entity.Property(e => e.IpAddress).HasMaxLength(512);
            entity.Property(e => e.RefreshToken).HasMaxLength(1024);
            entity.Property(e => e.IsLoggedIn).HasDefaultValue(false);
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
            entity.Property(e => e.Created).HasPrecision(2);
            entity.Property(e => e.RefreshTokenExpiration).HasPrecision(2);
            entity.Property(e => e.Updated).HasPrecision(2);

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSession_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
