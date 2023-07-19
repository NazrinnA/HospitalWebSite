using DAL.Repositories.Interfaces;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class TimeRepository:BaseRepository<Time,Hospital2DbContext>,ITimeRepository
    {
        private readonly Hospital2DbContext _context;

        public TimeRepository(Hospital2DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
