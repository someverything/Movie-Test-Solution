using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;
using UtilitiesDLL.Results.Concrete.SuccessResults;

namespace MovieTestSolution.Business.Concrete
{
    public class CountryManager : ICountryServices
    {
        private readonly ICountryDAL _countryDAL;
        private readonly ILogger<Country> _logger;
        private readonly IMapper _mapper;
        public CountryManager(ICountryDAL countryDAL, ILogger<Country> logger, IMapper mapper)
        {
            _countryDAL = countryDAL;
            _logger = logger;
            _mapper = mapper;
        }

        public IResult Create(CreateCountryDTO model)
        {
            var country = _countryDAL.CreateCountry(model);
            return new SuccessResult("Country created successfully", System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            await _countryDAL.DeleteCountryAsync(Id);
            return new SuccessResult("Country deleted successfully!", System.Net.HttpStatusCode.OK);

        }

        public IDataResult<ICollection<GetCountryDTO>> GetAll()
        {
            var countries = _countryDAL.GetAllCountries();
            return new SuccessDataResult<ICollection<GetCountryDTO>>(data: countries.Data, System.Net.HttpStatusCode.OK);
        }

        public IDataResult<GetCountryDTO> GetCountry(Guid Id)
        {
            var country = _countryDAL.GetCountry(Id);
            return new SuccessDataResult<GetCountryDTO>(country.Data, System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateCountryDTO model)
        {
            await _countryDAL.UpdateCountryAsync(Id, model);
            return new SuccessResult("Country updated successfully", System.Net.HttpStatusCode.OK);
        }
    }
}
