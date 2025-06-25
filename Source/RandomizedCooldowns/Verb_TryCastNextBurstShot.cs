using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RandomCooldowns;

[HarmonyPatch(typeof(Verb), "TryCastNextBurstShot")]
internal class Verb_TryCastNextBurstShot
{
    private const float Lowpoint = 0f;
    private const float Midpoint = 0.7f;
    private const float Highpoint = 1f;

    private static readonly SimpleCurve randomCooldownTicksCurve =
    [
        new CurvePoint(Lowpoint, -22),
        new CurvePoint(Midpoint, 0),
        new CurvePoint(Highpoint, 50)
    ];

    private static void Postfix(Verb __instance)
    {
        // A lot of things that aren't ranged attacks use TryCastNextBurstShot.
        // Also, don't alter cooldown between burst shots.
        if (!__instance.CasterIsPawn || __instance.verbProps?.nonInterruptingSelfCast == true ||
            !__instance.verbProps?.LaunchesProjectile == true || __instance.Bursting)
        {
            return;
        }

        // Don't modify extremely short cooldowns.
        if (__instance.CasterPawn?.stances?.curStance is not Stance_Cooldown { ticksLeft: > 29 } stance)
        {
            return;
        }

        var range = new FloatRange(0, 1f);
        // Take into account skillcap removers
        var levelInt = __instance.CasterPawn?.skills?.GetSkill(SkillDefOf.Shooting).levelInt;
        if (levelInt != null)
        {
            var skill = Math.Min((float)levelInt, 20);
            float skillPotency;
            switch (skill)
            {
                case < 10:
                    // If below 10 skill, add lower possible cooldown descrease in the randomizer
                    skillPotency = 1f * (skill / 10f);
                    range.min = Midpoint - (Midpoint * skillPotency);
                    break;
                case > 10:
                    // If above 10 skill, add lower possible cooldown increase in the randomizer
                    skillPotency = 1f * ((skill - 10) / 10f);
                    range.max = Highpoint - ((Highpoint - Midpoint) * skillPotency);
                    break;
            }
        }

        var random_ticks = (int)randomCooldownTicksCurve.Evaluate(Rand.Range(range.min, range.max));
        stance.ticksLeft += random_ticks;
        //Log.Message(
        //    $"{__instance.CasterPawn.NameFullColored} skill {skill}, potency {skillpotency}, range {range}, ticks {random_ticks}");
    }
}