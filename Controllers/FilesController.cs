using Books.DTO;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FileProcessor _fileProcessor;
        private readonly IBookService _bookService;
        private readonly ICacheService _cacheService;
        private readonly ICSVContentGenerator<BookResponseDTO> _csvContentGenerator;

        public FilesController(FileProcessor fileProcessor,
            IBookService bookService,
            ICacheService cacheService,
            ICSVContentGenerator<BookResponseDTO> csvContentGenerator)
        {
            _csvContentGenerator = csvContentGenerator;
            _cacheService = cacheService;
            _bookService = bookService;
            _fileProcessor = fileProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile([FromForm] FileDTO fileDTO)
        {
            var isbnList = await _fileProcessor.Process(fileDTO.File);
            var books = await _bookService.GetFromISBNsAsync(isbnList);

            var fileHeader = "Row Number,Data Retrieval Type,ISBN,Title,Subtitle,Author Name(s),Number of Pages,Publish Date";
            var fileContent = _csvContentGenerator.GenerateCSV(books, fileHeader);

            Guid fileId = Guid.NewGuid();
            _cacheService.Save<string>(fileId.ToString(), fileContent);

            return CreatedAtAction("GetFile", new { id = fileId.ToString() }, new { FileContent = books, FileId = fileId.ToString() });
        }

        [HttpGet("{id}", Name = "GetFile")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            if (!_cacheService.Exists(id.ToString())) return NotFound();

            var content = _cacheService.Get<string>(id.ToString());

            var stream = new MemoryStream();
            var writter = new StreamWriter(stream);
            await writter.WriteAsync(content);
            await writter.FlushAsync();
            stream.Position = 0;

            return File(stream, "text/csv");
        }
    }
}
