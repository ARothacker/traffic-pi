using System;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello Traffic Lights!");

      ILedController ledController = new TrafficLightController();
      Task.Run(() => ledController.DoLighting());

      Console.WriteLine("Press Enter to exit");
      Console.ReadLine();
      Console.WriteLine("Bye!");
    }
  }
}
