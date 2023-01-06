namespace Books.DTO
{
    public class BookResponseDTO
    {
        public int Row { get; set; }
        public string DataRetrivalType { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; } = "N/A";
        public List<string> AuthorsName { get; set; }
        public string NumberOfPages { get; set; } = "N/A";
        public string PublishDate { get; set; }

        public override string ToString()
        {
            var authorsNames = string.Join(", ", AuthorsName);
            return string.Format("{0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",
                Row,
                DataRetrivalType,
                ISBN,
                Title,
                Subtitle == null ? "N/A" : Subtitle,
                authorsNames,
                NumberOfPages == null ? "N/A" : NumberOfPages,
                PublishDate
                );
        }
    }
}
