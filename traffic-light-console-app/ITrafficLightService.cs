using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public interface ITrafficLightService
  {
    void SwitchToGreen(TrafficLightIdentifier identifier);
  }
}