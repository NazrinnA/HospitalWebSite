using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using Entities.Concrete.Models;
using Entities.Dtos.ResHistory;

namespace Business.Services.Implementations
{
    public class ReservationManager : IReservationService
    {
        private readonly IDoctorService _doctorService;
        private readonly IResHistoryService _res;
        private readonly IMapper _mapper;


        public ReservationManager(IDoctorService doctorService, IResHistoryService res, IMapper mapper)
        {
            _doctorService = doctorService;
            _res = res;
            _mapper = mapper;
        }

        public async Task<bool> Okei()
        {
            List<DoctorGetDto> docs = await _doctorService.GetAllAsync(d => !d.IsDeleted, "rezervs");
            if (docs is null) return false;
            foreach (DoctorGetDto doc in docs)
            {
                foreach (var item in doc.rezervs)
                {
                    if (item.Busy)
                    {
                        Mail.SendMessage(item.UserEmail, "Rezervasiya", $"{item.date.Day} si {doc.Name} həkim üçün etdiyiniz rezervasiya qəbul olundu,zəhmət olmasa gecikməyin.Təşəkkürlər;)");
                    }
                }

            }
            return true;
        }

        public async Task<bool> Reserv(int id, string time, string user)
        {
            DoctorGetDto doc = await _doctorService.GetbyId(id);
            if (doc is null) return false;
            foreach (var item in doc.rezervs)
            {
                if (item.Time == time)
                {
                    item.Busy = !item.Busy;
                    if (item.Busy)
                    {
                        item.UserEmail = user;
                        item.date = DateTime.Now;
                        ResHistory history = new ResHistory() { UserEmail = user, date = item.date, Doctor = _mapper.Map<Doctor>(doc) };
                        await _doctorService.AddHistory(history, doc.Id);
                    }
                    if (!item.Busy)
                    {
                        Mail.SendMessage(user, "Rezervasiya", $"Teessufle bildiririk ki,{item.date.Day} si {doc.Name} üçün etdiyiniz rezervasiya qebul olunmadi");
                    }
                }
            };
            DoctorUpdateDto updateDto = new DoctorUpdateDto() { getDto = doc };
            await _doctorService.UpdateAsync(updateDto);
            return true;
        }
        public async Task<bool> Refresh()
        {
            List<DoctorGetDto> docs = await _doctorService.GetAllAsync(d => !d.IsDeleted, "rezervs", "history");
            if (docs == null) return true;
            foreach (var doc in docs)
            {
                foreach (var rez in doc.rezervs)
                {
                    if (rez.Busy)
                    {
                        List<ResGetDto> res = await _res.GetAllAsync(r => r.date == rez.date && r.UserEmail == rez.UserEmail);
                        ResGetDto resGet = res.FirstOrDefault();
                        if (resGet.date.Day != DateTime.Today.Day)
                        {
                            rez.Busy = false;
                        }
                    }
                }
                DoctorUpdateDto update = new DoctorUpdateDto { getDto = doc };
                var result = await _doctorService.UpdateAsync(update);
            }
            return true;
        }
    }
}
