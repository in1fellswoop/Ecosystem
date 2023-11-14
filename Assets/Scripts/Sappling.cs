using System;
using System.Collections;
using UnityEngine;

public class Sappling : MonoBehaviour
{
    // [Range(0.1f, 1f)]
    public float growthConstant = 1f;
    public float xyGrowthConstant = 1.005f;
    public float growthRate;
    public float horizGrowthRate;

    // public Vector3 scale;
    // public float xScale;
    // public float yScale;
    // public float zScale;

    public float time; // At some point, I might create a static class that will allow a method to access the passage of time


    private Vector3 coord;
    public GameObject tree;


    private float[] probabilities;
    
    // Start is called before the first frame update
    void Start()
    {
        coord = transform.position;

        PseudoNormalDist();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Grow();
        ManageTreeTransition();
        // RandomDeath(); This is going to need workshopping
    }

    private void Grow()
    {
        growthRate = Mathf.Log(growthConstant * time)*1.75f;
        float xyGrowthRate = xyGrowthConstant * growthRate;
        if (growthRate > 1f)
        {
            transform.localScale = new Vector3(xyGrowthRate, growthRate, xyGrowthRate);
        }
    }

    private void ManageTreeTransition()
    {
        if (transform.localScale.y >= 10f)
        {
            BecomeTree();   
        }
    }

    private void BecomeTree()
    {
        Destroy(gameObject);
        Instantiate(tree, coord, Quaternion.identity);
    }

    private float NormalDistributionFormula(float x_value)
    {
        float e_exp = Mathf.Pow(-.5f * x_value,2);
        float denominator = Mathf.Pow(2*Mathf.PI,.5f);
        float probability = Mathf.Exp(e_exp)/denominator;

        return probability;
    }

     // Might be good to have it be symmetrical: could just subtract by half the target value, or insert both
     // pos and negative values (would require array size to be an odd number but almost 2x as large as granularity)
     // Could also be centered as zero
     // Or not, I guess order wouldn't matter, sinze we'll be indexing randomly
     // I could do this with a while loop as well
    private void PseudoNormalDist(float arb_max = 7f, int granularity = 10000)
    {
        float stepSize = arb_max / granularity; // Based on an arbitrarily determined maximum

        this.probabilities = new float[granularity];

        for (int i = 0; i < granularity; i++)
        {
            float xVar = stepSize * i;
            this.probabilities[i] = NormalDistributionFormula(xVar);
        }
    }

    private void RandomDeath(int granularity = 10000)
    {
        int randIndex = UnityEngine.Random.Range(0, granularity);

        if (this.probabilities[randIndex] > 6.96f)
        {
            Destroy(gameObject);
            print(this.probabilities[randIndex]); // This is going to need workshopping
            print("A sappling has died");
        }
    }







    private void TestPseudoRandomGeneration()
    {
                // Test psuedoNormal
        int lessThanHalf = 0;
        int lessThan1 = 0;
        int lessThan3Half = 0;
        int lessThan2 = 0;
        int lessThan5Half = 0;
        int lessThan3 = 0;

        for (int i = 0; i < 1000; i++)
        {
            float test = probabilities[UnityEngine.Random.Range(0,1000)];
            if (test < .5f)
            {
                lessThanHalf += 1;
            } else if (test < 1f)
            {
                lessThan1 += 1;                
            } else if (test < 1.5f)
            {
                lessThan3Half += 1;                
            } else if (test < 2f)
            {
                lessThan2 += 1;                
            } else if (test < 2.5f)
            {
                lessThan5Half += 1;                
            } else if (test < 3f)
            {
                lessThan3 += 1;                
            }
        }

        print((lessThanHalf, lessThan1, lessThan3Half, lessThan2, lessThan5Half, lessThan3));
    }
}
