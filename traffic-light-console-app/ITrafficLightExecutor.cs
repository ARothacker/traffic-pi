using System;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public interface ITrafficLightExecutor : IDisposable
  {
    void On(TrafficLight trafficLight, TrafficLightColorIdentifier identifier);
    void Off(TrafficLight trafficLight, TrafficLightColorIdentifier identifier);
  }
}