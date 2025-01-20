using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.Concrete
{
    public class MovieStudio
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}
