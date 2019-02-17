using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace camera_trigger_api_core.Models
{
    public class Trigger
    {
        public Trigger()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string CameraName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
