using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Plant_Deprecated : MonoBehaviour // The generic class for all plant type objects
{
    public void Grow()
    {
        // Each species of plant will have its own mean growth rate and how much energy growth consumes
        // float growthRate = Mathf.Log(yGrowthConstant * timeAlive)*yGrowthConstant;
        // float xyGrowthRate = xzGrowthConstant * growthRate;
        // print(growthRate);
        // if (growthRate > 1f) // I should workshop this until I don't need an if statmement
        //     {
        //         transform.localScale = new Vector3(xyGrowthRate, growthRate, xyGrowthRate);
        //     }
    }

}
