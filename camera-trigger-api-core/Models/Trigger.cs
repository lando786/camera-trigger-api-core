using System;

namespace camera_trigger_api_core.Models
{
    public class Trigger
    {
        public long Id { get; set; }
        public string CameraName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
