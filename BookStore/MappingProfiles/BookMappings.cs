using AutoMapper;
using BookStore.Business.Model;
using BookStore.DB.Model;

namespace BookStore.MappingProfiles;

public class BookMappings : Profile
{
    public BookMappings()
    {
        CreateMap<BookEntity, Book>()
            .ForMember("Authors",
            opt => opt.MapFrom(e => e.Authors.Select(a => a.Name)))
            .ForMember("PublishingHouse", 
                opt => opt.MapFrom(e => e.PublishingHouseEntity.Naming))
            .ForMember("PublishLocation", 
            opt => opt.MapFrom(e => e.PublishingHouseEntity.Location));
        CreateMap<Book, BookEntity>()
            .ForMember("Authors",
                opt =>
                    opt.MapFrom(e => e.Authors.Select(a => new AuthorEntity() { Name = a })))
            .ForMember("PublishingHouseEntity", 
                opt => 
                    opt.MapFrom(e => new PublishHouseEntity() { Naming = e.PublishingHouse, Location = e.PublishLocation }));
    }
}