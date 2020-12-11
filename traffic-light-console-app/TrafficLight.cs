namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public enum TrafficLightIdentifier
  {
    Top,
    Middle,
    Bottom
  }

  public record TrafficLight
  {
    public TrafficLightIdentifier Identifier { get; }
    public int Red { get; }
    public int Yellow { get; }
    public int Green { get; }
    public TrafficLightColorIdentifier CurrentColor { get; set; }

    public TrafficLight(TrafficLightIdentifier identifier, int red, int yellow, int green)
      => (Identifier, Red, Yellow, Green, CurrentColor) = (identifier, red, yellow, green, TrafficLightColorIdentifier.Red);
  }
}