using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.DirectoDTOs
{
    public class UpdateDirectorDTO
    {
        public required string Name { get; set; }
        public ICollection<Guid>? MoviesId { get; set; }
    }
}
