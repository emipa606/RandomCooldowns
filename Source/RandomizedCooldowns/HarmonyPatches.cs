using HarmonyLib;
using Verse;

namespace RandomCooldowns;
// Add a random value to ranged attack cooldowns.
// Cooldown decreases (bonuses) are more common than increases, but also smaller.
// Average (mean) cooldowns are the same as vanilla.
// The change is not a multiplier, so it's most noticeable with faster weapons.
// Works with most modded weapons, but does nothing if cooldowns are already very short.

[StaticConstructorOnStartup]
internal static class HarmonyPatches
{
    static HarmonyPatches()
    {
        var harmony = new Harmony("iron_xides.random_cooldowns");
        harmony.PatchAll();
    }
}