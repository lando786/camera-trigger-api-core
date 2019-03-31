using System;

namespace camera_trigger_api_core.DTOs
{
    public class TriggerDto
    {
        public TriggerDto()
        {
        }

        public TriggerDto(string cameraName)
        {
            CameraName = cameraName;
            TimeStamp = DateTime.Now;
        }

        public string CameraName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
