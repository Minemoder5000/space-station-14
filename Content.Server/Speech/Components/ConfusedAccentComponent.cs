using System.Numerics;
using Content.Server.Speech.EntitySystems;

namespace Content.Server.Speech.Components;

/// <summary>
/// This is used for the confused trait.
/// TODO: Replace with a random chance instead of a time
/// </summary>
[RegisterComponent, Access(typeof(ConfusedAccentSystem))]
public sealed partial class ConfusedAccentComponent : Component
{
    /// <summary>
    /// Percent chance per message sent to be scrambled.
    /// </summary>
    [DataField]
    public float ChanceToScramble = 1f;


}
