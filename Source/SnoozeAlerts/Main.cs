using System.Reflection;
using HarmonyLib;
using Verse;

namespace SnoozeAlerts;

[StaticConstructorOnStartup]
internal class Main
{
    static Main()
    {
        Log.Message("Hello from Harmony in scope: com.github.harmony.rimworld.maarx.snoozealerts");
        var harmony = new Harmony("com.github.harmony.rimworld.maarx.snoozealerts");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}