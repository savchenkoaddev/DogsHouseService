using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogsHouseService.Infrastructure.Persistence.Configurations
{
    internal sealed class DogConfiguration
        : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.HasIndex(q => q.Id)
                .IsUnique();

            builder.Property(cs => cs.Id).HasConversion(
                 id => id.Value,
                 value => new DogId(value));

            builder.Property(q => q.Name)
                .HasMaxLength(DogName.MaxLength)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => DogName.Create(value).Value);

            builder.HasIndex(d => d.Name)
                .IsUnique();

            builder.Property(q => q.Color)
                .HasMaxLength(DogColor.MaxLength)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => DogColor.Create(value).Value);

            builder.Property(q => q.TailLength)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => TailLength.Create(value).Value);

            builder.Property(q => q.Weight)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => DogWeight.Create(value).Value);
        }
    }
}
