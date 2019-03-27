using camera_trigger_api_core.Contexts;
using camera_trigger_api_core.DTOs;
using camera_trigger_api_core.Helpers;
using camera_trigger_api_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<TriggerDto>>> GetAsync()
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var list = await _ctx.Triggers.ToListAsync();
            return list.Select(x => x.ConvertToDto()).ToList();
        }

        // GET api/triggers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TriggerDto>> GetAsync(long id)
        {
            var item = await _ctx.Triggers.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.ConvertToDto();
        }

        [HttpPost]
        public async Task<ActionResult<TriggerDto>> PostTriggerItemText()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                string plainText = reader.ReadToEnd();

                var item = new Trigger(plainText);
                await _ctx.Triggers.AddAsync(item);
                await _ctx.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
            }
        }
    }
}