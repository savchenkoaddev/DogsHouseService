using FluentValidation;

namespace DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs
{
    internal sealed class GetDogsQueryValidator
        : AbstractValidator<GetDogsQuery>
    {
        private static readonly HashSet<string> AllowedSortColumns = new()
        {
            "id", "name", "color", "taillength", "tail-length", "tail_length", "weight"
        };

        public GetDogsQueryValidator()
        {
            RuleFor(x => x.Order)
                .Must(o => string.IsNullOrWhiteSpace(o) || o.ToLower() == "asc" || o.ToLower() == "desc")
                .WithMessage("Order must be either 'asc' or 'desc'.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize must be greater than 0.");

            RuleFor(x => x.Attribute)
                .Must(a => string.IsNullOrWhiteSpace(a) || AllowedSortColumns.Contains(a.ToLower()))
                .WithMessage($"Sort column must be one of: {string.Join(", ", AllowedSortColumns)}.");
        }
    }
}
