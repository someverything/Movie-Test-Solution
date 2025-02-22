using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.Entities.DTOs.MovieDTOs;

namespace MovieTestSolution.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieServices _movieServices;

        public MovieController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateMovieDTO model)
        {
            var result = await _movieServices.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _movieServices.GetByIdAsync(Id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movieServices.GetAllAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _movieServices.DeleteAsync(Id);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(Guid Id, UpdateMovieDTO model)
        {
            await _movieServices.UpdateAsync(model, Id);
            return Ok(model);
        }
    }
}
