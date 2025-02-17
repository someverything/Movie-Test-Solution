using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface IGenreDAL : IRepositoryBase<Genre>
    {
        Task<IDataResult<CreateGenreDTO>> CreateAsync(CreateGenreDTO model);
        Task<IResult> UpdateAsync(Guid id, UpdateGenreDTO actor);
        IDataResult<GetGenreDTO> Get(Guid id);
        IDataResult<ICollection<GetGenreDTO>> GetAll();
        Task<IResult> DeleteAsync(Guid id);
    }
}
