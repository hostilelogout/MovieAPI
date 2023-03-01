using MovieApi.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTO.Franchises
{
    public class FranchiseCreateDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
