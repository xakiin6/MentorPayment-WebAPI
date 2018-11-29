using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server.Controllers
{
    
    [Route("api/[controller]")]
   
    [Authorize]
    public class ClassController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lists = new List<object>();
            string path = Directory.GetCurrentDirectory();
            var datastring = await System.IO.File.ReadAllTextAsync(@"c:/datafiles/class.json");
            var jsondata = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassMentor[]>(datastring);
            
            var result = jsondata
            .GroupBy(l => (l.Paid? new DateTime(l.PDate.Value.Year,l.PDate.Value.Month,1) : new DateTime(l.Week.Year, l.Week.Month+1, 1)))
            .Select(cl => new 
            {   Month =  (cl.First().Paid? new DateTime(cl.First().PDate.Value.Year,cl.First().PDate.Value.Month,1) : new DateTime(cl.First().Week.Year, cl.First().Week.Month+1, 1)),
            Total = cl.Sum(c => c.Total)
            }).ToList();

            lists.Add(jsondata.OrderBy(a=> a.Week));
            lists.Add(result);                        
            return Ok(lists);
        }


        // POST api/values
      
        
    [HttpPost]
        public async Task<IActionResult> Post([FromBody]object classData) {
            if(classData is null)
                return NoContent();

              var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(classData);
              string path = Directory.GetCurrentDirectory();
              await System.IO.File.WriteAllTextAsync(@"c:/datafiles/class.json", jsondata);

            return Ok(classData);
        }
    }
}
