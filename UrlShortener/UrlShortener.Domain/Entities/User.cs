using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UrlShortener.Domain.Entities
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public uint RoleId { get; set; }
        [JsonIgnore]
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [JsonIgnore]
        public ICollection<UrlInfo> UrlInfos { get; set; }

    }
}
