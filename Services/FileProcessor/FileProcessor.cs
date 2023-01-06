namespace Books.Services
{
    public class FileProcessor
    {
        private readonly IEnumerable<IFileParser> _fileParsers;

        public FileProcessor(IEnumerable<IFileParser> fileParsers)
        {
            _fileParsers = fileParsers;
        }

        public Task<IEnumerable<string>> Process(IFormFile file)
        {
            var fileExt = Path.GetExtension(file.FileName);
            var fileParser = _fileParsers.FirstOrDefault(fp => fp.FileExt == fileExt);

            if (fileParser == null) throw new InvalidOperationException($"No file parser found for {fileExt} files.");

            return fileParser.Parse(file);
        }
    }
}
