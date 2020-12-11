using System.Threading;
using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public interface ILedController
  {
    Task DoLightingAsync(CancellationToken token);
  }
}