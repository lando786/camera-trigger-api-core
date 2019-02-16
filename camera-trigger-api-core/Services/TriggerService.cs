using camera_trigger_api_core.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace camera_trigger_api_core.Services
{
    public class TriggerService
    {
        private readonly IMongoCollection<Trigger> _triggers;
        public TriggerService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TriggerDb"));
            var db = client.GetDatabase("TriggerDb");
            _triggers = db.GetCollection<Trigger>("Triggers");
        }
    
        public async Task<List<Trigger>> GetAsync()
        {
            return await _triggers.Find(t => true).ToListAsync();
        }

        public async Task<Trigger> GetAsync(string id)
        {
            return await _triggers.Find<Trigger>(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Trigger> CreateAsync(Trigger trigger)
        {
            await _triggers.InsertOneAsync(trigger);
            return trigger;
        }
    }
}
