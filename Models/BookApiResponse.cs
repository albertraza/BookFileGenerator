using Newtonsoft.Json;

namespace Books.Models
{
    public partial class BookApiResponse
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("authors")]
        public Author[] Authors { get; set; }

        [JsonProperty("number_of_pages")]
        public int? NumberOfPages { get; set; }

        [JsonProperty("pagination")]
        public string Pagination { get; set; }

        [JsonProperty("by_statement")]
        public string ByStatement { get; set; }

        [JsonProperty("identifiers")]
        public Identifiers Identifiers { get; set; }

        [JsonProperty("classifications")]
        public Classifications Classifications { get; set; }

        [JsonProperty("publishers")]
        public Publish[] Publishers { get; set; }

        [JsonProperty("publish_places")]
        public Publish[] PublishPlaces { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDate { get; set; }

        [JsonProperty("subjects")]
        public Author[] Subjects { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("ebooks")]
        public Ebook[] Ebooks { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Classifications
    {
        [JsonProperty("lc_classifications")]
        public string[] LcClassifications { get; set; }

        [JsonProperty("dewey_decimal_class")]
        public string[] DeweyDecimalClass { get; set; }
    }

    public partial class Cover
    {
        [JsonProperty("small")]
        public Uri Small { get; set; }

        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("large")]
        public Uri Large { get; set; }
    }

    public partial class Ebook
    {
        [JsonProperty("preview_url")]
        public Uri PreviewUrl { get; set; }

        [JsonProperty("availability")]
        public string Availability { get; set; }

        [JsonProperty("formats")]
        public Formats Formats { get; set; }
    }

    public partial class Formats
    {
    }

    public partial class Identifiers
    {
        [JsonProperty("librarything")]
        public long[] Librarything { get; set; }

        [JsonProperty("wikidata")]
        public string[] Wikidata { get; set; }

        [JsonProperty("goodreads")]
        public string[] Goodreads { get; set; }

        [JsonProperty("isbn_10")]
        public string[] Isbn10 { get; set; }

        [JsonProperty("lccn")]
        public string[] Lccn { get; set; }

        [JsonProperty("openlibrary")]
        public string[] Openlibrary { get; set; }
    }

    public partial class Publish
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
