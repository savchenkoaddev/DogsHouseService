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
            builder.HasKey(dog => dog.Id);

            builder.HasIndex(dog => dog.Id)
                .IsUnique();

            builder.Property(dog => dog.Id)
                .ValueGeneratedNever()
                .HasConversion(
                 id => id.Value,
                 value => new DogId(value));

            builder.Property(dog => dog.Name)
                .HasMaxLength(DogName.MaxLength)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => DogName.Create(value).Value);

            builder.HasIndex(dog => dog.Name)
                .IsUnique();

            builder.Property(dog => dog.Color)
                .HasMaxLength(DogColor.MaxLength)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => DogColor.Create(value).Value);

            builder.Property(dog => dog.TailLength)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => TailLength.Create(value).Value);

            builder.Property(dog => dog.Weight)
                .IsRequired()
                .HasConversion(
                    content => content.Value,
                    value => DogWeight.Create(value).Value);
        }
    }
}
