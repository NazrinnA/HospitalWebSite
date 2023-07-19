using AutoMapper;
using Business.Services.Intefaces;
using DAL.Repositories.Interfaces;
using Entities.Concrete.Models;
using Entities.Dtos.Holiday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class HolidayManager : IHolidayService
    {
        private readonly IHolidayRepository _repository;
        private readonly IMapper _mapper;

        public HolidayManager(IHolidayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HolidayGetDto> Get()
        {
            Holiday holidays = await _repository.Get();
            HolidayGetDto getDto = _mapper.Map<HolidayGetDto>(holidays);
            return getDto;
        }
        public async Task Update(HolidayPostDto holiday)
        {
            Holiday hol = await _repository.Get();
             hol.Permission=holiday.Permission;
            _repository.Update(hol);
        }
        public async Task<bool> Check()
        {
            Holiday hol = await _repository.Get();
            return hol.Permission;
        }
    }
}
