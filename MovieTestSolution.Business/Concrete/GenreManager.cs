using Microsoft.Extensions.Logging;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;
using UtilitiesDLL.Results.Concrete.SuccessResults;

namespace MovieTestSolution.Business.Concrete
{
    public class GenreManager : IGenreServices
    {
        private readonly IGenreDAL _genreDAL;
        private readonly ILogger<Genre> _logger;

        public GenreManager(IGenreDAL genreDAL, ILogger<Genre> logger)
        {
            _genreDAL = genreDAL;
            _logger = logger;
        }

        public async Task<IDataResult<CreateGenreDTO>> CreateAsync(CreateGenreDTO model)
        {
            var genre = await _genreDAL.CreateAsync(model);
            return new SuccessDataResult<CreateGenreDTO>(genre.Data, genre.Message, System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            var result = await _genreDAL.DeleteAsync(Id);
            return new SuccessResult(result.Message, System.Net.HttpStatusCode.OK);
        }

        public IDataResult<ICollection<GetGenreDTO>> GetAll()
        {
            var result = _genreDAL.GetAll();
            return new SuccessDataResult<ICollection<GetGenreDTO>>(result.Data, result.Message, System.Net.HttpStatusCode.OK);
        }

        public IDataResult<GetGenreDTO> Get(Guid Id)
        {
            var result = _genreDAL.Get(Id);
            return new SuccessDataResult<GetGenreDTO>(result.Data, result.Message, System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateGenreDTO model)
        {
            var result = await _genreDAL.UpdateAsync(Id, model);
            return new SuccessResult("Genre updated successfully", result.StatusCode);
        }
    }
}
