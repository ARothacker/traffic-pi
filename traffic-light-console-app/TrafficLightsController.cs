using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class SimpleTrafficLightsController : ILedController
  {
    public void DoLighting()
    {
      var gpioController = new GpioController(PinNumberingScheme.Logical);

      var pin = 23; // GPIO 23 = Pin 16
      var intervalInMS = 300;

      gpioController.OpenPin(pin, PinMode.Output);

      try
      {
        while (true)
        {
          gpioController.Write(pin, PinValue.High);
          Thread.Sleep(intervalInMS);

          gpioController.Write(pin, PinValue.Low);
          Thread.Sleep(intervalInMS);
        }
      }
      finally
      {
        gpioController.ClosePin(pin);
      }
    }
  }
}