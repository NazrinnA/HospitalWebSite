
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]


public class HomeController : Controller
{
    private readonly IHomeService _service;
    private readonly IHomeRepository _repo;

    public HomeController(IHomeService service, IHomeRepository repo)
    {
        _service = service;
        _repo = repo;
    }
    //public async Task<IActionResult> Create()
    //{
    //    await _repo.CreateAsync(new Home
    //    {
    //        Image = "1f3d9bc9-b0f7-4ea5-a1f1-13be61fde95341a365a6-c18c-4c87-8022-00995f0389betestimonial-3",
    //        Slogan = "adassd",
    //        Title = "sdasasdas"
    //    });
    //    return Json("ok");
    //}
    public async Task<IActionResult> Index()
    {
        HomeGetDto home = await _service.Get();
        if (home == null)
        {
            return View();
        }
        return View(home);
    }
    public async Task<IActionResult> Update()
    {
        HomeGetDto getDto = await _service.Get();
        if (getDto == null) throw new NotFoundException(Messages.NotFound);
        HomeUpdateDto updateDto = new HomeUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(HomeUpdateDto updateDto)
    {
        updateDto.getDto = await _service.Get();
        if (!ModelState.IsValid && updateDto.postDto.formFile!=null)
        {
            return View(updateDto);
        }
        var result = await _service.UpdateAsync(updateDto);
        if (!result) return View(updateDto);
        return RedirectToAction(nameof(Index));
    }
}
