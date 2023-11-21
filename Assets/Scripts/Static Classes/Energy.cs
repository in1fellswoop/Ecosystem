using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public static class Energy
{
    private static float caloriesToJoules = 4.184f;

    public static float WattsToJoules(float watts, float duration)
    {
        return watts * duration;
    }

    public static float JoulesToCalories(float joules)
    {
        return joules / caloriesToJoules;
    }
}
