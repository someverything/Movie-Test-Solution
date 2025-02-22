using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.MovieDTOs
{
    public class UpdateMovieDTO
    {
        public required Guid Id { get; set; }
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
        public required string UpdatedBy { get; set; }
    }
}
