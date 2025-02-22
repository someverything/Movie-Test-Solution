using MovieTestSolution.Entities.DTOs.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace MovieTestSolution.Business.Abstract
{
    public interface ICountryServices
    {
        IResult Create(CreateCountryDTO model);
        IDataResult<GetCountryDTO> GetCountry(Guid Id);
        IDataResult<ICollection<GetCountryDTO>> GetAll();
        Task<IResult> DeleteAsync(Guid Id);
        Task<IResult> UpdateAsync(Guid Id, UpdateCountryDTO model);
    }
}
