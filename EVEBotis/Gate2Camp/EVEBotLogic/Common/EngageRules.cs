#region

using GalaSoft.MvvmLight;

#endregion

namespace Gate2Camp.EVEBotLogic.Common
{
  /// <summary>
  ///   Class that will define the engagement rules of the combat bot
  /// </summary>
  public class EngageRules : ObservableObject
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="EngageRules" /> class.
    /// </summary>
    /// <param name="maxRange">The maximum range.</param>
    /// <param name="goBrawl">The go brawl.</param>
    /// <param name="useProp">The use property.</param>
    public EngageRules(double? maxRange = null, bool? goBrawl = null, bool? useProp = null, bool? useRepairer = null)
    {
      MaxRange = maxRange;
      GoBrawl = goBrawl;
      UsePropulsion = useProp;
      UseRepairer = useRepairer;
    }

    /// <summary>
    ///   The maximum range
    /// </summary>
    public double? MaxRange { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether [go brawl].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [go brawl]; otherwise, <c>false</c>.
    /// </value>
    public bool? GoBrawl { get; set; }

    public bool? UseRepairer { get; set; }

    /// <summary>
    ///   Gets or sets the activate propulsion.
    /// </summary>
    /// <value>
    ///   The activate propulsion.
    /// </value>
    public bool? UsePropulsion { get; set; }
  }
}