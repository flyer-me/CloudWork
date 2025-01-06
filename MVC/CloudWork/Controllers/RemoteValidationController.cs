﻿using CloudWork.Common.DB;
using CloudWork.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudWork.Controllers
{
    public class RemoteValidationController : Controller
    {
        private readonly CloudWorkDbContext _context;
        private readonly IRegistrationService _registerationServices;
        public RemoteValidationController(CloudWorkDbContext context, IRegistrationService registeredServices)
        {
            _context = context;
            _registerationServices = registeredServices;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsUserNameAvailable(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Json("用户名不能为空");
            }
            if (await _context.Users.AnyAsync(u => u.UserName == userName))
            {
                var suggestions = await _registerationServices.GenerateUniqueUserNamesAsync(userName);
                var strSuggestions = string.Join(", ", suggestions);
                return Json($"已被占用，可使用： {strSuggestions}");
            }
            return Json(true);
        }
    }
}
