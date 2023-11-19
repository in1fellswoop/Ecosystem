using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath : MonoBehaviour
{
    public static float GetRandomFloat()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }

    public static float GetRandomInt(int min, int max, bool inclusive = false)
    {
        int randInt = inclusive ? UnityEngine.Random.Range(min, max+1) : UnityEngine.Random.Range(min, max);
        return randInt; 
    }

    public static float[] CreatePseudoNormalDistribution(float arb_max = 7f, int granularity = 10000)
    {
        float[] probabilities = new float[granularity];

        float stepSize = arb_max / granularity; // Based on an arbitrarily determined maximum
        for (int i = 0; i < granularity; i++)
        {
            float xVar = stepSize * i;
            probabilities[i] = PseudoNormalDistribution(xVar);
            // Debug.Log(probabilities[i]);
        }

        return probabilities;

    }

    public static float NaturalDropOff(float dist = 0f, float maxScalar = 1f, bool normalized = true)
    {
        // -.5(x-g)^2
        float e_exp = -.5f * Mathf.Pow(dist,2);
        // e ^ e_exp
        float numerator = Mathf.Exp(e_exp);
        // 1/scalar * (2*PI)^.5
        float denominator = Mathf.Pow(maxScalar, -1) * Mathf.Sqrt(2*Mathf.PI);
        
        float dropOff = numerator / denominator;

        if (normalized)
        {
            dropOff = Normalize(dropOff, denominator);
        }

        return LeftClamp(dropOff);
    }

    public static float PowerDropOff(float dist = 0f, float slope = 1f, float maxScalar = 0f, bool normalized = true)
    {

        float dropOff = -slope * Mathf.Pow(dist, 2) + maxScalar;

        if (normalized)
        {
            // The absolute maximum occurs at 0, so I am dividing everything by the maxScalar
            dropOff = Normalize(dropOff, maxScalar);
        }
        //  -ax^2 +  c
        return LeftClamp(dropOff);
    }

    public static float Normalize(float val, float globalMax)
    {
        return globalMax == 0 ? 0 : val / globalMax;
    }

    public static float LogisticGrowth()
    {
        // Max / 1 + e^(-slope(x-x_value of midpoint))
        return 0f;
    }

    public static float LeftClamp(float val, float min = 0f)
    {
        return val < min ? min : val;
    }

    public static float RightClamp(float val, float max = 100f)
    {
        return val > max ? max : val;
    }

    public static float Clamp(float val, float min = 0f, float max = 100f)
    {
        return val > max ? max : val < min ? min : val;
    }

    public static bool AtOrAboveThreshold(float val, float threshold)
    {
        return val >= threshold;
    }

    public static bool BelowThreshold(float val, float threshold)
    {
        return val < threshold;
    }

    public static float DistanceMagnitude2D(Vector3 pointA, Vector3 pointB)
    {
        float aSquared = Mathf.Pow(pointB.x - pointA.x, 2);
        float bSquared = Mathf.Pow(pointB.z - pointA.z, 2);
        float magnitude = Mathf.Pow(aSquared + bSquared, .5f);

        return magnitude;
    }

    public static float CalcVol(Vector3 objScale)
    {
        float vol = objScale.x * objScale.y * objScale.z;

        return vol;
    }

    public static float CalcArea(Vector3 objScale)
    {
        // Arbitrarily choosing x and y
        float area = objScale.x * objScale.y;

        return area;
    }



    private static float PseudoNormalDistribution(float x_value)
    {
        float e_exp = Mathf.Pow(.5f * x_value,2);
            // Debug.Log(e_exp);
        float denominator = Mathf.Pow(2*Mathf.PI,.5f);
            // Debug.Log(probabilities[i]);
        float probability = Mathf.Exp(-e_exp)/denominator;
            // Debug.Log(probabilities[i]);

        return probability;
    }


    



     // Might be good to have it be symmetrical: could just subtract by half the target value, or insert both
     // pos and negative values (would require array size to be an odd number but almost 2x as large as granularity)
     // Could also be centered as zero
     // Or not, I guess order wouldn't matter, sinze we'll be indexing randomly
     // I could do this with a while loop as well
}
