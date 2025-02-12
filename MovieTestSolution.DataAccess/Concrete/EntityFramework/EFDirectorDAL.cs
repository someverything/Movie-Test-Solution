using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Core.Utilities.Results.Concrete.ErrorResult;
using MovieTestSolution.Core.Utilities.Results.Concrete.SuccessResult;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.DirectoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Concrete.EntityFramework
{
    public class EFDirectorDAL : EFRepositoryBase<Director, AppDbContext>, IDirectorDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.Logging.ILogger<Country> _logger;

        public EFDirectorDAL(AppDbContext context, IMapper mapper, Microsoft.Extensions.Logging.ILogger<Country> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public IResult Create(CreateDirectorDTO model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (model == null)
                    return new ErrorResult("Director data is required.", System.Net.HttpStatusCode.BadRequest);

                var director = _mapper.Map<Director>(model);
                _context.Directors.Add(director);
                _context.SaveChanges();

                transaction.Commit();
                return new SuccessResult("Director created successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error while creating director.");
                return new ErrorResult("An error occurred while creating the director.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var director = await _context.Directors.FindAsync(Id);
                if (director == null)
                    return new ErrorResult("Director not found.", System.Net.HttpStatusCode.NotFound);

                _context.Directors.Remove(director);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return new SuccessResult("Director deleted successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error while deleting director.");
                return new ErrorResult("An error occurred while deleting the director.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<GetDirectorDTO> Get(Guid Id)
        {
            try
            {
                var director = _context.Directors.Find(Id);
                if (director == null)
                    return new ErrorDataResult<GetDirectorDTO>("Director not found.", System.Net.HttpStatusCode.NotFound);

                var directorDto = _mapper.Map<GetDirectorDTO>(director);
                return new SuccessDataResult<GetDirectorDTO>(directorDto, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching director.");
                return new ErrorDataResult<GetDirectorDTO>("An error occurred while fetching the director.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<ICollection<GetDirectorDTO>> GetAll()
        {
            try
            {
                var directors = _context.Directors.ToList();
                var directorDtos = _mapper.Map<ICollection<GetDirectorDTO>>(directors);

                return new SuccessDataResult<ICollection<GetDirectorDTO>>(directorDtos, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all directors.");
                return new ErrorDataResult<ICollection<GetDirectorDTO>>("An error occurred while fetching directors.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateDirectorDTO model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var director = await _context.Directors.FindAsync(Id);
                if (director == null)
                    return new ErrorResult("Director not found.", System.Net.HttpStatusCode.BadRequest);

                _mapper.Map(model, director);
                _context.Directors.Update(director);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return new SuccessResult("Director updated successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error while updating director.");
                return new ErrorResult("An error occurred while updating the director.", System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
