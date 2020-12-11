using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  #region Properties

  public class TrafficLightList : Collection<TrafficLight>
  {
    public TrafficLight this[TrafficLightIdentifier identifier]
    {
      get => this.Single(l => l.Identifier == identifier);
    }

    #endregion

    #region Public Methods

    public List<TrafficLight> GetCounterTrafficLights(TrafficLightIdentifier identifier)
    {
      return this.Where(c => c.Identifier != identifier).ToList();
    }

    #endregion
  }
}