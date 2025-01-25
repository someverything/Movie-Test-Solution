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
        public async Task CreateActorAsync(List<CreaateActorDTO> model)
        {
            await using var context = new AppDbContext();
            try
            {

            }
            catch (Exception ex)
            {

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
