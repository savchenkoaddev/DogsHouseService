using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using FluentValidation;

namespace DogsHouseService.Application.UseCases.Dogs.Commands
{
    internal sealed class CreateDogCommandValidator
        : AbstractValidator<CreateDogCommand>
    {
        public CreateDogCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Dog name is required.")
                .MaximumLength(DogName.MaxLength);

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Dog color is required.")
                .MaximumLength(DogColor.MaxLength);

            RuleFor(x => x.TailLength)
                .NotEmpty().WithMessage("Tail length is required.")
                .InclusiveBetween(
                    from: TailLength.MinLength,
                    to: TailLength.MaxLength)
                .WithMessage($"Tail length must be between {TailLength.MinLength} and {TailLength.MaxLength}."); ;

            RuleFor(x => x.Weight)
                .NotEmpty().WithMessage("Weight is required.")
                .InclusiveBetween(
                    from: DogWeight.MinValue,
                    to: DogWeight.MaxValue)
                .WithMessage($"Weight must be between {DogWeight.MinValue} and {DogWeight.MaxValue}.");
        }
    }
}
