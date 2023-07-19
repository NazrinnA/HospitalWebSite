
using Business.Services.Intefaces;
using Entities.Dtos.ResHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]


public class DoctorController : Controller
{
    private readonly IDoctorService _service;
    private readonly IPositionService _position;
    private readonly IResHistoryService _history;
    private readonly ITimeService _time;
    public DoctorController(IDoctorService service, IPositionService position, IResHistoryService history, ITimeService time)
    {
        _service = service;
        _position = position;
        _history = history;
        _time = time;
    }

    public async Task<IActionResult> Index(int currentpage = 1, int take = 7)
    {
        List<DoctorGetDto> getDtos = await _service.GetAllAsync(d => !d.IsDeleted);
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
    public async Task<IActionResult> Create()
    {
        ViewBag.Time = await _time.Get();
        ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DoctorPostDto postDto)
    {
        ViewBag.Time = await _time.Get();

        if (!ModelState.IsValid)
        {
            ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
            return View();
        }
        foreach (var item in postDto.TimeId)
        {
          List<int> ints= postDto.TimeId.FindAll(t => t == item);
            if (ints.Count != 1)
            {
                ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
                ViewBag.Time = await _time.Get();
                ModelState.AddModelError("", "Rezerv saatları təkrarlana bilməz!");
                return View();
            }
        }
        if (!postDto.formFile.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Formfile", "please send image");
            ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
            return View(postDto);
        }
        await _service.CreateAsync(postDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Time = await _time.Get();
        DoctorGetDto getDto = await _service.GetbyId(id);
        if (getDto.IsDeleted == true) { return NotFound(); }
        DoctorUpdateDto updateDto = new DoctorUpdateDto { getDto = getDto };
        ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(DoctorUpdateDto updateDto)
    {
        updateDto.getDto = await _service.GetbyId(updateDto.getDto.Id);
        foreach (var item in updateDto.postDto.TimeId)
        {
            List<int> ints = updateDto.postDto.TimeId.FindAll(t => t == item);
            if (ints.Count != 1)
            {
                ViewBag.Positions = await _position.GetAllAsync(p => !p.IsDeleted);
                ViewBag.Time = await _time.Get();
                ModelState.AddModelError("", "Rezerv saatları təkrarlana bilməz!");
                return View(updateDto);
            }
        }
        var result = await _service.UpdateAsync(updateDto);
        if (!result)
        {
            ViewBag.Positions = await _position.GetAllAsync(p=>!p.IsDeleted);
            ViewBag.Time = await _time.Get();

            return View(updateDto);
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        DoctorGetDto doctor = await _service.GetbyId(id);
        return View(doctor);
    }
    public async Task<IActionResult> GetAll(int currentpage = 1, int take = 7)
    {
        List<DoctorGetDto> getDtos = await _service.GetAllAsync();
        int count = getDtos.Count;
        if (getDtos == null) return View();
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
    public async Task<IActionResult> History(int currentpage = 1, int take = 7)
    {
        List<ResGetDto> getDtos = await _history.GetAllAsync();
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

