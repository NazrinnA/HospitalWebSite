using AutoMapper;
using Business.Services.Intefaces;
using DAL.Repositories.Interfaces;
using Entities.Concrete.Models;
using Entities.Dtos.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class TimeManager : ITimeService
    {
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public TimeManager(ITimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create()
        {
            List<string> time = new List<string>();
            //{ "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00" };
            time.Add("09:00");
            time.Add("10:00");
            time.Add("11:00");
            time.Add("12:00");
            time.Add("13:00");
            time.Add("14:00");
            time.Add("15:00");
            time.Add("16:00");
            time.Add("17:00");
            time.Add("18:00");
            time.Add("19:00");
            time.Add("20:00");
            time.Add("21:00");
            foreach (var item in time)
            {
                Time timee = new Time { time = item };
                await _repository.CreateAsync(timee);
            }
        }

        public async Task<List<TimeGetDto>> Get()
        {
            List<Time> times = await _repository.GetAllAsnyc();
            List<TimeGetDto> getDtos = _mapper.Map<List<TimeGetDto>>(times);
            return getDtos;

        }
        public async Task<Time> GetById(int id)
        {
            Time time = await _repository.Get(t => t.Id == id);
            return time;
        }
        public async Task<int> GetByName(string time)
        {
            Time timee = await _repository.Get(t => t.time==time);
            return timee.Id;
        }
    }
}
