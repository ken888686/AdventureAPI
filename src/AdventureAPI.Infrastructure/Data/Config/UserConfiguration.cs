using AdventureAPI.Core.Aggregates.UserAggregate;

namespace AdventureAPI.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(
            x => x.Address,
            a =>
            {
                a.Property(p => p.PostalCode).HasMaxLength(8).IsRequired();
                a.Property(p => p.Prefecture).HasMaxLength(20).IsRequired();
                a.Property(p => p.City).HasMaxLength(50).IsRequired();
                a.Property(p => p.Ward).HasMaxLength(50);
                a.Property(p => p.Block).HasMaxLength(50).IsRequired();
                a.Property(p => p.Number).HasMaxLength(20).IsRequired();
                a.Property(p => p.Building).HasMaxLength(100);
                a.Property(p => p.Lng).HasColumnType("numeric(9, 6)");
                a.Property(p => p.Lat).HasColumnType("numeric(8, 6)");
            }
        );

        builder
            .Property(x => x.Username)
            .HasMaxLength(50)
            .IsRequired()
            .UseCollation("case_insensitive");
        builder.HasIndex(x => x.Username).IsUnique().HasFilter("[Status] != 2"); // Exclude suspended users
        builder
            .Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired()
            .UseCollation("case_insensitive");
        builder.HasIndex(x => x.Email).IsUnique().HasFilter("[Status] != 2"); // Exclude suspended users
        builder.Property(x => x.PasswordHash).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Salt).HasMaxLength(64).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.PhotoUrl).HasMaxLength(200);
        builder
            .Property(x => x.Status)
            .HasConversion(x => x.Value, x => UserStatus.FromValue(x))
            .IsRequired();
        builder.Property(x => x.CreateTime).HasColumnType("timestamp with time zone").IsRequired();
        builder
            .Property(x => x.CreateUser)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_NAME_LENGTH)
            .IsRequired();
        builder.Property(x => x.UpdateTime).HasColumnType("timestamp with time zone").IsRequired();
        builder
            .Property(x => x.UpdateUser)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_NAME_LENGTH)
            .IsRequired();
        builder.Property(x => x.UserRole).HasConversion<string>().HasMaxLength(20).IsRequired();
        // Add index for common queries
        builder.HasIndex(x => new { x.Status, x.CreateTime });
    }
}
