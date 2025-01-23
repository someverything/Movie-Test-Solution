using MovieTestSolution.Entities.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.Concrete
{
    public class Director : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
