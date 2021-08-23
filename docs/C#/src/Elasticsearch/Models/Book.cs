using Nest;

namespace Elasticsearch.Models
{
    [ElasticsearchType(IdProperty = nameof(BookId))]
    public class Book
    {
        [Text(Ignore = true)]
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PageCount { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }
        public string Authors { get; set; }
        public string Categories { get; set; }
    }
}