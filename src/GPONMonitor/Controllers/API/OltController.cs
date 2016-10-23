using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GPONMonitor.Controllers
{
    [Route("api/[controller]")]
    public class OltApiController : Controller
    {
        // GET: api/values
        //[HttpGet]
        //public  Task<JsonResult> Get()
        //{
        //    var results =  "value1";

        //    return Json(results);
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
