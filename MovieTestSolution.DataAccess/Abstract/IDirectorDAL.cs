using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using MovieTestSolution.Entities.DTOs.DirectoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface IDirectorDAL : IRepositoryBase<Director>
    {
        IResult Create(CreateDirectorDTO model);
        IDataResult<GetDirectorDTO> Get(Guid Id);
        IDataResult<ICollection<GetDirectorDTO>> GetAll();
        Task<IResult> DeleteAsync(Guid Id);
        Task<IResult> UpdateAsync(Guid Id, UpdateDirectorDTO model);
    }
}
