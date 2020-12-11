using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Runtime.InteropServices;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class GpioTrafficLightExecutor : ITrafficLightExecutor
  {
    #region Private Fields

    private GpioController gpioController;
    private TrafficLightList trafficLights;
    private ITrafficLightExecutor additionalExecutor;

    #endregion

    #region Constructors

    public GpioTrafficLightExecutor(TrafficLightList trafficLights, ITrafficLightExecutor additionalExecutor = null)
    {
      if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        Console.WriteLine("This GpioTrafficLightExecutor only works on your Raspberry Pi!");
        return;
      }

      this.trafficLights = trafficLights;
      this.additionalExecutor = additionalExecutor;

      this.gpioController = new GpioController(PinNumberingScheme.Logical);
      OpenPins();
    }

    #endregion

    #region Public Methods

    public void On(TrafficLight trafficLight, TrafficLightColor color)
    {
      List<int> pins = GetPins(trafficLight, color);
      pins.ForEach(pin => this.gpioController.Write(pin, PinValue.High));

      this.additionalExecutor?.On(trafficLight, color);
    }

    public void Off(TrafficLight trafficLight, TrafficLightColor color)
    {
      List<int> pins = GetPins(trafficLight, color);
      pins.ForEach(pin => this.gpioController.Write(pin, PinValue.Low));

      this.additionalExecutor?.Off(trafficLight, color);
    }

    public void Dispose()
    {
      this.additionalExecutor?.Dispose();
      ClosePins();
    }

    #endregion

    #region Private Methods

    private List<int> GetPins(TrafficLight trafficLight, TrafficLightColor color)
    {
      return color switch
      {
        TrafficLightColor.Red => new List<int>() { trafficLight.Red },
        TrafficLightColor.RedYellow => new List<int>() { trafficLight.Red, trafficLight.Yellow },
        TrafficLightColor.Yellow => new List<int>() { trafficLight.Yellow },
        TrafficLightColor.Green => new List<int>() { trafficLight.Green },
        _ => throw new Exception($"Unknown TrafficLightColor {color}")
      };
    }

    private void OpenPins()
    {
      foreach (var light in this.trafficLights)
      {
        gpioController.OpenPin(light.Red, PinMode.Output);
        gpioController.OpenPin(light.Yellow, PinMode.Output);
        gpioController.OpenPin(light.Green, PinMode.Output);
      }
    }

    private void ClosePins()
    {
      foreach (var light in this.trafficLights)
      {
        gpioController.ClosePin(light.Red);
        gpioController.ClosePin(light.Yellow);
        gpioController.ClosePin(light.Green);
      }
    }

    #endregion
  }
}