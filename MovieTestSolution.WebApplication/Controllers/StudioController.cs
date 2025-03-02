using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.Entities.DTOs.StudioDTOs;

namespace MovieTestSolution.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IStudioServices _studioServices;

        public StudioController(IStudioServices studioServices)
        {
            _studioServices = studioServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudioDTO model)
        {
            await _studioServices.CreateAsync(model);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var result = _studioServices.Get(Id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _studioServices.DeleteAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _studioServices.GetAll();
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, UpdateStudioDTO model)
        {
            await _studioServices.UpdateAsync(Id, model);
            return Ok(model);
        }
    }
}
