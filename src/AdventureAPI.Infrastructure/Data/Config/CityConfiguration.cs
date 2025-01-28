using AdventureAPI.Core.Aggregates.CityAggregate;

namespace AdventureAPI.Infrastructure.Data.Config;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_CITY_NAME_LENGTH)
            .IsRequired();
        builder.Property(x => x.CreateTime)
            .HasColumnType("timestamp with time zone");
        builder.Property(x => x.CreateUser)
            .HasMaxLength(DataSchemaConstants.DEFAULT_CITY_NAME_LENGTH);
        builder.Property(x => x.UpdateTime)
            .HasColumnType("timestamp with time zone");
        builder.Property(x => x.UpdateUser)
            .HasMaxLength(DataSchemaConstants.DEFAULT_CITY_NAME_LENGTH);
    }
}
