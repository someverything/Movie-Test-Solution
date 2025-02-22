using MovieTestSolution.Entities.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.Business.Abstract
{
    public interface IMovieServices
    {
        Task<IResult> CreateAsync(CreateMovieDTO model);
        Task<IResult> DeleteAsync(Guid Id);
        Task<IDataResult<GetMovieDTO>> GetByIdAsync(Guid Id);
        Task<IDataResult<ICollection<GetMovieDTO>>> GetAllAsync();
        Task<IResult> UpdateAsync(UpdateMovieDTO model, Guid Id);
    }
}
