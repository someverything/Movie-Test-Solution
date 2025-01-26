using AutoMapper;
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

        public Task DeleteActorAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public GetActorDTO GetActorDTO(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<GetActorDTO> GetActors()
        {
            throw new NotImplementedException();
        }

        public Task UpdateActorAsybc(Guid Id, UpdateActorDTO actor)
        {
            throw new NotImplementedException();
        }
    }
}
