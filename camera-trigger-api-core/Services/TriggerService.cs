﻿using camera_trigger_api_core.Contexts;
using camera_trigger_api_core.DTOs;
using camera_trigger_api_core.Helpers;
using camera_trigger_api_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace camera_trigger_api_core.Services
{
    public interface ITriggerService
    {
        Task<ActionResult<IEnumerable<TriggerDto>>> GetAllTriggersAsync();

        Task<ActionResult<TriggerDto>> FindByIdAsync(long id);

        Task<long> AddTriggerAsync(TriggerDto item);
    }

    public class TriggerService : ITriggerService
    {
        private readonly ITriggerContext _ctx;

        public TriggerService(ITriggerContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<long> AddTriggerAsync(TriggerDto item)
        {
            var toAdd = new Trigger()
            {
                CameraName = item.CameraName,
                TimeStamp = item.TimeStamp
            };
            await _ctx.Triggers.AddAsync(toAdd);
            var saveResult = await _ctx.SaveChangesAsync();
            return saveResult > 0 ? toAdd.Id : -1;
        }

        public async Task<ActionResult<TriggerDto>> FindByIdAsync(long id)
        {
            var item = await _ctx.Triggers.FindAsync(id);
            if (item == null)
            {
                return null;
            }
            return item.ConvertToDto();
        }

        public async Task<ActionResult<IEnumerable<TriggerDto>>> GetAllTriggersAsync()
        {
            var list = await _ctx.Triggers.ToListAsync();
            return list.Select(x => x.ConvertToDto()).ToList();
        }
    }
}
