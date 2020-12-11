using System.Threading.Tasks;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public enum TrafficLightColorIdentifier
  {
    Red,
    RedYellow,
    Yellow,
    Green
  }

  public record TrafficLightColor
  {
    public TrafficLightColorIdentifier Identifier { get; }
    public int DwellTimeInMs { get; }
    public bool HasMultipleLights { get; }

    public string Name => this.Identifier.ToString();

    public TrafficLightColor(TrafficLightColorIdentifier identifier, int dwellTimeInMs, bool hasMultipleLights)
      => (Identifier, DwellTimeInMs, HasMultipleLights) = (identifier, dwellTimeInMs, hasMultipleLights);
  }
}