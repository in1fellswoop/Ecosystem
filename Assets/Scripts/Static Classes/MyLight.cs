using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyLight
{
    public static float ComplimentaryColor(float hue)
    {
        return hue + 180f;
    }

    // Color.RGBToHSV(new Color(0.9f, 0.7f, 0.1f, 1.0F), out H, out S, out V);
    // m_Renderer.material.color = Color.HSVToRGB(m_Hue, m_Saturation, m_Value);
}
