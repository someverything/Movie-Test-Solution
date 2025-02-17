using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.Entities.DTOs.GenreDTOs;

namespace MovieTestSolution.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreServices _services;

        public GenreController(IGenreServices services)
        {
            _services = services;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAsync(CreateGenreDTO model)
        {
            await _services.CreateAsync(model);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Guid Id)
        {
            var result = _services.Get(Id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _services.GetAll();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _services.DeleteAsync(Id);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(UpdateGenreDTO model, Guid Id)
        {
            var result = await _services.UpdateAsync(Id, model);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
