using FirebaseProxy.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirebaseProxy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetwayController : ControllerBase
    {
        private readonly ILogger<GetwayController> _logger;
        private readonly IFirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = "043adAn87dLdvPfkFlgVPBUhSR2roeqLyftAwB5C",//"OA4QKAKnS5wwys2hLMz47QIwutkh1x0UYT1Gc8RY",
            BasePath = "https://mytestproj-1fda7.firebaseio.com/"//"https://devconsole-f3fdb-default-rtdb.firebaseio.com/"
        };

        private IFirebaseClient client;

        public GetwayController(ILogger<GetwayController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{**regs}")]
        public async Task<IActionResult> AddRecord(string regs)
        {
            var date = DateTime.Now;
            dynamic result = null;
            try
            {

                client = new FirebaseClient(fbc);

                string[] pars = regs.Split(new char[] { '&' });

                var dict = new Dictionary<string, string>();
                int i = 1;
                foreach (var p in pars)
                {
                    if (i < 28)
                    {
                        dict.Add("reg" + (i - 1), pars[i]);
                    }
                    i++;
                }

                var data = new DeviceRawData
                {
                    DevGUID = pars.First(),
                    DataCreatedTime = date,
                    DeviceOnceDataPortion = dict
                };

                var resp = await client.PushAsync("tempData", data);

                client = new FirebaseClient(fbc);
                var config = JsonSerializer.Deserialize<DeviceConfigToDev>(client.GetAsync(data.DevGUID).Result.Body);

                result = config;
            }
            catch (Exception e)
            {
                throw new DataMisalignedException(e.Message);
            }

            return Ok(result);
        }
    }
}
