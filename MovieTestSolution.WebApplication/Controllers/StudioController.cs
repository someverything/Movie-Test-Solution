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
        private readonly IStudionServices _studionServices;

        public StudioController(IStudionServices studionServices)
        {
            _studionServices = studionServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudioDTO model)
        {
            await _studionServices.CreateAsync(model);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var result = _studionServices.Get(Id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _studionServices.DeleteAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _studionServices.GetAll();
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, UpdateStudioDTO model)
        {
            await _studionServices.UpdateAsync(Id, model);
            return Ok(model);
        }
    }
}
