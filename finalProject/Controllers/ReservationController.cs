using AutoMapper;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDoctorService _service;
        private readonly IReservationService _res;
        private readonly IPositionService _position;
        private readonly IHolidayService _holiday;

        public ReservationController(UserManager<AppUser> userManager, IDoctorService service, IReservationService res, IPositionService position, IHolidayService holiday)
        {
            _userManager = userManager;
            _service = service;
            _res = res;
            _position = position;
            _holiday = holiday;
        }
        public IActionResult Holiday()
        {
            return View();
        }
        public async Task< IActionResult> Index()
        {
          if(await _holiday.Check())
            {
                return RedirectToAction(nameof(Holiday));
            };
         //   ViewBag.Doctors = await _service.GetAllAsync(d => !d.IsDeleted);
            ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ReservationDto rez)
        {
         //   ViewBag.Doctors = await _service.GetAllAsync(d => !d.IsDeleted);
            ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
            List<DoctorGetDto> getDtos = await _service.GetAllAsync(d => d.PositionId == rez.PositionId && !d.IsDeleted, "Position","rezervs");
    //        DoctorGetDto doc = await _service.Get(d=>d.Id==rez.DoctorId && !d.IsDeleted, "rezervs","Position");
            if (getDtos == null)
            {
                ModelState.AddModelError("", "Doctor not found");
                return View();
            }
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "Please first login or register");
                return View();
            }
           
            return RedirectToAction(nameof(Next1), rez);
        }
        public async Task<IActionResult> Next1(ReservationDto rez)
        {
            ViewBag.Doctors = await _service.GetAllAsync(d => !d.IsDeleted && d.PositionId==rez.PositionId,"rezervs","Position");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Next2(ReservationDto rez)
        {
            DoctorGetDto getDto = await _service.Get(d => d.Id == rez.DoctorId && !d.IsDeleted, "rezervs", "Position");
            rez.getDto = getDto;
            return RedirectToAction(nameof(Next),rez);
        }
        public async Task<IActionResult> Next(ReservationDto rez)
        {
            DoctorGetDto getDto = await _service.Get(d => d.Id == rez.DoctorId && !d.IsDeleted, "rezervs", "Position");
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            rez.UserName = user.UserName;
            rez.UserEmail = user.Email;
            rez.getDto = getDto;
            return View(rez);
        }
        public async Task<IActionResult> Rezerv(int id, string time, string user)
        {
            DoctorGetDto doc = await _service.GetbyId(id);
            foreach (var item in doc.rezervs)
            {
                if (item.Time == time)
                {
                    item.Busy = !item.Busy;
                }
            };
            DoctorUpdateDto updateDto = new DoctorUpdateDto() { getDto = doc };
            await _service.UpdateAsync(updateDto);
            return RedirectToAction("Index","ResHistory");
        }

    }
}
