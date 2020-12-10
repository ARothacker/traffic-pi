using System;
using System.Runtime.InteropServices;

namespace ARWebApps.Learning.TrafficPi.SimpleLedConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        Console.WriteLine("This program should only run on a Raspberry Pi!");
        return;
      }

      Console.WriteLine("Hello LED!");

      ILedController ledController = new SimpleLedController();
      ledController.Start();

      Console.ReadKey();
      Console.WriteLine("Bye!");
    }
  }
}
