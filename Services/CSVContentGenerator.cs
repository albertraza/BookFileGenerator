using System.Text;

namespace Books.Services
{
    public class CSVContentGenerator<T> : ICSVContentGenerator<T>
    {
        public string GenerateCSV(IEnumerable<T> dataRows, string commaSeparatedHeader = null)
        {
            if (dataRows == null) throw new ArgumentNullException(nameof(dataRows));

            var csvContent = new StringBuilder();

            if (commaSeparatedHeader != null)
            {
                csvContent.AppendLine(commaSeparatedHeader);
            }

            foreach (var row in dataRows)
            {
                csvContent.AppendLine(row.ToString());
            }
            return csvContent.ToString();
        }
    }
}
