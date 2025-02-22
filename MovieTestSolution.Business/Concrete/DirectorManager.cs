using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.DirectoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;
using UtilitiesDLL.Results.Concrete.SuccessResults;

namespace MovieTestSolution.Business.Concrete
{
    public class DirectorManager : IDirectorServices
    {
        private readonly IDirectorDAL _directorDAL;
        private readonly ILogger<Director> _logger;
        private readonly IMapper _mapper;

        public DirectorManager(IDirectorDAL directorDAL, ILogger<Director> logger, IMapper mapper)
        {
            _directorDAL = directorDAL;
            _logger = logger;
            _mapper = mapper;
        }

        public IResult Create(CreateDirectorDTO model)
        {
            _directorDAL.Create(model);
            return new SuccessResult("Director created successfully", System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            await _directorDAL.DeleteAsync(Id);
            return new SuccessResult("Director deleted successfully", System.Net.HttpStatusCode.OK);
        }

        public IDataResult<GetDirectorDTO> Get(Guid Id)
        {
            var director = _directorDAL.Get(Id);
            return new SuccessDataResult<GetDirectorDTO>(data:  director.Data, System.Net.HttpStatusCode.OK);
        }

        public IDataResult<ICollection<GetDirectorDTO>> GetAll()
        {
            var result = _directorDAL.GetAll();
            return new SuccessDataResult<ICollection<GetDirectorDTO>>(data: result.Data, System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateDirectorDTO model)
        {
            await _directorDAL.UpdateAsync(Id, model);
            return new SuccessResult("Data updated successfully", System.Net.HttpStatusCode.OK);
        }
    }
}
