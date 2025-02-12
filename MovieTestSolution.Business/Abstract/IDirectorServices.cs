using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Entities.DTOs.DirectoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Business.Abstract
{
    public interface IDirectorServices
    {
        IResult Create(CreateDirectorDTO model);
        IDataResult<GetDirectorDTO> Get(Guid Id);
        IDataResult<ICollection<GetDirectorDTO>> GetAll();
        Task<IResult> DeleteAsync(Guid Id);
        Task<IResult> UpdateAsync(Guid Id, UpdateDirectorDTO model);
    }
}
