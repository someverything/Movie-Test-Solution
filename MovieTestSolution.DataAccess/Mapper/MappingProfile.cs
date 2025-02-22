using AutoMapper;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using MovieTestSolution.Entities.DTOs.DirectoDTOs;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.MovieDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {   
            //Actor mapping
            CreateMap<Actor, GetActorDTO>().ReverseMap();
            CreateMap<Actor, CreateActorDTO>().ReverseMap();
            CreateMap<Actor, UpdateActorDTO>().ReverseMap();

            //Country mapping
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            //Director mapping
            CreateMap<Director, CreateDirectorDTO>().ReverseMap();
            CreateMap<Director, GetDirectorDTO>().ReverseMap();
            CreateMap<Director, UpdateDirectorDTO>().ReverseMap();

            //Genre mapping
            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, GetGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();

            //Movie mapping
            CreateMap<Movie, CreateMovieDTO>().ReverseMap();
            CreateMap<Movie, GetMovieDTO>().ReverseMap();
            CreateMap<Movie, UpdateMovieDTO>().ReverseMap();

            //Studion mapping
            CreateMap<Studio, CreateStudioDTO>().ReverseMap();
            CreateMap<Studio, GetStudioDTO>().ReverseMap();
            CreateMap<Studio, UpdateStudioDTO>().ReverseMap();
        }
    }
}
