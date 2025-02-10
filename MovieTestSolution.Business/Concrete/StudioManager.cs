using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieTestSolution.Business.Abstract;
using MovieTestSolution.Core.Utilities.Results.Abstract;
using MovieTestSolution.Core.Utilities.Results.Concrete.SuccessResult;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.DTOs.StudioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Business.Concrete
{
    public class StudioManager : IStudionServices
    {
        private readonly IStudioDAL _studioDAL;
        private readonly ILogger<Studio> _logger;
        private readonly IMapper _mapper;

        public StudioManager(IStudioDAL studioDAL, ILogger<Studio> logger, IMapper mapper)
        {
            _studioDAL = studioDAL;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IDataResult<CreateStudioDTO>> CreateAsync(CreateStudioDTO model)
        {
            var studio = await _studioDAL.CreateAsync(model);
            return new SuccessDataResult<CreateStudioDTO>(data: studio.Data, studio.Message, System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            _logger.LogInformation("Delete method started");
            await _studioDAL.DeleteAsync(Id);
            _logger.LogInformation("Stuido deleted successfully");
            return new SuccessResult("Stuido deleted successfully", System.Net.HttpStatusCode.OK);
        }

        public IDataResult<GetStudioDTO> Get(Guid Id)
        {
            _logger.LogInformation("Get method started");
            var studio = _studioDAL.Get(Id);
            return new SuccessDataResult<GetStudioDTO>(data: studio.Data, System.Net.HttpStatusCode.OK);
        }

        public IDataResult<ICollection<GetStudioDTO>> GetAll()
        {
            var studios = _studioDAL.GetAll();
            return new SuccessDataResult<ICollection<GetStudioDTO>>(data: studios.Data, System.Net.HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateStudioDTO model)
        {
            await _studioDAL.UpdateAsync(Id, model);
            return new SuccessResult($"Data by Id {Id} updated successfully", System.Net.HttpStatusCode.OK);
        }
    }
}
