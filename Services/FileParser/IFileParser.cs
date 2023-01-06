namespace Books.Services
{
    public interface IFileParser
    {
        string FileExt { get; }
        Task<IEnumerable<string>> Parse(IFormFile file);
    }
}
