using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.MovieDTOs
{
    public class GetMovieDTO
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Date { get; set; }
        public decimal? Rating { get; set; }
        public int? Views { get; set; }
        public string DirectorName { get; set; }
        public ICollection<string> Studios { get; set; } = new List<string>();
        public ICollection<string> Actors { get; set; } = new List<string>();
        public ICollection<string> Genres { get; set; } = new List<string>();
        public ICollection<string> Countries { get; set; } = new List<string>();
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
