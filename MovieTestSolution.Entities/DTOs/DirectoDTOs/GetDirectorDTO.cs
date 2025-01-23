using MovieTestSolution.Entities.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.DirectoDTOs
{
    public class GetDirectorDTO
    {
        public string Name { get; set; }
        public ICollection<GetMovieNameDTO> MovieName { get; set; }
    }
}
