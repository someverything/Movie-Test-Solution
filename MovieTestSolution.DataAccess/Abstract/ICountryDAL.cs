using MovieTestSolution.Core.DataAccess.EntityFramework;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.ActorsDTOs;
using MovieTestSolution.Entities.DTOs.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Abstract
{
    public interface ICountryDAL : IRepositoryBase<Country>
    {
        IResult CreateCountry(CreateCountryDTO model);
        IDataResult<GetCountryDTO> GetCountry(Guid Id);
        IDataResult<ICollection<GetCountryDTO>> GetAllCountries();
        Task<IResult> DeleteCountryAsync(Guid Id);
        Task<IResult> UpdateCountryAsync(Guid Id, UpdateCountryDTO model);
    }
}
