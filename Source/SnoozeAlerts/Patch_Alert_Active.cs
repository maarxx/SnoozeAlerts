using HarmonyLib;
using RimWorld;
using Verse;

namespace SnoozeAlerts;

[HarmonyPatch(typeof(Alert))]
[HarmonyPatch("Active", MethodType.Getter)]
public class Patch_Alert_Active
{
    private static bool Prefix(Alert __instance, ref bool __result)
    {
        var val = SnoozedAlerts.snoozedAlerts.TryGetValue(__instance.GetType(), -1);
        if (val == -1)
        {
            return true;
        }

        if (val < Find.TickManager.TicksGame)
        {
            SnoozedAlerts.snoozedAlerts.Remove(__instance.GetType());
            return true;
        }

        __result = false;
        return false;
    }
}