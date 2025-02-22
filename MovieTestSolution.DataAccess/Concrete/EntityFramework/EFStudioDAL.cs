using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
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
    public class EFStudioDAL : EFRepositoryBase<Studio, AppDbContext>, IStudioDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.Logging.ILogger<Studio> _logger;

        public EFStudioDAL(AppDbContext context, IMapper mapper, Microsoft.Extensions.Logging.ILogger<Studio> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IDataResult<CreateStudioDTO>> CreateAsync(CreateStudioDTO model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invalid Studio data provided");
                return new ErrorDataResult<CreateStudioDTO>("Invalid Studio data provided", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var studio = _mapper.Map<Studio>(model);

                await _context.Studios.AddAsync(studio);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{studio.Name} created successfully");
                return new SuccessDataResult<CreateStudioDTO>(_mapper.Map<CreateStudioDTO>(studio), $"{studio.Name} created successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while creating the Studio");
                return new ErrorDataResult<CreateStudioDTO>("An error occurred while creating the Studio", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _logger.LogWarning("Invalid Id provided");
                return new ErrorResult("Invalid Id provided", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var studio = await _context.Studios.FirstOrDefaultAsync(x => x.Id == Id);

                if (studio == null)
                {
                    _logger.LogWarning("Studio not found");
                    return new ErrorResult("Studio not found", System.Net.HttpStatusCode.NotFound);
                }

                _context.Studios.Remove(studio);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Studio deleted successfully.");
                return new SuccessResult("Studio deleted successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting studio");
                return new ErrorResult("Error occurred while deleting studio", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<GetStudioDTO> Get(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _logger.LogWarning("Invalid Studio Id provided");
                return new ErrorDataResult<GetStudioDTO>("Invalid Studio Id provided", System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var studio = _context.Studios.AsNoTracking()
                    .Where(x => x.Id == Id)
                    .Select(x => _mapper.Map<GetStudioDTO>(x))
                    .FirstOrDefault();

                if (studio == null)
                {
                    _logger.LogWarning("Studio not found");
                    return new ErrorDataResult<GetStudioDTO>("Studio not found", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<GetStudioDTO>(studio, "Studio retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the studio.");
                return new ErrorDataResult<GetStudioDTO>("An error occurred while retrieving the studio.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<ICollection<GetStudioDTO>> GetAll()
        {
            try
            {
                var studios = _context.Studios.AsNoTracking()
                    .Select(x => _mapper.Map<GetStudioDTO>(x))
                    .ToList();

                if (studios == null || !studios.Any())
                {
                    _logger.LogWarning("No studios found.");
                    return new ErrorDataResult<ICollection<GetStudioDTO>>("No studios found.", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<ICollection<GetStudioDTO>>(studios, "Studios retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting studios");
                return new ErrorDataResult<ICollection<GetStudioDTO>>("An error occurred while getting studios", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateStudioDTO model)
        {
            if (Id == Guid.Empty || model == null)
            {
                _logger.LogWarning("Invalid input: Id or studio data is null/empty.");
                return new ErrorResult("Invalid input: Id or studio data is null/empty.", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var studio = await _context.Studios.FirstOrDefaultAsync(x => x.Id == Id);

                if (studio == null)
                {
                    _logger.LogWarning("Studio not found");
                    return new ErrorResult("Studio not found", System.Net.HttpStatusCode.NotFound);
                }

                _mapper.Map(model, studio);

                _context.Studios.Update(studio);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{studio.Name} updated successfully.");
                return new SuccessResult($"{studio.Name} updated successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Database error occurred while updating studio.");
                return new ErrorResult("Database error occurred while updating studio.", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while updating studio.");
                return new ErrorResult("An error occurred while updating studio.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
