using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace camera_trigger_api_core.Models
{
    public class Trigger
    {
        public Trigger()
        {

        }
        public Trigger(string cameraName)
        {
            CameraName = cameraName;
            TimeStamp = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string CameraName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
