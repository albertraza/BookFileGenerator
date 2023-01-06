using AutoMapper;
using Books.DTO;
using Books.Models;

namespace Books.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookApiResponse, BookResponseDTO>()
                .ForMember(dto => dto.AuthorsName, config => config.MapFrom(src => src.Authors.Select(a => a.Name)));
        }
    }
}
