using AutoMapper;
using Castle.Core.Logging;
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

        public IResult CreateCountry(CreateActorDTO model)
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
                _logger.LogError("There is Error ocurred while creating Country");
                return new ErrorResult("There is Error ocurred while creating Country", false, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public Task<IResult> DeleteCountryAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<GetCountryDTO> GetAllCountries()
        {
            throw new NotImplementedException();
        }

        public IDataResult<GetCountryDTO> GetCountry(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateCountryAsync(Guid Id, UpdateCountryDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
