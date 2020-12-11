using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  class Program
  {
    public static async Task Main(string[] args)
    {
      Console.WriteLine("Hello Traffic Lights!");

      using (CancellationTokenSource cts = new CancellationTokenSource())
      {
        ILedController ledController = new TrafficLightController();

        var cancelTask = Task.Run(() =>
        {
          Console.WriteLine("Press Enter to exit");
          Console.ReadKey();

          cts.Cancel();
        });

        try
        {
          await ledController.DoLightingAsync(cts.Token);
        }
        catch (TaskCanceledException ex)
        {
          Console.WriteLine(ex.Message);
        }

        await cancelTask;
        Console.WriteLine("Bye!");
      }
    }
  }
}
