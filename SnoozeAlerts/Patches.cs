using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using static RimWorld.Alert_UnusableMeditationFocus;

namespace SnoozeAlerts
{

    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            Log.Message("Hello from Harmony in scope: com.github.harmony.rimworld.maarx.snoozealerts");
            var harmony = new Harmony("com.github.harmony.rimworld.maarx.snoozealerts");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    static class SnoozedAlerts
    {
        public static Dictionary<Type, int> snoozedAlerts = new Dictionary<Type, int>();
    }

    [HarmonyPatch(typeof(Alert_PermitAvailable))]
    [HarmonyPatch("OnClick")]
    public class Patch_Alert_PermitAvailable_OnClick
    {
        static bool Prefix(Alert __instance)
        {
            if (Event.current.button == 1)
            {
                SnoozedAlerts.snoozedAlerts.Add(__instance.GetType(), Find.TickManager.TicksGame + 7500);
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Alert))]
    [HarmonyPatch("OnClick")]
    public class Patch_Alert_OnClick
    {
        static bool Prefix(Alert __instance)
        {
            if (Event.current.button == 1)
            {
                SnoozedAlerts.snoozedAlerts.Add(__instance.GetType(), Find.TickManager.TicksGame + 7500);
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Alert))]
    [HarmonyPatch("Active", MethodType.Getter)]
    public class Patch_Alert_Active
    {
        static bool Prefix(Alert __instance, ref bool __result)
        {
            int val = SnoozedAlerts.snoozedAlerts.TryGetValue(__instance.GetType(), -1);
            if (val == -1 )
            {
                return true;
            }
            else if (val < Find.TickManager.TicksGame)
            {
                SnoozedAlerts.snoozedAlerts.Remove(__instance.GetType());
                return true;
            }
            else
            {
                __result = false;
                return false;
            }
        }
    }
}
