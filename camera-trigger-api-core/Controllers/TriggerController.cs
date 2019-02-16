using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using camera_trigger_api_core.Contexts;
using camera_trigger_api_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace camera_trigger_api_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private TriggerContext _ctx;
        public TriggersController(TriggerContext ctx)
        {
            _ctx = ctx;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trigger>>> GetAsync() => 
            await _ctx.Triggers.ToListAsync();

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trigger>> GetAsync(long id)
        {
            var item = await _ctx.Triggers.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
