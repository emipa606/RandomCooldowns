﻿using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RandomCooldowns;

[HarmonyPatch(typeof(Verb), "TryCastNextBurstShot")]
internal class Patch
{
    private const float Lowpoint = 0f;
    private const float Midpoint = 0.7f;
    private const float Highpoint = 1f;

    private static readonly SimpleCurve RandomCooldownTicksCurve = new SimpleCurve
    {
        // 70% of -21/2 + 30% of +49/2 = 0. Range doesn't include -22 or 50.
        new CurvePoint(Lowpoint, -22),
        new CurvePoint(Midpoint, 0),
        new CurvePoint(Highpoint, 50)
    };

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
        if (__instance.CasterPawn?.stances?.curStance is not Stance_Cooldown stance || stance.ticksLeft <= 29)
        {
            return;
        }

        var range = new FloatRange(0, 1f);
        // Take into account skillcap removers
        var levelInt = __instance.CasterPawn?.skills?.GetSkill(SkillDefOf.Shooting).levelInt;
        if (levelInt != null)
        {
            var skill = Math.Min((float)levelInt, 20);
            float skillpotency;
            switch (skill)
            {
                case < 10:
                    // If below 10 skill, add lower possible cooldown descrease in the randomizer
                    skillpotency = 1f * (skill / 10f);
                    range.min = Midpoint - (Midpoint * skillpotency);
                    break;
                case > 10:
                    // If above 10 skill, add lower possible cooldown increase in the randomizer
                    skillpotency = 1f * ((skill - 10) / 10f);
                    range.max = Highpoint - ((Highpoint - Midpoint) * skillpotency);
                    break;
            }
        }

        var random_ticks = (int)RandomCooldownTicksCurve.Evaluate(Rand.Range(range.min, range.max));
        stance.ticksLeft += random_ticks;
        //Log.Message(
        //    $"{__instance.CasterPawn.NameFullColored} skill {skill}, potency {skillpotency}, range {range}, ticks {random_ticks}");
    }
}