using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class TrafficLightService : ITrafficLightService, IDisposable
  {
    #region Private Fields

    private TrafficLightList trafficLights;
    private TrafficLightColorList colors;
    private ITrafficLightExecutor executor;

    #endregion

    #region Constructors

    public TrafficLightService(TrafficLightList trafficLights)
    {
      this.trafficLights = trafficLights;

      this.colors = new TrafficLightColorList();
      InitializeExecutor();
    }

    #endregion

    #region Public Methods

    public async Task SwitchToGreenAsync(TrafficLightIdentifier identifier)
    {
      foreach (var trafficLight in this.trafficLights.GetCounterTrafficLights(identifier))
      {
        await InternalSwitchToRedAsync(trafficLight);
      }

      await InternalSwitchToGreenAsync(trafficLights[identifier]);
    }

    public async Task SwitchToRedAsync()
    {
      foreach (var trafficLight in this.trafficLights)
      {
        await InternalSwitchToRedAsync(trafficLight);
      }
    }

    public async Task PerformLedCheckAsync()
    {
      InternalAllOn();
      await Task.Delay(2000);

      InternalAllOff();
      await Task.Delay(2000);
    }

    public async Task SwitchOff()
    {
      await SwitchToRedAsync();

      await InternalAllYellow();

      InternalAllOff();
      await Task.Delay(1000);
      await InternalAllYellow();
      InternalAllOff();
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

    private void InternalAllOn()
    {
      foreach (var trafficLight in this.trafficLights)
      {
        InternalAllOn(trafficLight);
      }
    }
    private async Task InternalAllYellow()
    {
      foreach (var trafficLight in this.trafficLights)
      {
        await InternalSwitchToColorAsync(trafficLight, TrafficLightColorIdentifier.Yellow);
      }

      await Task.Delay(1000);
    }
    private void InternalAllOff()
    {
      foreach (var trafficLight in this.trafficLights)
      {
        InternalAllOff(trafficLight);
      }
    }

    private void InternalAllOn(TrafficLight trafficLight)
    {
      foreach (var color in this.colors.GetSingleLightColors())
      {
        this.executor.On(trafficLight, color.Identifier);
      }
    }
    private void InternalAllOff(TrafficLight trafficLight)
    {
      foreach (var color in this.colors.GetSingleLightColors())
      {
        this.executor.Off(trafficLight, color.Identifier);
      }
    }

    private async Task InternalSwitchToGreenAsync(TrafficLight trafficLight)
    {
      if (trafficLight.CurrentColor != TrafficLightColorIdentifier.Green)
      {
        if (trafficLight.CurrentColor != TrafficLightColorIdentifier.RedYellow)
        {
          await InternalSwitchToColorAsync(trafficLight, TrafficLightColorIdentifier.RedYellow);
        }

        await InternalSwitchToColorAsync(trafficLight, TrafficLightColorIdentifier.Green);
      }
    }
    private async Task InternalSwitchToRedAsync(TrafficLight trafficLight)
    {
      if (trafficLight.CurrentColor != TrafficLightColorIdentifier.Red)
      {
        if (trafficLight.CurrentColor != TrafficLightColorIdentifier.Yellow)
        {
          await InternalSwitchToColorAsync(trafficLight, TrafficLightColorIdentifier.Yellow);
        }

        await InternalSwitchToColorAsync(trafficLight, TrafficLightColorIdentifier.Red);
      }
    }

    private async Task InternalSwitchToColorAsync(TrafficLight trafficLight, TrafficLightColorIdentifier identifier)
    {
      foreach (var counterColor in this.colors.GetCounterColors(identifier))
      {
        this.executor.Off(trafficLight, counterColor.Identifier);
      }
      this.executor.On(trafficLight, identifier);

      trafficLight.CurrentColor = identifier;
      await Task.Delay(this.colors[identifier].DwellTimeInMs);
    }

    #endregion
  }
}