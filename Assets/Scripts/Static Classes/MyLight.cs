using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyLight
{
    public static float ComplimentaryColor(float hue)
    {
        return hue + 180f;
    }
}
