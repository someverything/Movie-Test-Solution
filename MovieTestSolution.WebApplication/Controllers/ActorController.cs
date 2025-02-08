using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTestSolution.Business.Aиstract;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;

namespace MovieTestSolution.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorServices _actorServices;

        public ActorController(IActorServices actorServices)
        {
            _actorServices = actorServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateActorAsync(CreateActorDTO model)
        {
            await _actorServices.CreateActorAsync(model);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetActorById(Guid Id)
        {
            var result = _actorServices.GetActor(Id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActorById(Guid Id)
        {
            var result = await _actorServices.DeleteActorAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllActors()
        {
            var result = _actorServices.GetAllActors();
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateActor(Guid Id, UpdateActorDTO model)
        {
            await _actorServices.UpdateActorAsync(Id, model);
            return Ok(model);
        }
    }
}

