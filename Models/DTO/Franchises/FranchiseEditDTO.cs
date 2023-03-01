namespace MovieApi.Models.DTO.Franchises
{
    public class FranchiseEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
