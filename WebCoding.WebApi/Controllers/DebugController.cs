using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Webcoding.Api.Models;

namespace Webcoding.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        [HttpPost("")]
        public ActionResult<string> Debug([FromBody] JObject obj)
        {
            string code = obj["code"].ToString();
            Lang   l    = DebugModel.StringToLang(obj["lang"].ToString());
            var    a    = new DebugModel(l,code);
            return a.RunCode();
        }
    }
}
