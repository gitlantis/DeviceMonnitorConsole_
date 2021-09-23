using DeviceMonnitorAPI.DBModels;
using DeviceMonnitorAPI.Models;
using DeviceMonnitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Controllers
{
    public class DeviceController : BaseController
    {
        private readonly DeviceService _deviceService;
        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;

        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> GetDevices()
        {
            var result = await _deviceService.GetDevices();
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] Device model)
        {
            var result = await _deviceService.AddDevice(model);
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> EditDevice([FromBody] DeviceModel model)
        {
            var result = await _deviceService.EditDevice(model);
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteDevice([FromBody]Guid guid)
        {
            var result = await _deviceService.DeleteDevice(guid);
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> GetDeviceUsers([FromBody]Guid guid)
        {
            var result = await _deviceService.GetDeviceUsers(guid);
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> ConnectUsers([FromBody] DeviceUsersModel model)
        {
            var result = await _deviceService.ConnectUsers(model);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConfig([FromBody] DeviceConfigModel model)
        {
            var result = await _deviceService.SetConfig(model);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetConfig([FromBody] Guid guid)
        {
            var result = await _deviceService.GetConfigByDeviceGuid(guid);
            return Ok(result);
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetConfigAsMass([FromBody] Guid guid)
        {
            var result = await _deviceService.GetConfigAsMass(guid);
            return Ok(result);
        }
    }
}
