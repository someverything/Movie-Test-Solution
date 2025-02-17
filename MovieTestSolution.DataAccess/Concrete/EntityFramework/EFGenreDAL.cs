using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
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
    public class EFGenreDAL : EFRepositoryBase<Genre, AppDbContext>, IGenreDAL
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public EFGenreDAL(AppDbContext context, ILogger logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IDataResult<CreateGenreDTO>> CreateAsync(CreateGenreDTO model)
        {
            _logger.LogInformation("Creating started");
            if (model == null)
            {
                _logger.LogWarning("Model is incorrect");
                return new ErrorDataResult<CreateGenreDTO>("Model is incorrect", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var genre = _mapper.Map<Genre>(model);

                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{genre.Name} created successfully");
                return new SuccessDataResult<CreateGenreDTO>(_mapper.Map<CreateGenreDTO>(genre), $"{genre.Name} created successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while creating the Studio");
                return new ErrorDataResult<CreateGenreDTO>("An error occurred while creating the genre", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting genre started.");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid Genre Id provided.");
                return new ErrorResult("Invalid Genre Id provided.", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

                if (genre == null)
                {
                    _logger.LogWarning("Genre not found.");
                    return new ErrorResult("Genre not found.", System.Net.HttpStatusCode.NotFound);
                }

                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{genre.Name} deleted successfully.");
                return new SuccessResult($"{genre.Name} deleted successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while deleting the genre.");
                return new ErrorResult("An error occurred while deleting the genre.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<GetGenreDTO> Get(Guid id)
        {
            _logger.LogInformation("Method started");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid Genre Id provided");
                return new ErrorDataResult<GetGenreDTO>("Invalid Genre Id provided", System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var genre = _context.Genres.AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => _mapper.Map<GetGenreDTO>(x))
                    .FirstOrDefault();

                if (genre == null)
                {
                    _logger.LogWarning("Genre not found");
                    return new ErrorDataResult<GetGenreDTO>("Genre not found", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<GetGenreDTO>(genre, "Genre retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the genre.");
                return new ErrorDataResult<GetGenreDTO>("An error occurred while retrieving the genre.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<ICollection<GetGenreDTO>> GetAll()
        {
            _logger.LogInformation("Method started");

            try
            {
                var genres = _context.Genres.AsNoTracking()
                    .Select(x => _mapper.Map<GetGenreDTO>(x))
                    .ToList();

                if (genres == null || !genres.Any())
                {
                    _logger.LogWarning("No genres found.");
                    return new ErrorDataResult<ICollection<GetGenreDTO>>("No genres found.", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<ICollection<GetGenreDTO>>(genres, "Genres retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting genres");
                return new ErrorDataResult<ICollection<GetGenreDTO>>("An error occurred while getting genres", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> UpdateAsync(Guid id, UpdateGenreDTO model)
        {
            _logger.LogInformation("Updating genre started.");

            if (id == Guid.Empty || model == null)
            {
                _logger.LogWarning("Invalid input: Id or genre data is null/empty.");
                return new ErrorResult("Invalid input: Id or genre data is null/empty.", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

                if (genre == null)
                {
                    _logger.LogWarning("Genre not found.");
                    return new ErrorResult("Genre not found.", System.Net.HttpStatusCode.NotFound);
                }

                _mapper.Map(model, genre);
                _context.Genres.Update(genre);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{genre.Name} updated successfully.");
                return new SuccessResult($"{genre.Name} updated successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Database error occurred while updating genre.");
                return new ErrorResult("Database error occurred while updating genre.", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while updating genre.");
                return new ErrorResult("An error occurred while updating genre.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

    }
}
