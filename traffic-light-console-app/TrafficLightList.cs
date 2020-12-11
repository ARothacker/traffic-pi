using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class TrafficLightList : Collection<TrafficLight>
  {
    public TrafficLight this[TrafficLightIdentifier identifier]
    {
      get => this.Single(l => l.Identifier == identifier);
    }
  }
}