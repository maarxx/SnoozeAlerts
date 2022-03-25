using System;
using System.Collections.Generic;

namespace SnoozeAlerts;

internal static class SnoozedAlerts
{
    public static Dictionary<Type, int> snoozedAlerts = new Dictionary<Type, int>();
}