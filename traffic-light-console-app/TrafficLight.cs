namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public record TrafficLight
  {
    public TrafficLightIdentifier Identifier { get; }
    public int Red { get; }
    public int Yellow { get; }
    public int Green { get; }

    public TrafficLight(TrafficLightIdentifier identifier, int red, int yellow, int green)
      => (Identifier, Red, Yellow, Green) = (identifier, red, yellow, green);
  }
}