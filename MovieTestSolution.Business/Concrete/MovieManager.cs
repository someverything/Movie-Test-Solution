using MovieTestSolution.Business.Abstract;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;
using UtilitiesDLL.Results.Concrete.SuccessResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieTestSolution.Business.Concrete
{
    public class MovieManager : IMovieServices
    {
        private readonly IMovieDAL _movieDAL;

        public MovieManager(IMovieDAL movieDAL)
        {
            _movieDAL = movieDAL;
        }

        public async Task<IResult> CreateAsync(CreateMovieDTO model)
        {
            var result = await _movieDAL.CreateAsync(model);
            return new SuccessResult(result.Message, result.StatusCode);
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            var result = await _movieDAL.DeleteAsync(Id);
            return new SuccessResult($"Movie by Id {Id} deleted successfully", System.Net.HttpStatusCode.OK);
        }

        public async Task<IDataResult<ICollection<GetMovieDTO>>> GetAllAsync()
        {
            var result = await _movieDAL.GetAllAsync();
            return new SuccessDataResult<ICollection<GetMovieDTO>>(result.Data, result.Message, result.StatusCode);
        }

        public async Task<IDataResult<GetMovieDTO>> GetByIdAsync(Guid Id)
        {
            var result = await _movieDAL.GetByIdAsync(Id);
            return new SuccessDataResult<GetMovieDTO>(result.Data, result.Message, result.StatusCode);
        }

        public async Task<IResult> UpdateAsync(UpdateMovieDTO model, Guid Id)
        {
            var result = await _movieDAL.UpdateAsync(model, Id);
            return new SuccessResult("Movie updated successfully", System.Net.HttpStatusCode.OK);
        }
    }
}
