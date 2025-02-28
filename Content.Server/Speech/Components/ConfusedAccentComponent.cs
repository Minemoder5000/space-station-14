using System.Numerics;
using Content.Server.Speech.EntitySystems;

namespace Content.Server.Speech.Components;

/// <summary>
/// This is used for the confusion trait.
/// </summary>
[RegisterComponent, Access(typeof(ConfusedAccentSystem))]
public sealed partial class ConfusedAccentComponent : Component
{
    /// <summary>
    /// The random time between incidents, (min, max).
    /// </summary>
    [DataField("timeBetweenIncidents", required: true)]
    public Vector2 TimeBetweenIncidents { get; private set; }

    /// <summary>
    /// The duration of incidents, (min, max).
    /// </summary>
    [DataField("durationOfIncident", required: true)]
    public Vector2 DurationOfIncident { get; private set; }

    public float NextIncidentTime;
}
