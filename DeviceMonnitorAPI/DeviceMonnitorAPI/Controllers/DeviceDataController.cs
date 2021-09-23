using DeviceMonnitorAPI.DBModels;
using DeviceMonnitorAPI.Models;
using DeviceMonnitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Controllers
{
    [AllowAnonymous]
    public class DeviceDataController : BaseController
    {
        private readonly DeviceDataService _deviceDataService;

        public DeviceDataController(DeviceDataService deviceDataService)
        {
            _deviceDataService = deviceDataService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetDynamicData()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");
            var role = claimsIdentity.FindFirst(claimsIdentity.RoleClaimType).Value;
            var userGuid = claim.Value;

            List<DeviceDynamicData> result = new List<DeviceDynamicData>();
            if (role.Equals("ApiAdmin") || role.Equals("Admin"))
            {
                result = await _deviceDataService.GetDynamicData(null);
            }
            else
            {
                result = await _deviceDataService.GetDynamicData(Guid.Parse(userGuid));
            }

            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] DeviceRawData model)
        {
            var guid = await _deviceDataService.AddData(model);
            return Ok(guid);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteData([FromBody] Guid guid)
        {
            var result = await _deviceDataService.DeleteData(guid);
            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteDataByDevice([FromBody] Guid guid)
        {
            var result = await _deviceDataService.DeleteDeviceData(guid);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetArchive([FromBody] ArchiveDataModel model)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");           
            var userGuid = claim.Value;

            var result = await _deviceDataService.GetArchive(Guid.Parse(userGuid), model.DeviceGuid, model.ItemCount, model.PageNum);

            return Ok(result);
        }

        [Authorize(Roles = "ApiAdmin, Admin")]                
        [HttpPost]
        public async Task<IActionResult> SetParams([FromBody] ParamsModel model)
        {
            var result = await _deviceDataService.SetParams(model);

            return Ok(result);
        }
        
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetParams([FromBody] Guid guid)
        {
            var result = await _deviceDataService.GetParams(guid);

            return Ok(result);
        }


    }
}
