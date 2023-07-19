using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IPositionService
    {
        Task<List<PositionGetDto>> GetAllAsync(Expression<Func<Position, bool>> exp);
        Task<PositionGetDto> GetbyId(int id);
        Task CreateAsync(PositionPostDto postDto);
        Task<bool> UpdateAsync(PositionUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
