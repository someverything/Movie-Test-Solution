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
        public async Task<IActionResult> CreateAsync(CreateActorDTO model)
        {
            await _actorServices.CreateActorAsync(model);
            return Ok(model);
        }
    }
}
