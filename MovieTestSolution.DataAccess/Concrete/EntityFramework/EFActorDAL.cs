using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<CreaateActorDTO> CreateActorAsync(List<CreaateActorDTO> model)
        {
            try
            {
                if (model == null || !model.Any())
                {
                    throw new ArgumentException("Model is empty.");
                }

                var actor = new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = model.First().Name,
                };

                await _context.Actors.AddAsync(actor);
                await _context.SaveChangesAsync();

                return _mapper.Map<CreaateActorDTO>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating actor.");
                throw new InvalidOperationException("Error while creating actor.", ex);
            }
        }

        public async Task DeleteActorAsync(Guid Id)
        {
            var actor = _context.Actors.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            if (actor != null) _context.Actors.Remove(actor);
            await (_context.SaveChangesAsync());
        }

        public GetActorDTO GetActor(Guid Id)
        {
            if (Id == Guid.Empty) throw new ArgumentException("Invalid actor ID provided");

            var actor = _context.Actors.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            
            if(actor == null)  throw new ArgumentException("There is no data");

            return _mapper.Map<GetActorDTO>(actor);
        }

        public ICollection<GetActorDTO> GetAllActors()
        {
            throw new NotImplementedException();
        }

        public Task UpdateActorAsybc(Guid Id, UpdateActorDTO actor)
        {
            throw new NotImplementedException();
        }
    }
}
