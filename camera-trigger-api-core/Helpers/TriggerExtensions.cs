using camera_trigger_api_core.DTOs;
using camera_trigger_api_core.Models;

namespace camera_trigger_api_core.Helpers
{
    public static class TriggerExtensions
    {
        public static TriggerDto ConvertToDto(this Trigger from)
        {
            return new TriggerDto()
            {
                CameraName = from.CameraName,
                TimeStamp = from.TimeStamp
            };
        }
    }
}