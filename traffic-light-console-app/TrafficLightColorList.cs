using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ARWebApps.Learning.TrafficPi.TrafficLightsConsoleApp
{
  public class TrafficLightColorList : Collection<TrafficLightColor>
  {
    #region Fields

    private List<TrafficLightColor> colors;

    #endregion

    #region Properties

    public TrafficLightColor this[TrafficLightColorIdentifier identifier]
    {
      get => this.colors.Single(c => c.Identifier == identifier);
    }

    #endregion

    #region Constructors

    public TrafficLightColorList()
    {
      this.colors = new List<TrafficLightColor>
      {
        new TrafficLightColor(TrafficLightColorIdentifier.Red, 5000, false),
        new TrafficLightColor(TrafficLightColorIdentifier.RedYellow, 3000, true),
        new TrafficLightColor(TrafficLightColorIdentifier.Yellow, 3000, false),
        new TrafficLightColor(TrafficLightColorIdentifier.Green, 8000, false)
      };
    }

    #endregion

    #region Public Methods

    public List<TrafficLightColor> GetSingleLightColors()
    {
      return colors.Where(c => !c.HasMultipleLights).ToList();
    }

    public List<TrafficLightColor> GetCounterColors(TrafficLightColorIdentifier identifier)
    {
      return colors.Where(c => c.Identifier != identifier && !c.HasMultipleLights).ToList();
    }

    #endregion
  }
}