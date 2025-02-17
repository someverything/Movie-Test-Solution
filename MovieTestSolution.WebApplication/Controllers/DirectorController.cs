using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using MovieTestSolution.Entities.DTOs.DirectoDTOs;

namespace MovieTestSolution.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorServices _services;

        public DirectorController(IDirectorServices services)
        {
            _services = services;
        }


        [HttpPost]
        public IActionResult Create(CreateDirectorDTO model)
        {
            _services.Create(model);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var result = _services.Get(Id);
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
        public async Task<IActionResult> Update(Guid Id, UpdateDirectorDTO model)
        {
            await _services.UpdateAsync(Id, model);
            return Ok(model);
        }
    }
}
