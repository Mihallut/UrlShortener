using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Entities
{
    public class Role
    {
        [Required]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
