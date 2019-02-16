using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using camera_trigger_api_core.Models;
using camera_trigger_api_core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace camera_trigger_api_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private TriggerService _ctx;
        public TriggersController(TriggerService ctx)
        {
            _ctx = ctx;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trigger>>> GetAsync() => 
            await _ctx.GetAsync();

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trigger>> GetAsync(string id)
        {
            var item = await _ctx.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Trigger>> PostTriggerItem(Trigger item)
        {
            item.TimeStamp = DateTime.Now;
            var created = _ctx.CreateAsync(item);
            return CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
        }
    }
}
