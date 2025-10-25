namespace DogsHouseService.Application.Abstractions
{
    public interface IAppOptions
    {
        /// <summary>
        /// The configuration section name this options class should bind to.
        /// </summary>
        static abstract string SectionName { get; }
    }
}
