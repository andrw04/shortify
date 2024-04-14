using System.ComponentModel.DataAnnotations.Schema;

namespace Shortify.Data.Mapping.DTOs
{
    public class LinkDto
    {
        public string Id { get; set; } = null!;
        public string ShortURL { get; set; } = null!;
        public string LongURL { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public int ClickCount { get; set; } = 0;
    }
}
