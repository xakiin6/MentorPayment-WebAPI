using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    
        [Authorize]
    public class ReviewsController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lists = new List<object>();
            string path = Directory.GetCurrentDirectory();
            var datastring = await System.IO.File.ReadAllTextAsync(@"c:/datafiles/reviews.json");
            var jsondata = Newtonsoft.Json.JsonConvert.DeserializeObject<Reviewer[]>(datastring);

            var result = jsondata
            .GroupBy(l => (l.Paid? l.PDate.Value: l.Month))
            .Select(cl => new 
            {   Month =  (cl.First().Paid? cl.First().PDate.Value: new DateTime(cl.First().Month.Year,cl.First().Month.Month+1,1)),
            Total = cl.Sum(c => c.Total)
            }).ToList();

            lists.Add(jsondata);
            lists.Add(result);

            return Ok(lists);
        
        }


        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object reviewData)
        {
            
            if(reviewData is null)
                return NoContent();
            var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(reviewData);
            string path = Directory.GetCurrentDirectory();
           await System.IO.File.WriteAllTextAsync(@"c:/datafiles/reviews.json", jsondata);
        return Ok(reviewData);
        }

    }
}
