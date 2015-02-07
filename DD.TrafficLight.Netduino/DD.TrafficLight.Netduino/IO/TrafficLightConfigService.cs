using System;
using System.Collections;
using System.IO;
using System.Net;
using DD.TrafficLight.Netduino.Model;
using Json.NETMF;
using Microsoft.SPOT;

namespace DD.TrafficLight.Netduino.IO
{
    class TrafficLightConfigService
    {
        public static TrafficLightConfig GetConfiguration()
        {
            try
            {
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create("http://ddtrafficlightweb.azurewebsites.net/api/TrafficLightConfigurationsApi");

                request.Accept = "application/json";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Get the stream containing content returned by the server.
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            // Read the content. 
                            string responseFromServer = reader.ReadToEnd();
                            var jsonResponse = JsonSerializer.DeserializeString(responseFromServer) as Hashtable;
                            if (jsonResponse != null)
                            {
                                return new TrafficLightConfig
                                {
                                    Red = int.Parse(jsonResponse["Red"].ToString()),
                                    Yellow = int.Parse(jsonResponse["Yellow"].ToString()),
                                    Green = int.Parse(jsonResponse["Green"].ToString()),
                                    MaintenanceMode = jsonResponse["MaintenanceMode"] is bool && (bool)jsonResponse["MaintenanceMode"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return new TrafficLightConfig
            {
                Red = 10,
                Green = 10,
                Yellow = 10
            };
        }
    }
}
