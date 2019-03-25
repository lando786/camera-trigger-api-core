﻿using System;
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
    public class ReportsController : ControllerBase
    {
        private TriggerContext _ctx;
        public ReportsController(TriggerContext ctx)
        {
            _ctx = ctx;
        }
        // GET api/triggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyReport>>> GetAsync()
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var triggers = await _ctx.Triggers.ToListAsync();
            return triggers.ToLookup(x => x.TimeStamp.Date).Select(r =>
                new DailyReport
                {
                    Date = r.Key.ToString().Substring(0, r.Key.ToString().IndexOf(' ')),
                    Count = r.Count()
                }
                ).ToList();
        }
    }
}