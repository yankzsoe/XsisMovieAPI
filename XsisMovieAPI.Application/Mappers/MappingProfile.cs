using AutoMapper;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Domain.Entities;

namespace XsisMovieAPI.Application.Mappers {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Movie, MovieViewModel>();
            CreateMap<MovieViewModel, Movie>();

            CreateMap<CreateUpdateMovie, MovieViewModel>()
                .ForMember(dest => dest.Title, src => src.MapFrom(e => e.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(e => e.Description))
                .ForMember(dest => dest.Rating, src => src.MapFrom(e => e.Rating))
                .ForMember(dest => dest.Image, src => src.MapFrom(e => e.Image))
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdateDate, src => src.Ignore()).ReverseMap();
        }
    }
}
