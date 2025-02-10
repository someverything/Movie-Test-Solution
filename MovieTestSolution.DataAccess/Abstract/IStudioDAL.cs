using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface IStudioDAL : IRepositoryBase<Studio>
    {
        Task<IDataResult<CreateStudioDTO>> CreateAsync(CreateStudioDTO model);
        Task<IResult> UpdateAsync(Guid Id, UpdateStudioDTO actor);
        IDataResult<GetStudioDTO> Get(Guid Id);
        IDataResult<ICollection<GetStudioDTO>> GetAll();
        Task<IResult> DeleteAsync(Guid Id);
    }
}
