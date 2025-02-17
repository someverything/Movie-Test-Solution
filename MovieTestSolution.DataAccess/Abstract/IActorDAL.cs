using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface IActorDAL : IRepositoryBase<Actor>
    {
        Task<IDataResult<CreateActorDTO>> CreateActorAsync(CreateActorDTO model);
        Task<IResult> UpdateActorAsync(Guid id, UpdateActorDTO actor);
        IDataResult<GetActorDTO> GetActor(Guid id);
        IDataResult<ICollection<GetActorDTO>> GetAllActors();
        Task<IResult> DeleteActorAsync(Guid id);
    }
}

