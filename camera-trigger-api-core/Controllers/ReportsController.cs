using camera_trigger_api_core.Models;
using camera_trigger_api_core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace camera_trigger_api_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IReportsService _service;

        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        // GET api/triggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAsync()
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return await _service.GetFullReport();
        }

        [HttpGet]
        [Route("week")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetWeek()
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return await _service.GetWeeklyReport();
        }
    }
}
