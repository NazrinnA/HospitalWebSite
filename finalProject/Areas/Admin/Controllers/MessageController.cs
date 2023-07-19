﻿
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]

public class MessageController : Controller
{
    private readonly IMessageService _service;

    public MessageController(IMessageService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int currentpage = 1, int take = 6)
    {
        List<MessageGetDto> messages = await _service.GetAllAsync(m => !m.IsDeleted&& m.IsActive==null);
        int count=messages.Count;
        if (messages == null) { return View(); }
        messages = messages
                 .OrderByDescending(d => d.Id)
                .Skip((currentpage - 1) * take)
                .Take(take)
                .ToList();
        int pageCount = (int)Math.Ceiling((decimal)count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<MessageGetDto> pagination = new PaginationDto<MessageGetDto>
        {
            Models = messages,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };
        return View(pagination);
    }
    public async Task<IActionResult> Delete(int id)
    {
      var result=  await _service.DeleteAsync(id);
        if (!result) throw new NotFoundException(Messages.NotFound);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Deleted()
    {
        List<MessageGetDto> getDtos = await _service.GetAllAsync(m => m.IsDeleted);
        if (getDtos == null) return View();
        return View(getDtos);
    }
    public async Task<IActionResult> Restore(int id)
    {
      var result=  await _service.RestoreAsync(id);
        if (!result) throw new NotFoundException(Messages.NotFound);
        return RedirectToAction(nameof(Deleted));
    }
    public async Task<IActionResult> Accept(int id)
    {
        var result = await _service.RestoreAsync(id);
        if (!result) throw new NotFoundException(Messages.NotFound);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Succeded(int currentpage = 1, int take = 6)
    {
        List<MessageGetDto> messages = await _service.GetAllAsync(m => !m.IsDeleted);
        int count=messages.Count;
        if (messages == null) { return View(); }
        messages = messages
                 .OrderByDescending(d => d.Id)
                .Skip((currentpage - 1) * take)
                .Take(take)
                .ToList();
        int pageCount = (int)Math.Ceiling((decimal)count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<MessageGetDto> pagination = new PaginationDto<MessageGetDto>
        {
            Models = messages,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };
        return View(pagination);
    }
}

