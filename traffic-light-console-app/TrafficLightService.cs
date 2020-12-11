using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class TrafficLightService : ITrafficLightService, IDisposable
  {
    #region Private Fields

    private ITrafficLightExecutor executor;
    private TrafficLightList trafficLights;
    private int crossroadsClosureInMs;
    private int lightSwitchIntervalInMs;

    #endregion

    #region Constructors

    public TrafficLightService(int crossroadsClosureInMs, int lightSwitchIntervalInMs, TrafficLightList trafficLights)
    {
      this.crossroadsClosureInMs = crossroadsClosureInMs;
      this.lightSwitchIntervalInMs = lightSwitchIntervalInMs;
      this.trafficLights = trafficLights;

      InitializeExecutor();
    }

    #endregion

    #region Public Methods

    public void SwitchToGreen(TrafficLightIdentifier identifier)
    {
      foreach (var otherTrafficLight in this.trafficLights.Where(tl => tl.Identifier != identifier))
      {
        InternalSwitchToRed(otherTrafficLight);
      }

      var trafficLight = trafficLights.SingleOrDefault(tl => tl.Identifier == identifier);
      if (trafficLight != null)
      {
        InternalSwitchToGreen(trafficLight);
      }
    }

    public void SwitchToRed()
    {
      foreach (var trafficLight in this.trafficLights)
      {
        InternalSwitchToRed(trafficLight);
      }
    }

    public void Dispose()
    {
      this.executor.Dispose();
    }

    #endregion

    #region Private Methods

    private void InitializeExecutor()
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        this.executor = new GpioTrafficLightExecutor(trafficLights, new ConsoleTrafficLightExecutor());
      }
      else
      {
        this.executor = new ConsoleTrafficLightExecutor();
      }
    }

    private void InternalSwitchToGreen(TrafficLight trafficLight)
    {
      this.executor.Off(trafficLight, TrafficLightColor.RedYellow);
      this.executor.On(trafficLight, TrafficLightColor.Green);
    }

    private void InternalSwitchToRed(TrafficLight trafficLight)
    {
      this.executor.On(trafficLight, TrafficLightColor.Red);
      this.executor.Off(trafficLight, TrafficLightColor.Yellow);
      this.executor.Off(trafficLight, TrafficLightColor.Green);
    }

    #endregion
  }
}