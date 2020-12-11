using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class TrafficLightController : ILedController
  {
    public async Task DoLightingAsync(CancellationToken token)
    {
      var trafficLights = new TrafficLightList
      {
        new TrafficLight(
          TrafficLightIdentifier.Top,
          18, // GPIO 18 - Pin 12
          23, // GPIO 23 - Pin 16
          24  // GPIO 24 - Pin 18
        ),
        new TrafficLight(
          TrafficLightIdentifier.Middle,
          8,  // GPIO 8 - Pin 24 - SPICE0 / CE0
          7,  // GPIO 7 - Pin 26 - SPICE1 / CE1
          12  // GPIO 12 - Pin 32
        ),
        new TrafficLight(
          TrafficLightIdentifier.Bottom,
          16, // GPIO 16 - Pin 36
          20, // GPIO 20 - Pin 38 - MOSI
          21  // GPIO 21 - Pin 40 - SCLK
        )
      };

      using (var trafficLightService = new TrafficLightService(trafficLights))
      {
        while (true)
        {
          await trafficLightService.PerformLedCheckAsync();

          foreach (var trafficLightIdentifier in (TrafficLightIdentifier[])Enum.GetValues(typeof(TrafficLightIdentifier)))
          {
            await trafficLightService.SwitchToGreenAsync(trafficLightIdentifier);

            if (token.IsCancellationRequested)
            {
              await trafficLightService.SwitchOff();
              throw new TaskCanceledException("Lighting was cancelled");
            }
          }
        }
      }
    }
  }
}