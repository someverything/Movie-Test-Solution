using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Business.Aиstract;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;
using UtilitiesDLL.Results.Concrete.SuccessResults;

namespace MovieTestSolution.Business.Concrete
{
    public class ActorManager : IActorServices
    {
        private readonly IActorDAL _actorDAL;
        private readonly ILogger<ActorManager> _logger;
        private readonly IMapper _mapper;

        public ActorManager(IActorDAL actorDAL, ILogger<ActorManager> logger, IMapper mapper)
        {
            _actorDAL = actorDAL;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IDataResult<CreateActorDTO>> CreateActorAsync(CreateActorDTO model)
        {
            var actor = await _actorDAL.CreateActorAsync(model);
            return new SuccessDataResult<CreateActorDTO>(data: actor.Data, "Actor created successfully", HttpStatusCode.OK);
        }

        public async Task<IResult> DeleteActorAsync(Guid id)
        {
            await _actorDAL.DeleteActorAsync(id);
            _logger.LogInformation($"Actor with Id {id} deleted successfully.", id);
            return new SuccessResult("Actor deleted successfully!", System.Net.HttpStatusCode.OK);
        }

        public IDataResult<GetActorDTO> GetActor(Guid Id)
        {
            var actor = _actorDAL.GetActor(Id);
            return new SuccessDataResult<GetActorDTO>(data: actor.Data, $"There is your Actor by Id: {Id}", HttpStatusCode.OK);
        }

        public IDataResult<ICollection<GetActorDTO>> GetAllActors()
        {
            var actors = _actorDAL.GetAllActors();
            return new SuccessDataResult<ICollection<GetActorDTO>>(data: actors.Data,"All data received successfully", HttpStatusCode.OK);
        }


        public async Task<IResult> UpdateActorAsync(Guid Id, UpdateActorDTO model)
        {
            await _actorDAL.UpdateActorAsync(Id, model);
            return new SuccessResult("Actor updated successfully", HttpStatusCode.OK);

        }
    }
}
