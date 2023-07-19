using AutoMapper;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace finaProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMessageService _service;

        public MessageController( UserManager<AppUser> userManager, IMessageService service)
        {

            _userManager = userManager;
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Send(MessagePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return Json("Məlumatları daxil etdiyinizə əmin olun");
            }
            if (!User.Identity.IsAuthenticated)
            {
                return Json("Zəhmət olmasa ilk öncə login və ya registr olun");
            }
            string username = User.Identity.Name;
            AppUser user=await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return Json("Make sure you fill in the fields correctly");
            }

            postDto.Email= user.Email;
            postDto.UserName = user.UserName;
            await _service.CreateAsync(postDto);
            return Json(new { status = 200 });
        }
    }
}
