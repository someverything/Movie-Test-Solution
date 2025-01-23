using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.MovieDTOs
{
    public class UpdateMovieDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public ICollection<Guid> CountriesIds { get; set; }
        public ICollection<Guid> StudioIds { get; set; } 
        public ICollection<Guid> ActorIds { get; set; }  
        public ICollection<Guid> GenreIds { get; set; }  
        public string? UpdatedBy { get; set; }
    }
}
