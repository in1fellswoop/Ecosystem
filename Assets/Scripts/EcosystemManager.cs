using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EcosystemManager : MonoBehaviour
{
    public float worldSeconds;

    public GameObject ground;
    public GameObject sappling;
    public GameObject tree;

    [Range(0f,100f)]
    public float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        MyTime.SetTimeScale(timeScale);
        // SpawnSapplings(ground, sappling,);
        SpawnPlant(ground, tree, plantDensity:.5f);

    }

    void Update()
    {
        worldSeconds = MyTime.PrecisionSeconds();
        MyTime.SetTimeScale(timeScale);
    }
    

    public void SpawnSapplings(GameObject terrain, GameObject sapplingToSpawn, float sapplingDensity = 1.0f, bool jitter = true)
    {
        // In this implementation, I am going to iterate over 1m square regions on a square plane
        // If the random probability is less than the density, I will spawn a sappling at the center of that 
        // 1m square. I could add randomness to this, but I will wait

        // Realistically, if it's a square plane, I won't need both but whatever
        // This also assumes a plane with interger capable dimensions
        int xScale = (int)terrain.transform.localScale.x*5;
        int zScale = (int)terrain.transform.localScale.z*5;

        for (int i = -xScale; i < xScale; i++)
        {
            for (int j = -zScale; j < zScale; j++)
            {
                float xOffset = jitter ? UnityEngine.Random.Range(0f,1f) : .5f;
                float zOffset = jitter ? UnityEngine.Random.Range(0f,1f) : .5f;

                Vector3 coord = new Vector3 (i+xOffset, 0f, j+zOffset);

                // print(coord);
                if (UnityEngine.Random.Range(0f,1f) < sapplingDensity)
                {
                    Instantiate(sapplingToSpawn, coord, Quaternion.identity);                    
                }
            }
        }
    }

    
    

    public void SpawnPlant(GameObject terrain, GameObject plantTypeToSpawn, float plantDensity = 1.0f, bool jitter = true)
    {
        // In this implementation, I am going to iterate over 1m square regions on a square plane
        // If the random probability is less than the density, I will spawn a sappling at the center of that 
        // 1m square. I could add randomness to this, but I will wait

        // Realistically, if it's a square plane, I won't need both but whatever
        // This also assumes a plane with interger capable dimensions
        int xScale = (int)terrain.transform.localScale.x*5;
        int zScale = (int)terrain.transform.localScale.z*5;

        for (int i = -xScale; i < xScale; i++)
        {
            for (int j = -zScale; j < zScale; j++)
            {
                float xOffset = jitter ? UnityEngine.Random.Range(0f,1f) : .5f;
                float zOffset = jitter ? UnityEngine.Random.Range(0f,1f) : .5f;

                Vector3 coord = new Vector3 (i+xOffset, 0f, j+zOffset);

                // print(coord);
                if (UnityEngine.Random.Range(0f,1f) < plantDensity)
                {
                    Instantiate(plantTypeToSpawn, coord, Quaternion.identity);                    
                }
            }
        }
    }
}
