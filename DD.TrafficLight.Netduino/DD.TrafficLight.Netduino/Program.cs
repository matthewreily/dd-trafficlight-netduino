using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace DD.TrafficLight.Netduino
{
    public class Program
    {
        private const bool Off = true;
        private const bool On = false;

        static Program()
        {
            RedLightRelay = new OutputPort(Pins.GPIO_PIN_D4, Off);
            YellowLightRelay = new OutputPort(Pins.GPIO_PIN_D3, Off);
            GreenLightRelay = new OutputPort(Pins.GPIO_PIN_D2, Off);
        }

        private static OutputPort RedLightRelay { get; set; }
        private static OutputPort GreenLightRelay { get; set; }
        private static OutputPort YellowLightRelay { get; set; }

        public static void Main()
        {
            while (true)
            {
                TurnLightOn(RedLightRelay, 1000 * 2);
                TurnLightOn(GreenLightRelay, 1000 * 2);
                TurnLightOn(YellowLightRelay, 1000 * 1);
            }
        }

        private static void TurnLightOn(OutputPort relay, int duration)
        {
            relay.Write(On);
            Thread.Sleep(duration);
            relay.Write(Off);
        }
    }
}
