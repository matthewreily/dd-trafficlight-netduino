using System;
using Microsoft.SPOT;

namespace DD.TrafficLight.Netduino.Model
{
    class TrafficLightConfig 
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Yellow { get; set; }
        public bool MaintenanceMode { get; set; }
        public TrafficLightConfig Clone()
        {
            return new TrafficLightConfig()
            {
                Red = Red,
                Green = Green,
                Yellow = Yellow,
                MaintenanceMode = MaintenanceMode
            };
        }
    }
}
