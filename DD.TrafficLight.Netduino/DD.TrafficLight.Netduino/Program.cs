using System.Threading;
using DD.TrafficLight.Netduino.IO;
using DD.TrafficLight.Netduino.Model;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace DD.TrafficLight.Netduino
{
    public class Program
    {
        private const bool Off = true;
        private const bool On = false;
        private static TrafficLightConfig _currentConfig;
        private static TrafficLightConfig _updatedConfig;
        private static Timer _timer;

        static Program()
        {
            RedLightRelay = new OutputPort(Pins.GPIO_PIN_D4, Off);
            YellowLightRelay = new OutputPort(Pins.GPIO_PIN_D3, Off);
            GreenLightRelay = new OutputPort(Pins.GPIO_PIN_D2, Off);
            _currentConfig = _updatedConfig = new TrafficLightConfig
            {
                Red = 10,
                Green = 10,
                Yellow = 5
            };
        }

        private static OutputPort RedLightRelay { get; set; }
        private static OutputPort GreenLightRelay { get; set; }
        private static OutputPort YellowLightRelay { get; set; }

        public static void Main()
        {
            _timer = new Timer(CheckForConfigurationChanges, null, 0, 10*1000);
            while (true)
            {
                _currentConfig = _updatedConfig.Clone();
                if (_currentConfig.MaintenanceMode)
                {
                    TurnLightOn(RedLightRelay, 1000*1);
                    continue;
                }
                TurnLightOn(RedLightRelay, _currentConfig.Red);
                TurnLightOn(GreenLightRelay, _currentConfig.Green);
                TurnLightOn(YellowLightRelay, _currentConfig.Yellow);
            }
        }

        private static void CheckForConfigurationChanges(object state)
        {
            _updatedConfig = TrafficLightConfigService.GetConfiguration();
        }

        private static void TurnLightOn(OutputPort relay, int durationInSeconds)
        {
            relay.Write(On);
            Thread.Sleep(durationInSeconds*1000);
            relay.Write(Off);
        }
    }
}