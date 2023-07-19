

using AutoMapper;
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]

public class ServiceController : Controller
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        List<ServiceGetDto> getDtos = await _service.GetAllAsync();
        if (getDtos is null) return View();
        return View(getDtos);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ServicePostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _service.CreateAsync(postDto);  
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {

        ServiceGetDto getDto =  await _service.GetbyId(id);
        if (getDto is null) throw new NotFoundException(Messages.NotFound);
        ServiceUpdateDto updateDto = new ServiceUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ServiceUpdateDto updateDto)
    {
        updateDto.getDto = await _service.GetbyId(updateDto.getDto.Id);
        if (!ModelState.IsValid)
        {
            return View(updateDto);
        }

       await _service.UpdateAsync(updateDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
       var result=  await _service.DeleteAsync(id);
        if (!result) throw new NotFoundException(Messages.NotFound);
        return RedirectToAction(nameof(Index));
    }
}

