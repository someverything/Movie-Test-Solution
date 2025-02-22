using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.MovieDTOs
{
    public class CreateMovieDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Date { get; set; }
        public decimal? Rating { get; set; }
        public int? Views { get; set; }
        public Guid DirectorId { get; set; }
        public ICollection<Guid> StudioIds { get; set; } = new List<Guid>();
        public ICollection<Guid> ActorIds { get; set; } = new List<Guid>();
        public ICollection<Guid> GenreIds { get; set; } = new List<Guid>();
        public ICollection<Guid> CountryIds { get; set; } = new List<Guid>();
        public string CreatedBy { get; set; }

    }
}
