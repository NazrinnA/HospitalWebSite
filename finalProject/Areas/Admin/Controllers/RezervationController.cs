
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Entities.Concrete;
using Core.Utilities;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Area("admin")]
[Authorize(Roles = "admin")]

public class RezervationController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly IReservationService _service;
    private readonly UserManager<AppUser> _user;

    public RezervationController(IDoctorService doctorService, IReservationService service, UserManager<AppUser> user)
    {
        _doctorService = doctorService;
        _service = service;
        _user = user;
    }
    public async Task<IActionResult> Index(int currentpage = 1, int take = 7)
    {
        List<DoctorGetDto> getDtos = await _doctorService.GetAllAsync(d => !d.IsDeleted);
        if (getDtos == null)
        {
            return View();
        }
        int count = getDtos.Count;
        getDtos = getDtos
            .OrderByDescending(d => d.Id)
            .Skip((currentpage - 1) * take)
            .Take(take)
            .ToList();
        int pageCount = (int)Math.Ceiling((decimal)count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<DoctorGetDto> pagination = new PaginationDto<DoctorGetDto>
        {
            Models = getDtos,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };
        return View(pagination);
    }
    public async Task<IActionResult> Rezerv(int id, string time, string user)
    {
        if (user == null) 
        {
            AppUser userr = await _user.FindByNameAsync(User.Identity.Name);
            user = userr.Email;
        }
        var result = await _service.Reserv(id, time, user);
        if (!result) throw new NotFoundException(Messages.NotFound);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Okei()
    {
        var result = await _service.Okei();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> DeleteAll()
    {
        List<DoctorGetDto> docs = await _doctorService.GetAllAsync(d => !d.IsDeleted);
        if (docs is null) throw new NotFoundException(Messages.NotFound);
        foreach (DoctorGetDto doc in docs)
        {
            foreach (var item in doc.rezervs)
            {
                if (item.Busy)
                {
                    item.Busy = false;
                }
            }
            DoctorUpdateDto dto = new DoctorUpdateDto { getDto = doc };
            await _doctorService.UpdateAsync(dto);
        }

        return RedirectToAction(nameof(Index));
    }
}

