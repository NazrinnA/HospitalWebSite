using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Core.Entities.Concrete;
using Business.Services.Intefaces;
using System.Xml.Linq;
using Core.Utilities;
using Entities.Dtos.ResHistory;
using Entities.Concrete.Models;
using AutoMapper;

namespace finaProject.Areas.doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "doctor")]
    public class RezervationController : Controller
    {
        private readonly IDoctorService _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IResHistoryService _history;
        private readonly IMapper _mapper;

        public RezervationController(UserManager<AppUser> userManager, IDoctorService service, IResHistoryService history, IMapper mapper)
        {
            _userManager = userManager;
            _service = service;
            _history = history;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(name);
            DoctorGetDto getDto = await _service.Get(d => d.Email == name, "rezervs");
            if (getDto == null)
            {
                return View();
            }
            return View(getDto);
        }
        public async Task<IActionResult> Rezerv(int id, string time, string user)
        {
            DoctorGetDto getDto = await _service.Get(d => d.Id == id, "Icons", "rezervs");

            foreach (var item in getDto.rezervs)
            {
                if (item.Time == time)
                {
                    item.Busy = !item.Busy;
                    if (user == null && item.Busy)
                    {
                        AppUser userr = await _userManager.FindByNameAsync(User.Identity.Name);
                        item.UserEmail = userr.Email;
                        item.date = DateTime.Now;
                        ResHistory history = new ResHistory() { UserEmail = userr.Email, date = item.date, Doctor = _mapper.Map<Doctor>(getDto) };
                        await _service.AddHistory(history, getDto.Id);
                    }
                    if (!item.Busy && user != null) Mail.SendMessage(user, "Rezervasiya", $"Təəssüflə bildiririk ki,{item.date.Day} günü üçün {getDto.Name} həkimə etdiyiniz rezervasiya qəbul olunmadı");
                }
            };
            DoctorUpdateDto dto = new DoctorUpdateDto { getDto = getDto };
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Okei()
        {
            List<DoctorGetDto> docs = await _service.GetAllAsync(d => !d.IsDeleted);
            foreach (DoctorGetDto doc in docs)
            {
                foreach (var item in doc.rezervs)
                {
                    if (item.Busy)
                        Mail.SendMessage(item.UserEmail, "Rezervasiya", $"{item.date.Day} günü üçün {doc.Name} həkimə etdiyiniz rezervasiya qəbul olundu,zehmət olmasa gecikməyin.Təşəkkürlər;)");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> History(int currentpage = 1, int take = 7)
        {
            string name = User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(name);
            List<ResGetDto> getDtos = await _history.GetAllAsync(r => r.Doctor.Email == user.Email);
            int count = getDtos.Count;

            if (getDtos == null) return View();
            getDtos = getDtos
                   .OrderByDescending(d => d.Id)
                   .Skip((currentpage - 1) * take)
                   .Take(take)
                   .ToList();
            int pageCount = (int)Math.Ceiling((decimal)count / take);
            if (pageCount == 0) pageCount = 1;
            PaginationDto<ResGetDto> pagination = new PaginationDto<ResGetDto>
            {
                Models = getDtos,
                CurrentPage = currentpage,
                PageCount = pageCount,
                Next = currentpage < pageCount,
                Previous = currentpage > 1
            };

            return View(pagination);
        }
    }
}
