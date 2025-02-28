using Content.Server.Speech.Components;
using Content.Shared.StatusEffect;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

/// <summary>
/// This handles confusion, causing the affected to have scrambled speech at random intervals.
/// Yes, I stole this from narcolepsy.
/// </summary>
public sealed class ConfusedAccentSystem : EntitySystem
{
    [ValidatePrototypeId<StatusEffectPrototype>]
    private const string StatusEffectKey = "Stutter"; // Same as cryptobiolin and mercury

    [Dependency] private readonly StatusEffectsSystem _statusEffects = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<ConfusedAccentComponent, ComponentStartup>(SetupConfusedAccent);
    }

    private void SetupConfusedAccent(EntityUid uid, ConfusedAccentComponent accentComponent, ComponentStartup args)
    {
        accentComponent.NextIncidentTime =
            _random.NextFloat(accentComponent.TimeBetweenIncidents.X, accentComponent.TimeBetweenIncidents.Y);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<ConfusedAccentComponent>();
        while (query.MoveNext(out var uid, out var confused))
        {
            confused.NextIncidentTime -= frameTime;

            if (confused.NextIncidentTime >= 0)
                continue;

            // Set the new time.
            confused.NextIncidentTime +=
                _random.NextFloat(confused.TimeBetweenIncidents.X, confused.TimeBetweenIncidents.Y);

            var duration = _random.NextFloat(confused.DurationOfIncident.X, confused.DurationOfIncident.Y);

            // Make sure the confusion time doesn't cut into the time to next incident.
            confused.NextIncidentTime += duration;

            _statusEffects.TryAddStatusEffect<ScrambledAccentComponent>(uid, StatusEffectKey,
                TimeSpan.FromSeconds(duration), false);
        }
    }
}
