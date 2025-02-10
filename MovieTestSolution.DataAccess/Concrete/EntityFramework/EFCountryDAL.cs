using AutoMapper;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Core.Utilities.Results.Concrete.ErrorResult;
using MovieTestSolution.Core.Utilities.Results.Concrete.SuccessResult;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Concrete.EntityFramework
{
    public class EFCountryDAL : EFRepositoryBase<Country, AppDbContext>, ICountryDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.Logging.ILogger<Country> _logger;

        public EFCountryDAL(AppDbContext context, IMapper mapper, Microsoft.Extensions.Logging.ILogger<Country> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        public IResult CreateCountry(CreateCountryDTO model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invalid Country data provided");
                return new ErrorResult("Invalid Country data provided", false, System.Net.HttpStatusCode.BadRequest);
            }

            using var transaction = _context.Database.BeginTransaction();
            
            try
            {
                var country = new Country
                {
                    Name = model.Name,
                };

                _context.Countries.Add(country);

                _context.SaveChanges();
                transaction.Commit();

                _logger.LogInformation($"{country.Name} created successfully");
                return new SuccessResult($"{country.Name} created successfully", true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "There is Error ocurred while creating Country");
                return new ErrorResult("There is Error ocurred while creating Country", false, System.Net.HttpStatusCode.BadRequest);
            }
        }


        public async Task<IResult> DeleteCountryAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _logger.LogWarning("Invalid Id provided");
                return new ErrorResult("Invalid Id provided", System.Net.HttpStatusCode.BadRequest);
            }

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

                if(country == null)
                {
                    _logger.LogWarning("Country not found");
                    return new ErrorResult("Country not found", System.Net.HttpStatusCode.NotFound);
                }

                _context.Countries.Remove(country);
                _context.SaveChanges();
                transaction.Commit();

                _logger.LogInformation("Country deleted successfully.");
                return new SuccessResult("Country deleted successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error ocured while deleting country");
                return new ErrorResult("Error ocured while deleting country", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<ICollection<GetCountryDTO>> GetAllCountries()
        {
            try
            {
                var countries = _context.Countries.AsNoTracking()
                    .Select(x => new GetCountryDTO
                    {
                        Name = x.Name,
                        Id = x.Id,
                    }).ToList();

                if (countries == null || !countries.Any())
                {
                    _logger.LogWarning("No countries found.");
                    return new ErrorDataResult<ICollection<GetCountryDTO>>("No countries found.", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<ICollection<GetCountryDTO>>(countries, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is Error ocurred while getting data");
                return new ErrorDataResult<ICollection<GetCountryDTO>>(data: null, "There is Error ocurred while getting data", false, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<GetCountryDTO> GetCountry(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _logger.LogWarning("Invalid Country data provided");
                return new ErrorDataResult<GetCountryDTO>(data: null,"Invalid Country data provided", false, System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var country = _context.Countries.AsNoTracking()
                    .Where(x => x.Id == Id)
                    .Select(x => new GetCountryDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).FirstOrDefault();

                if (country == null)
                {
                    _logger.LogWarning("Actor not found");
                    return new ErrorDataResult<GetCountryDTO>("Country not found", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<GetCountryDTO>(country, "Actor retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while retrieving the actor.");
                return new ErrorDataResult<GetCountryDTO>("An error occurred while retrieving the actor.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> UpdateCountryAsync(Guid Id, UpdateCountryDTO model)
        {
            if (Id == Guid.Empty || model == null)
            {
                _logger.LogWarning("Invalid input: Id or country data is null/empty.");
                return new ErrorResult("Invalid input: Id or country data is null/empty.", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = _context.Database.BeginTransaction();

            try
            {
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == Id);

                if (country == null)
                {
                    _logger.LogWarning("Country not found");
                    return new ErrorResult("Country not found", System.Net.HttpStatusCode.NotFound);
                }

                country.Name = model.Name;

                _context.Countries.Update(country);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"{country.Name} updated successfully.");
                return new SuccessResult($"{country.Name} updated successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Database error occurred while updating country.");
                return new ErrorResult("Database error occurred while updating country.", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "An error occurred while updating country.");
                return new ErrorResult("An error occurred while updating country.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
