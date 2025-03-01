using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MovieTestSolution.Business.Concrete;
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
using Xunit;

namespace MovieTestSolution.Tests.Business
{
    public class CountryManagerTest
    {
        private readonly Mock<ICountryDAL> _countrydalMock;
        private readonly CountryManager _countryManager;
        private readonly Mock<ILogger<Country>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;

        public CountryManagerTest()
        {
            _countrydalMock = new Mock<ICountryDAL>();
            _loggerMock = new Mock<ILogger<Country>>();
            _mapperMock = new Mock<IMapper>();

            _countryManager = new CountryManager(
                _countrydalMock.Object,
                _loggerMock.Object,
                _mapperMock.Object);

        }

        [Fact]
        public void Create_ShouldReturnSuccessResult()
        {
            var createCountryDTO = new CreateCountryDTO();
            _countrydalMock.Setup(dal => dal.CreateCountry(It.IsAny<CreateCountryDTO>()))
                .Returns(new SuccessResult("Country created successfully", System.Net.HttpStatusCode.OK));

            var result = _countryManager.Create(createCountryDTO);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResult()
        {
            var countryId = Guid.NewGuid();
            _countrydalMock.Setup(dal => dal.DeleteCountryAsync(countryId))
                .Returns(Task.FromResult<IResult>(new SuccessResult("Country deleted successfully!", System.Net.HttpStatusCode.OK)));

            var result = await _countryManager.DeleteAsync(countryId);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfCountries()
        {
            var countries = new List<GetCountryDTO> { new GetCountryDTO() };
            _countrydalMock.Setup(dal => dal.GetAllCountries())
                .Returns(new SuccessDataResult<ICollection<GetCountryDTO>>(countries, System.Net.HttpStatusCode.OK));

            var result = _countryManager.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetCountry_ShouldReturnSingleCountry()
        {
            var countryId = Guid.NewGuid();
            var countryDTO = new GetCountryDTO();
            _countrydalMock.Setup(dal => dal.GetCountry(countryId))
                .Returns(new SuccessDataResult<GetCountryDTO>(countryDTO, System.Net.HttpStatusCode.OK));

            var result = _countryManager.GetCountry(countryId);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessResult()
        {
            var countryId = Guid.NewGuid();
            var updateCountryDTO = new UpdateCountryDTO();
            _countrydalMock.Setup(dal => dal.UpdateCountryAsync(countryId, updateCountryDTO))
                .Returns(Task.FromResult<IResult>(new SuccessResult("Country updated successfully", System.Net.HttpStatusCode.OK)));

            var result = await _countryManager.UpdateAsync(countryId, updateCountryDTO);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }

    }
}
