
namespace ConstructPP.API.Shared
{
    //TODO: Что за хуйня и зачем оно существует?
    public readonly record struct Report
    {
        public required bool Success { get; init; }
        public required string Message { get; init; }
        public string Error { get; init; }

    }
}
