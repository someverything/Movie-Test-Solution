using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface IActorDAL : IRepositoryBase<Actor>
    {
        Task CreateActorAsync(List<CreaateActorDTO> model);
        Task UpdateActorAsybc(Guid Id, UpdateActorDTO actor);
        GetActorDTO GetActorDTO(Guid Id);
        ICollection<GetActorDTO> GetActors();
        Task DeleteActorAsync(Guid Id);
        
    }
}
