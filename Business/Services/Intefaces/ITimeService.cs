using Entities.Concrete.Models;
using Entities.Dtos.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface ITimeService
    {
        Task Create();
        Task<List<TimeGetDto>> Get();
        public Task<Time> GetById(int id);
        public Task<int> GetByName(string time);
    }
}
