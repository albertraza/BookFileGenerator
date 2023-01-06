using AutoMapper;
using Books.DTO;
using Books.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Books.Services
{
    public class BookService : IBookService
    {
        private readonly BookApiConfig _config;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public BookService(BookApiConfig config,
                           ICacheService cacheService,
                           IMapper mapper)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _config = config;
        }

        public async Task<BookResponseDTO> GetByISBN(string isbn)
        {
            using var client = new HttpClient();
            var book = new BookResponseDTO();
            if (_cacheService.Exists(isbn))
            {
                book = _cacheService.Get<BookResponseDTO>(isbn);
                book.DataRetrivalType = "Cache";
            }
            else
            {
                var response = await client.GetAsync($"{_config.ServerUrl}/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data");
                var jsonString = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonString);
                var jsonData = json.GetValue($"ISBN:{isbn}").ToString();
                var bookResponse = JsonConvert.DeserializeObject<BookApiResponse>(jsonData);
                book = _mapper.Map<BookResponseDTO>(bookResponse);
                book.ISBN = isbn;
                book.DataRetrivalType = "Server";
                _cacheService.Save<BookResponseDTO>(isbn, book);
            }
            book.Row = 1;
            return book;
        }

        public async Task<List<BookResponseDTO>> GetFromISBNsAsync(IEnumerable<string> isbnList)
        {
            var bookList = new List<BookResponseDTO>();
            int count = 1;
            foreach (var isbn in isbnList)
            {
                var book = await GetByISBN(isbn);
                book.Row = count;
                bookList.Add(book);
                count++;
            }

            return bookList;
        }
    }
}
