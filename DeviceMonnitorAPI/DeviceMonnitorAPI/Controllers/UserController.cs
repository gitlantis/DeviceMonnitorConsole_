using DeviceMonnitorAPI.DBModels;
using DeviceMonnitorAPI.Models;
using DeviceMonnitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Controllers
{
    [Authorize(Roles = "Admin, ApiAdmin")]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var result = await _userService.Authorize(model);

            if (result == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] OrgUserModel model)
        {
            var result = await _userService.AddUser(model);
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] Guid guid)
        {
            var result = await _userService.DeleteUser(guid);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin")]
        [HttpPost]
        public async Task<IActionResult> EditUser([FromBody] OrgUserModel model)
        {
            var result = await _userService.EditUser(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            return Ok(token);
        }
    }
}
