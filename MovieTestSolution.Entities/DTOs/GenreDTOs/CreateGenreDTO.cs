using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.GenreDTOs
{
    public class CreateGenreDTO
    {
        [Required(ErrorMessage = "Genre name is required.")]
        [StringLength(100, ErrorMessage = "Genre name cannot exceed 100 characters.")]
        public string Name { get; init; }
    }
}
