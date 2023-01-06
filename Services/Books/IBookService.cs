using Books.DTO;

namespace Books.Services
{
    public interface IBookService
    {
        Task<BookResponseDTO> GetByISBN(string isbn);
        Task<List<BookResponseDTO>> GetFromISBNsAsync(IEnumerable<string> isbnList);
    }
}
