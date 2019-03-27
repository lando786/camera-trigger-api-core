using camera_trigger_api_core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace camera_trigger_api_core.Contexts
{
    public interface ITriggerContext
    {
        DbSet<Trigger> Triggers { get; set; }

        Task<int> SaveChangesAsync();
    }

    public class TriggerContext : DbContext, ITriggerContext
    {
        public TriggerContext(DbContextOptions<TriggerContext> options)
            : base(options)
        {
        }

        public DbSet<Trigger> Triggers { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
