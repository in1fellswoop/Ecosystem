using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MyTime
{
    public static float seconds;
    public static void Update()
    {
        seconds += Time.deltaTime;
    }

    // Helpful for determining offset required for true time experienced by object
    public static float InstantiatedAt()
    {
        return seconds;
    }

    public static void ProgressTime(float timeVariable)
    {
        timeVariable += Time.deltaTime;
    }

    public static float PrecisionSeconds(int precision = 2) // Assuming hundredths is sufficient
    {
        float factor = Mathf.Pow(10,precision);
        // We'll pretend that it's an int even though it's a float
        int  preciseSeconds = (int)(seconds * factor);

        return preciseSeconds/factor;
    }

    public static void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
}
