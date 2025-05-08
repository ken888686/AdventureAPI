using AdventureAPI.Core.Aggregates.StoreAggregate;

namespace AdventureAPI.Infrastructure.Data.Config;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Store");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(
            x => x.Address,
            a =>
            {
                a.Property(p => p.PostalCode)
                    .HasMaxLength(8)
                    .IsRequired();
                a.Property(p => p.Prefecture)
                    .HasMaxLength(20)
                    .IsRequired();
                a.Property(p => p.City)
                    .HasMaxLength(50)
                    .IsRequired();
                a.Property(p => p.Ward)
                    .HasMaxLength(50);
                a.Property(p => p.Block)
                    .HasMaxLength(50)
                    .IsRequired();
                a.Property(p => p.Number)
                    .HasMaxLength(20)
                    .IsRequired();
                a.Property(p => p.Building)
                    .HasMaxLength(100);
                a.Property(p => p.Lng)
                    .HasColumnType("numeric(9, 6)");
                a.Property(p => p.Lat)
                    .HasColumnType("numeric(8, 6)");
            });

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_STORE_NAME_LENGTH)
            .IsRequired();
        builder.Property(x => x.Logo);
        builder.Property(x => x.Link);
        builder.Property(x => x.Status)
            .HasConversion(
                x => x.Value,
                x => StoreStatus.FromValue(x));
        builder.Property(x => x.CreateTime)
            .HasColumnType("timestamp with time zone");
        builder.Property(x => x.CreateUser)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_NAME_LENGTH);
        builder.Property(x => x.UpdateTime)
            .HasColumnType("timestamp with time zone");
        builder.Property(x => x.UpdateUser)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_NAME_LENGTH);
    }
}
