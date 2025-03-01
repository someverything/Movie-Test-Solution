using MovieTestSolution.Entities.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.Concrete
{
    public class Movie : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }              
        public decimal? Rating { get; set; }                   
        public int? Views { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public ICollection<MovieStudio> MovieStudios { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieCountry> MovieCountries { get; set; }
        public Guid DirectorId { get; set; }
        public Director Director { get; set; }
        public string CreatedBy { get; set; }                
        public string? UpdatedBy { get; set; }
    }
}
