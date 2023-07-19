﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface ISettingService
    {
        Task<SettingGetDto> Get();
        Task UpdateAsync(SettingUpdateDto updateDto);
        Task Create();
    }
}
