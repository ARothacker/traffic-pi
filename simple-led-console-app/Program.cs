using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.SimpleLedConsoleApp
{
  class Program
  {
    public static void Main(string[] args)
    {
      if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        Console.WriteLine("This program should only run on a Raspberry Pi!");
        return;
      }

      Console.WriteLine("Hello LED!");

      ILedController ledController = new SimpleLedController();
      Task.Run(() => ledController.DoLighting());

      Console.WriteLine("Press Enter to exit");
      Console.ReadLine();
      Console.WriteLine("Bye!");
    }
  }
}
