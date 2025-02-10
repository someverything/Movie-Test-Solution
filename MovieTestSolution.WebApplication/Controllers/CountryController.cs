using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.Entities.DTOs.CountryDTOs;

namespace MovieTestSolution.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _services;

        public CountryController(ICountryServices services)
        {
            _services = services;
        }

        [HttpPost]
        public IActionResult Create(CreateCountryDTO model)
        {
            _services.Create(model);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var result = _services.GetCountry(Id);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteById(Guid Id)
        {
            var result = await _services.DeleteAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _services.GetAll();
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, UpdateCountryDTO model)
        {
            await _services.UpdateAsync(Id, model);
            return Ok(model);
        }
    }
}
