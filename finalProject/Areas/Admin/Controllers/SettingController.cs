
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]

[Authorize(Roles = "admin")]

public class SettingController : Controller
{
    private readonly ISettingService _service;

    public SettingController(ISettingService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        SettingGetDto getDto = await _service.Get();
        if (getDto is null) return View();
        return View(getDto);
    }
    public async Task<IActionResult> Create()
    {
        await _service.Create();
        return Json("ok");
    }
    public async Task<IActionResult> Update()
    {
        SettingGetDto getDto = await _service.Get();
        if (getDto == null) throw new NotFoundException(Messages.NotFound);
        SettingUpdateDto updateDto = new SettingUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(SettingUpdateDto updateDto)
    {
        updateDto.getDto = await _service.Get();
        if (!ModelState.IsValid)
        {
            return View(updateDto);
        }
        await _service.UpdateAsync(updateDto);
        return RedirectToAction(nameof(Index));
    }
}

