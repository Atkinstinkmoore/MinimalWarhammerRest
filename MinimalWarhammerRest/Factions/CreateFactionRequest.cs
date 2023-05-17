using System.ComponentModel.DataAnnotations;

namespace MinimalWarhammerRest.Factions
{
    public sealed class CreateFactionRequest
    {
        [Required]
        public string Name { get; init; } = default!;
    }
}
