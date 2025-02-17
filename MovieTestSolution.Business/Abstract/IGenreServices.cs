using MovieTestSolution.Entities.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.Business.Abstract
{
    public interface IGenreServices
    {
        Task<IDataResult<CreateGenreDTO>> CreateAsync(CreateGenreDTO model);
        Task<IResult> DeleteAsync(Guid Id);
        IDataResult<GetGenreDTO> Get(Guid Id);
        IDataResult<ICollection<GetGenreDTO>> GetAll();
        Task<IResult> UpdateAsync(Guid Id, UpdateGenreDTO model);
    }
}
