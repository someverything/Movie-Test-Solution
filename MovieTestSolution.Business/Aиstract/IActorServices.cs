using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Business.Aиstract
{
    public interface IActorServices
    {
        Task<IDataResult<CreateActorDTO>> CreateActorAsync(CreateActorDTO model);
        Task<IResult> UpdateActorAsync(Guid id, UpdateActorDTO model);
        IDataResult<GetActorDTO> GetActor(Guid Id);
        IDataResult<ICollection<GetActorDTO>> GetAllActors();
        Task<IResult> DeleteActorAsync(Guid id);
    }
}
