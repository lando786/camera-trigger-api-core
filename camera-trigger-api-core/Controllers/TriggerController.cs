using System;
using System.Collections.Generic;
using System.IO;
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
        // GET api/triggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trigger>>> GetAsync() =>
            await _ctx.Triggers.ToListAsync();

        // GET api/triggers/5
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

        // POST api/triggers
        //[HttpPost]
        //public async Task<ActionResult<Trigger>> PostTriggerItemAsync(Trigger item)
        //{
        //    item.TimeStamp = DateTime.Now;
        //    var created = _ctx.Triggers.AddAsync(item);
        //    await _ctx.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
        //}

        [HttpPost]
        public async Task<ActionResult<Trigger>> PostTriggerItemText()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                string plainText = reader.ReadToEnd();

                var item = new Trigger(plainText);
                await _ctx.Triggers.AddAsync(item);
                await _ctx.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);

                return Ok(plainText);
            }
            
        }
    }
}
