using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService _service;
        private readonly IReservationService _res;

        public HomeController(IHomeService service, IReservationService res)
        {
            _service = service;
            _res = res;
        }

        public  async Task<IActionResult> Index()
        {
            HomeGetDto getdto = await _service.Get();
             await _res.Refresh();

            if(getdto == null)
            {
                return View();
            }
            HomeSearchDto searchDto = new HomeSearchDto{ getDto = getdto };
            return View(searchDto);
        }
        public async  Task<IActionResult>Hide()
        {
            return Json("ok");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}