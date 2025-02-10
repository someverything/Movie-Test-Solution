using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Core.Utilities.Results.Concrete.ErrorResult;
using MovieTestSolution.Core.Utilities.Results.Concrete.SuccessResult;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieTestSolution.DataAccess.Concrete.EntityFramework
{
    public class EFActorDAL : EFRepositoryBase<Actor, AppDbContext>, IActorDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Actor> _logger;

        public EFActorDAL(AppDbContext context, IMapper mapper, ILogger<Actor> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        //Create--------------------------
        public async Task<IDataResult<CreateActorDTO>> CreateActorAsync(CreateActorDTO model)
        {
            if (model == null)
            {
                _logger.LogWarning("CreateActorAsync: Invalid actor data provided.");
                return new ErrorDataResult<CreateActorDTO>("Invalid actor data", System.Net.HttpStatusCode.BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var actor = new Actor
                {
                    Name = model.Name,
                };

                await _context.Actors.AddAsync(actor);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var createdActor = new CreateActorDTO
                {
                    Name = model.Name,
                };

                _logger.LogInformation("CreateActorAsync: Actor created successfully.");
                return new SuccessDataResult<CreateActorDTO>("Actor created successfully.", System.Net.HttpStatusCode.OK);
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex, "CreateActorAsync: Database error occurred while creating actor.");
                return new ErrorDataResult<CreateActorDTO>("Database error occurred.", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateActorAsync: Error occurred while creating actor.");
                return new ErrorDataResult<CreateActorDTO>("An error occurred while creating the actor.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        //Delete--------------------------
        public async Task<IResult> DeleteActorAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                _logger.LogWarning("Id is empty");
                return new ErrorResult("Id is empty", System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var actor = _context.Actors.AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (actor == null)
                {
                    _logger.LogWarning("Actor not found");
                    return new ErrorResult("Actor not found", System.Net.HttpStatusCode.NotFound);
                }

                _context.Actors.Remove(entity: actor);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Actor deleted successfully.");
                return new SuccessResult("Actor deleted successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting actor.");
                return new ErrorResult("Error occurred while deleting actor.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        //Get-----------------------------
        public IDataResult<GetActorDTO> GetActor(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Id is empty");
                return new ErrorDataResult<GetActorDTO>("Actor not found", System.Net.HttpStatusCode.NotFound);
            }

            try
            {
                var actor = _context.Actors.AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new GetActorDTO
                    {
                        Name = x.Name,
                        Id = x.Id,
                    }).FirstOrDefault();

                if (actor == null)
                {
                    _logger.LogWarning("Actor not found");
                    return new ErrorDataResult<GetActorDTO>("Actor not found", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<GetActorDTO>(actor, "Actor retrieved successfully",System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while retrieving the actor.");
                return new ErrorDataResult<GetActorDTO>("An error occurred while retrieving the actor.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IDataResult<ICollection<GetActorDTO>> GetAllActors()
        {
            try
            {
                var actors = _context.Actors.AsNoTracking()
                    .Select(x => new GetActorDTO
                    {
                        Name = x.Name,
                        Id = x.Id,
                    }).ToList();

                if (actors == null || !actors.Any())
                {
                    _logger.LogWarning("No actors found.");
                    return new ErrorDataResult<ICollection<GetActorDTO>>("No actors found.", System.Net.HttpStatusCode.NotFound);
                }

                return new SuccessDataResult<ICollection<GetActorDTO>>(actors, "Actors retrieved successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the actors.");
                return new ErrorDataResult<ICollection<GetActorDTO>>("An error occurred while retrieving the actors.", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> UpdateActorAsync(Guid id, UpdateActorDTO actor)
        {
            if (actor == null || id == Guid.Empty)
            {
                _logger.LogWarning("Id is empty");
                return new ErrorResult("Invalid input: Id or actor is null/empty.", System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var existingActor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
                if (existingActor == null)
                {
                    _logger.LogWarning($"Actor with id {id} not found.");
                    return new ErrorResult($"Actor with id {id} not found.", System.Net.HttpStatusCode.NotFound);
                } 

                existingActor.Name = actor.Name;
                
                _context.Actors.Update(existingActor);
                await _context.SaveChangesAsync();

                return new SuccessResult("Actor updated successfuly.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex ,$"Failed to update actor: {id}");
                return new ErrorResult($"Failed to update actor: {id}", System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
