using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.Business.Abstract
{
    public interface IStudioServices
    {
        Task<IDataResult<CreateStudioDTO>> CreateAsync(CreateStudioDTO model);
        Task<IResult> UpdateAsync(Guid Id, UpdateStudioDTO model);
        IDataResult<GetStudioDTO> Get(Guid Id);
        IDataResult<ICollection<GetStudioDTO>> GetAll();
        Task<IResult> DeleteAsync(Guid Id);
    }
}
