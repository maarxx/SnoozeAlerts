using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace SnoozeAlerts;

[HarmonyPatch(typeof(Alert_UnusableMeditationFocus.Alert_PermitAvailable))]
[HarmonyPatch("OnClick")]
public class Patch_Alert_PermitAvailable_OnClick
{
    private static bool Prefix(Alert __instance)
    {
        if (Event.current.button != 1)
        {
            return true;
        }

        SnoozedAlerts.snoozedAlerts.Add(__instance.GetType(), Find.TickManager.TicksGame + 7500);
        return false;
    }
}