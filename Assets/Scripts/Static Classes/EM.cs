using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EM
{
    public static float Einsteins(float wavelength)
    {
        // Of a singular quanta of light
        // May not need this tbh but just in case
        // Returns in kcal
        // Should this be a general list where element 0 is the magnitude and element 1 is the unit? probably not
        return 28_600f/wavelength;
    }
}
