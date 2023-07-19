﻿using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IDoctorService
    {
        Task<List<DoctorGetDto>> GetAllAsync(Expression<Func<Doctor, bool>> exp);
        Task<List<DoctorGetDto>> GetAllAsync();
        Task<List<DoctorGetDto>> GetAllAsync(Expression<Func<Doctor, bool>> exp, params string[] includes);
        Task<DoctorGetDto> GetbyId(int id);
        Task<DoctorGetDto> Get(Expression<Func<Doctor,bool>> exp,params string[] includes);
        Task CreateAsync(DoctorPostDto postDto);
        Task<bool> UpdateAsync(DoctorUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task AddHistory(ResHistory history, int id);
    }
}
