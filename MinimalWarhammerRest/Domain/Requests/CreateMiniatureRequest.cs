using System.ComponentModel.DataAnnotations;

namespace MinimalWarhammerRest.Domain.Requests
{
    public struct CreateMiniatureRequest
    {
        public string Name { get; init; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount has to be more than 0")]
        public int Amount { get; init; }
        public int FactionId { get; init; }
        public int SubfactionId { get; set; }
    }
}
