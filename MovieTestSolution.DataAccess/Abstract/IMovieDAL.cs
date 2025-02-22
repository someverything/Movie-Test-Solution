using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface IMovieDAL : IRepositoryBase<Movie>
    {
        Task<IResult> CreateAsync(CreateMovieDTO model);
        Task<IResult> DeleteAsync(Guid Id);
        Task<IDataResult<GetMovieDTO>> GetByIdAsync(Guid Id);
        Task<IDataResult<ICollection<GetMovieDTO>>> GetAllAsync();
        Task<IResult> UpdateAsync(UpdateMovieDTO model, Guid Id);
    }
}
