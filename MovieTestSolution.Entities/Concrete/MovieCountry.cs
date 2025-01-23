using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.Concrete
{
    public class MovieCountry
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
    }
}
