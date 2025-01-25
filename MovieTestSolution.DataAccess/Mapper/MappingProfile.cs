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
            CreateMap<Actor, GetActorDTO>();
            CreateMap<Actor, CreaateActorDTO>();
            CreateMap<Actor, UpdateActorDTO>();

            //Country mapping
            CreateMap<Country, CreateCountryDTO>();
            CreateMap<Country, GetCountryDTO>();
            CreateMap<Country, UpdateCountryDTO>();

            //Director mapping
            CreateMap<Director, CreateDirectorDTO>();
            CreateMap<Director, GetDirectorDTO>();
            CreateMap<Director, UpdateDirectorDTO>();

            //Genre mapping
            CreateMap<Genre, CreateGenreDTO>();
            CreateMap<Genre, GetGenreDTO>();
            CreateMap<Genre, UpdateGenreDTO>();

            //Movie mapping
            CreateMap<Movie, CreateMovieDTO>();
            CreateMap<Movie, GetMovieDTO>();
            CreateMap<Movie, UpdateMovieDTO>();
            CreateMap<Movie, GetMovieNameDTO>();

            //Studion mapping
            CreateMap<Studio, CreateStudioDTO>();
            CreateMap<Studio, GetStudioDTO>();
            CreateMap<Studio, UpdateStudioDTO>();
        }
    }
}
