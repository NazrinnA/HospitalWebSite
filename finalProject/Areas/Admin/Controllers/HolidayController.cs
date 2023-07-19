using AutoMapper;
using Business.Services.Intefaces;
using DAL.Repositories.Interfaces;
using Entities.Dtos.Holiday;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class HolidayController : Controller
    {
        private readonly IHolidayService _service;
        private readonly IMapper _mapper;


        public HolidayController(IMapper mapper, IHolidayService service)
        {
            _mapper = mapper;
            _service = service;
 
        }

        public async Task<IActionResult> Index()
        {
            HolidayGetDto getDto = await _service.Get();
            getDto.Permission = !getDto.Permission;
            HolidayPostDto postDto = new HolidayPostDto { Permission = getDto.Permission };
            await _service.Update(postDto);
            return RedirectToAction("Index","Doctor");
        }
       

    }
}

