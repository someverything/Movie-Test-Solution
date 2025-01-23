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
        public string Title { get; set; }                     
        public string Description { get; set; }              
        public Guid RatingId { get; set; }                  
        public decimal Rating { get; set; }                   
        public int? Views { get; set; }                    
        public ICollection<Studio> Studios { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Country> Countries { get; set; }
        public Guid DirectorId { get; set; }
        public Director Directo { get; set; }
        public string CreatedBy { get; set; }                
        public string? UpdatedBy { get; set; }
    }
}
