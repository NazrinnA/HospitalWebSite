
using AutoMapper;
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

[Area("admin")]
[Authorize(Roles = "admin")]
public class AboutController : Controller
{
    private readonly IAboutService _service;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public AboutController(IAboutService service, IMapper mapper, IWebHostEnvironment env, IHolidayService hol)
    {
        _service = service;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        AboutGetDto getDto = await _service.Get();
        if (getDto is null) return View();
        return View(getDto);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task< IActionResult> Create(AboutPostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (!postDto.formFile.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Formfile", "please send image");
            return View(postDto);
        }
      await   _service.CreateAsync(postDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        AboutGetDto getDto = await _service.Get();
        if (getDto is null) throw new NotFoundException(Messages.NotFound);
        AboutUpdateDto updateDto = new AboutUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(AboutUpdateDto updateDto)
    {
        AboutGetDto getDto = await _service.Get();
        updateDto.getDto = getDto;
        if (!ModelState.IsValid && updateDto.aboutPost.formFile!=null)
        {
            return View(updateDto);
        }
        var result = await _service.UpdateAsync(updateDto);
        if (!result)
        {
            ModelState.AddModelError("", "Please send image");
            return View(updateDto);
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
