using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;
using UtilitiesDLL.Results.Concrete.ErrorResults;
using UtilitiesDLL.Results.Concrete.SuccessResults;

namespace MovieTestSolution.DataAccess.Concrete.EntityFramework
{
    public class EFMovieDAL : EFRepositoryBase<Movie, AppDbContext>, IMovieDAL
    {
        private readonly AppDbContext _context;
        private readonly ILogger<Movie> _logger;
        private readonly IMapper _mapper;

        public EFMovieDAL(AppDbContext context, ILogger<Movie> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IResult> CreateAsync(CreateMovieDTO model)
        {
            _logger.LogInformation("Method started");
            if (model == null)
            {
                _logger.LogWarning("Check all fields");
                return new ErrorResult("Check all fields", System.Net.HttpStatusCode.BadRequest); 
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var movie = _mapper.Map<Movie>(model);
                await transaction.CommitAsync();
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Movie '{movie.Title}' created successfully.");
                return new SuccessResult($"Movie '{movie.Title}' created successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"Error creating movie: {ex.Message}");
                return new ErrorResult($"Error creating movie: {ex.Message}", System.Net.HttpStatusCode.BadRequest);
            }

        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            _logger.LogInformation("Method started");
            if (Id == Guid.Empty)
            {
                _logger.LogWarning("Field is empty");
                return new ErrorResult("Field is empty", System.Net.HttpStatusCode.BadRequest);
            }
            
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

                if (movie == null)
                {
                    _logger.LogError($"Movie by Id {Id} not found");
                    return new ErrorResult($"Movie by Id {Id} not found", System.Net.HttpStatusCode.NotFound);
                }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{movie.Title} deleted successfully.");
                return new SuccessResult($"{movie.Title} deleted successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("An error occurred while deleting movie");
                return new SuccessResult("An error occurred while deleting movie", System.Net.HttpStatusCode.OK);
            }
        }

        public async Task<IDataResult<ICollection<GetMovieDTO>>> GetAllAsync()
        {
            _logger.LogInformation("Method started");
            try
            {
                var movies = await _context.Movies.AsNoTracking()
                    .ToListAsync();

                var res = _mapper.Map<GetMovieDTO>(movies);

                if (movies == null || !movies.Any())
                {
                    _logger.LogWarning("No genres found.");
                    return new ErrorDataResult<ICollection<GetMovieDTO>>("No genres found.", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<ICollection<GetMovieDTO>>(data: (ICollection<GetMovieDTO>)res, "Genres retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting genres");
                return new ErrorDataResult<ICollection<GetMovieDTO>>("An error occurred while getting genres", System.Net.HttpStatusCode.BadRequest);
            }

        }

        public async Task<IDataResult<GetMovieDTO>> GetByIdAsync(Guid Id)
        {
            _logger.LogInformation("Method started");
            if(Id == Guid.Empty)
            {
                _logger.LogWarning("Field is empty");
                return new ErrorDataResult<GetMovieDTO>(data: null, "Field is empty", System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

                var res = _mapper.Map<GetMovieDTO>(movie);

                if(res == null)
                {
                    _logger.LogError("There is no data");
                    return new ErrorDataResult<GetMovieDTO>(data: res, "There is no data", System.Net.HttpStatusCode.NotFound);
                }

                _logger.LogInformation("Method finished successfully");
                return new SuccessDataResult<GetMovieDTO>(data: res, "Movie retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is error ocured while getting movie by Id: {Id}", ex);
                return new ErrorDataResult<GetMovieDTO>(data: null, $"There is error ocured while getting movie by Id: {Id}", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateAsync(UpdateMovieDTO model, Guid Id)
        {
            _logger.LogInformation("Method started");
            if (Id == Guid.Empty || model == null)
            {
                _logger.LogWarning("Invalid input: Id or genre data is null/empty.");
                return new ErrorResult("Invalid input: Id or genre data is null/empty.", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == Id);

                if (movie == null)
                {
                    _logger.LogWarning("Movie not found.");
                    return new ErrorResult("Movie not found.", System.Net.HttpStatusCode.NotFound);
                }

                _mapper.Map(movie, model);
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


                _logger.LogInformation($"{movie.Title} updated successfully.");
                return new SuccessResult($"{movie.Title} updated successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Database error occurred while updating movie.");
                return new ErrorResult("Database error occurred while updating movie.", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while updating movie.");
                return new ErrorResult("An error occurred while updating movie.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
