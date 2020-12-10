using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.SimpleLedConsoleApp
{
  public class SimpleLedController : ILedController
  {
    private const int PIN = 23; // GPIO 23 = Pin 16

    public void DoLighting()
    {
      var gpioController = new GpioController(PinNumberingScheme.Logical);
      var intervalInMS = 300;

      gpioController.OpenPin(PIN, PinMode.Output);

      try
      {
        while (true)
        {
          gpioController.Write(PIN, PinValue.High);
          Thread.Sleep(intervalInMS);

          gpioController.Write(PIN, PinValue.Low);
          Thread.Sleep(intervalInMS);
        }
      }
      finally
      {
        gpioController.ClosePin(PIN);
      }
    }
  }
}