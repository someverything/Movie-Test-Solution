using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Business.Aиstract;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Core.Utilities.Results.Concrete.ErrorResult;
using MovieTestSolution.Core.Utilities.Results.Concrete.SuccessResult;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            if (model == null)
            {
                _logger.LogWarning("Invalid Actor data provided");
                return new ErrorDataResult<CreateActorDTO>(
                            data: null,
                            "Invalid actor data",
                            false,
                            System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                _logger.LogInformation("Starting to create Article");

                await _actorDAL.CreateActorAsync(model);
                
                _logger.LogInformation("Actor created");
                return new SuccessDataResult<CreateActorDTO>(model, "Actor created properly", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocured while creating actor");
                return new ErrorDataResult<CreateActorDTO>(model, HttpStatusCode.BadRequest);
            }
        }

        public Task<IResult> DeleteActorAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<GetActorDTO> GetActor(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _logger.LogInformation("Id is empty");
                return new ErrorDataResult<GetActorDTO>(data: null, "Id is empty", false, HttpStatusCode.BadRequest);
            }

            try
            {
                var actor = _actorDAL.GetActor(Id);

                if (actor == null)
                {
                    _logger.LogWarning("Actor not found in database.");
                    return new ErrorDataResult<GetActorDTO>("Actor not found", HttpStatusCode.NotFound);
                }

                var result = new GetActorDTO
                {
                    Name = actor.Data.Name,
                    Id = Id,
                };

                if (result == null)
                {
                    _logger.LogWarning("Actor not found");
                    return new ErrorDataResult<GetActorDTO>("Actor not found", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<GetActorDTO>(result, "Actor retrieved successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the actor.");
                return new ErrorDataResult<GetActorDTO>("An error occurred while retrieving the actor.", HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<ICollection<GetActorDTO>> GetAllActors()
        {
            throw new NotImplementedException();
        }


        public Task<IResult> UpdateActorAsync(Guid id, UpdateActorDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
