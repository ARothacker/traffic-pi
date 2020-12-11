using System;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class ConsoleTrafficLightExecutor : ITrafficLightExecutor
  {
    public ConsoleTrafficLightExecutor()
    {
      Console.WriteLine($"--- {this.GetType()} initialized ---");
    }

    public void On(TrafficLight trafficLight, TrafficLightColor color)
    {
      Console.WriteLine(GetFormattedString(trafficLight.Identifier.ToString(), color, "ON"));
    }

    public void Off(TrafficLight trafficLight, TrafficLightColor color)
    {
      Console.WriteLine(GetFormattedString(trafficLight.Identifier.ToString(), color, "OFF"));
    }

    public void Dispose()
    {
      Console.WriteLine($"--- {this.GetType()} disposed ---");
    }

    private string GetFormattedString(string identifier, TrafficLightColor color, string action)
    {
      var formattedIdentifier = identifier.PadRight(8);
      var formattedColor = color.ToString().PadRight(11);
      return $"{formattedIdentifier}{formattedColor}» {action}";
    }
  }
}