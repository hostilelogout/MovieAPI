using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Domain
{
    [Table("Character")]
    public class Character
    {
        public int Id { get; set; }
        public string ? FullName { get; set; }
        public string ? Alias { get; set; }
        public enum Genders { Male,Female,Other} 
        public Genders ? Gender { get; set; }
        public string ? PictureUrl { get; set; }

    }
}
