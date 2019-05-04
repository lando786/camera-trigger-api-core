using camera_trigger_api_core.Contexts;
using camera_trigger_api_core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace camera_trigger_api_core.Services
{
    public interface IReportsService
    {
        Task<IEnumerable<ReportDto>> GetFullReport();

        Task<IEnumerable<ReportDto>> GetWeeklyReport();
    }

    public class ReportsService : IReportsService
    {
        private readonly ITriggerContext _ctx;

        public ReportsService(ITriggerContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<ReportDto>> GetFullReport()
        {
            try
            {
                var triggers = await _ctx.Triggers
                    .Where(x => x.TimeStamp >= DateTime.Today.AddDays(-30))
                    .ToListAsync();
                return triggers.ToLookup(x => x.TimeStamp.Date).Select(r =>
                    new ReportDto
                    {
                        Date = r.Key.ToString().Substring(0, r.Key.ToString().IndexOf(' ')),
                        Count = r.Count()
                    }
                    ).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<ReportDto>> GetWeeklyReport()
        {
            try
            {
                var triggers = await _ctx.Triggers
                    .Where(x => x.TimeStamp >= DateTime.Today.AddDays(-6))
                    .ToListAsync();
                return triggers.ToLookup(x => x.TimeStamp.Date).Select(r =>
                    new ReportDto
                    {
                        Date = r.Key.ToString().Substring(0, r.Key.ToString().IndexOf(' ')),
                        Count = r.Count()
                    }
                    ).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
