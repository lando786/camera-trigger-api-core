using System;

namespace camera_trigger_api_core.DTOs
{
    public class TriggerDto
    {
        public string CameraName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}