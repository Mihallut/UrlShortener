namespace UrlShortener.Domain.Entities
{
    public class UrlInfo
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortedUrl { get; set; }
        public string Code { get; set; }
        public Guid IdCreatedBy { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
