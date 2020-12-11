using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public interface ITrafficLightService
  {
    Task SwitchToGreenAsync(TrafficLightIdentifier identifier);
  }
}