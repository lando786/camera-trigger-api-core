using camera_trigger_api_core.Models;
using Microsoft.EntityFrameworkCore;

namespace camera_trigger_api_core.Contexts
{
    public class TriggerContext : DbContext
    {
        public TriggerContext(DbContextOptions<TriggerContext> options)
            : base(options)
        {
        }
        public DbSet<Trigger> Triggers { get; set; }
    }
}
