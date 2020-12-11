using System;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class ConsoleTrafficLightExecutor : ITrafficLightExecutor
  {
    public ConsoleTrafficLightExecutor()
    {
      Console.WriteLine($"--- {this.GetType().Name} initialized ---");
    }

    public void On(TrafficLight trafficLight, TrafficLightColorIdentifier identifier)
    {
      Console.WriteLine(GetFormattedString(trafficLight.Identifier.ToString(), identifier, "ON"));
    }

    public void Off(TrafficLight trafficLight, TrafficLightColorIdentifier identifier)
    {
      Console.WriteLine(GetFormattedString(trafficLight.Identifier.ToString(), identifier, "OFF"));
    }

    public void Dispose()
    {
      Console.WriteLine($"--- {this.GetType().Name} disposed ---");
    }

    private string GetFormattedString(string trafficLightIdentifier, TrafficLightColorIdentifier colorIdentifier, string action)
    {
      var formattedIdentifier = trafficLightIdentifier.PadRight(8);
      var formattedColor = colorIdentifier.ToString().PadRight(11);
      return $"{formattedIdentifier}{formattedColor}» {action}";
    }
  }
}