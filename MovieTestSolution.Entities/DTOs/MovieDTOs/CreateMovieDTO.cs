using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.MovieDTOs
{
    public class CreateMovieDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<CreateStudioDTO> Studios { get; set; }
        public ICollection<CreateActorDTO> Actors { get; set; }
        public ICollection<CreateGenreDTO> Genres { get; set; }

    }
}
