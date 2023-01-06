namespace Books.Services
{
    public abstract class FileParser : IFileParser
    {
        public abstract string FileExt { get; }
        protected bool IsValidExtension(IFormFile file) => FileExt == Path.GetExtension(file.FileName);
        protected abstract Task<IEnumerable<string>> GetContent(IFormFile file);
        public virtual async Task<IEnumerable<string>> Parse(IFormFile file)
        {
            if (!IsValidExtension(file)) throw new InvalidOperationException($"The file {file.FileName} is not supported. This parser only supports {FileExt} files.");
            return await GetContent(file);
        }
    }
}
