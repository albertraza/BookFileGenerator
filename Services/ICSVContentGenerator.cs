namespace Books.Services
{
    public interface ICSVContentGenerator<T>
    {
        string GenerateCSV(IEnumerable<T> dataRows, string commaSeparatedHeader = null);
    }
}
