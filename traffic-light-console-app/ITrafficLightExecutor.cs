using System;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public interface ITrafficLightExecutor : IDisposable
  {
    void On(TrafficLight trafficLight, TrafficLightColor color);
    void Off(TrafficLight trafficLight, TrafficLightColor color);
  }
}