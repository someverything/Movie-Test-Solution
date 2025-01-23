using MovieTestSolution.Entities.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.GenreDTOs
{
    public class GetGenreDTO
    {
        public string Name { get; set; }
        public GetMovieNameDTO MovieName { get; set; }
    }
}
