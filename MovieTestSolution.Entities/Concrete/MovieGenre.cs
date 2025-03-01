using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.Concrete
{
    public class MovieGenre
    {
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
        public Genre Genre { get; set; }
        public Guid GenreId { get; set; }
    }
}
