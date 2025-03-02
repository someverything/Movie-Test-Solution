using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.Business.Abstract
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
