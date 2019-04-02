using camera_trigger_api_core.DTOs;
using camera_trigger_api_core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace camera_trigger_api_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private ITriggerService _service;

        public TriggersController(ITriggerService service)
        {
            _service = service;
        }

        // GET api/triggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TriggerDto>>> GetAsync()
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var triggers = await _service.GetAllTriggersAsync();
            return Ok(triggers);
        }

        // GET api/triggers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TriggerDto>> GetAsync(long id)
        {
            var res = await _service.FindByIdAsync(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TriggerDto>> PostTriggerItemText()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                string plainText = reader.ReadToEnd();

                var item = new TriggerDto(plainText);
                var res = await _service.AddTriggerAsync(item);
                return CreatedAtAction(nameof(GetAsync).ToLower(), new { id = res }, item);
            }
        }
    }
}