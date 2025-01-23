using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.RatingDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using MovieTestSolution.Entities.DTOs.ViewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.MovieDTOs
{
    public class GetMovieDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public decimal Rating { get; set; }
        public ICollection<GetCountryDTO> Countries { get; set; }
        public ICollection<GetStudioDTO> Studios { get; set; }
        public ICollection<GetActorDTO> Actors { get; set; }
        public ICollection<GetGenreDTO> Genres { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
