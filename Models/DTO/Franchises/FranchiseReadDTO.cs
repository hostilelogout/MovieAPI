using MovieApi.Models.Domain;

namespace MovieApi.Models.DTO.Franchises
{
    public class FranchiseReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Dictionary<int, string> Movies { get; set; } = null!;
    }
}
