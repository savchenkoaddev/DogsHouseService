namespace DogsHouseService.Application.UseCases.Dogs.DTO
{
    public sealed record DogResponse(
        string Name,
        string Color,
        int TailLength,
        int Weight);
}
