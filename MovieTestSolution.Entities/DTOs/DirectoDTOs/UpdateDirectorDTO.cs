using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.DirectoDTOs
{
    public class UpdateDirectorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Guid>? MoviesId { get; set; }
    }
}
