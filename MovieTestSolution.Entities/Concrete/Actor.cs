using MovieTestSolution.Entities.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.Concrete
{
    public class Actor : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
