using System.ComponentModel.DataAnnotations;

namespace MinimalWarhammerRest.Miniatures
{
    public sealed class CreateMiniatureRequest
    {
        [Required]
        public string Name { get; init; } = default!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount has to be more than 0")]
        public int Amount { get; init; }

        [Required]
        public int FactionId { get; init; }

        [Required]
        public int SubfactionId { get; set; }
    }
}