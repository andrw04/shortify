using System.ComponentModel.DataAnnotations.Schema;

namespace Shortify.Data.Entities
{
    [Table("links")]
    public class Link
    {

        [Column("id")]
        public string Id { get; set; } = null!;

        [Column("long_url")]
        public string LongURL { get; set; } = null!;

        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [Column("click_count")]
        public int ClickCount { get; set; } = 0;
    }
}
