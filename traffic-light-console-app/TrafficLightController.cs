using System.Collections.Generic;
using System.Threading;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class TrafficLightController : ILedController
  {
    private const int CROSSROADS_CLOSURE_IN_MS = 500;
    private const int LIGHT_SWITCH_INTERVAL_IN_MS = 500;

    public void DoLighting()
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

      using (var trafficLightService = new TrafficLightService(CROSSROADS_CLOSURE_IN_MS, LIGHT_SWITCH_INTERVAL_IN_MS, trafficLights))
      {
        while (true)
        {
          trafficLightService.PerformLedCheck();

          trafficLightService.SwitchToGreen(TrafficLightIdentifier.Top);
          Thread.Sleep(CROSSROADS_CLOSURE_IN_MS);

          trafficLightService.SwitchToGreen(TrafficLightIdentifier.Middle);
          Thread.Sleep(CROSSROADS_CLOSURE_IN_MS);

          trafficLightService.SwitchToGreen(TrafficLightIdentifier.Bottom);
          Thread.Sleep(CROSSROADS_CLOSURE_IN_MS);
        }
      }
    }
  }
}