using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Plant : MonoBehaviour // The generic class for all plant type objects
{

    // What do all plants have?
    // Should I have a normalized and an absolute attirbutes e.g., 100% energy is 1234.5 joules

    // Health
    [Range(0f,100f)]
    public float health = 100; 

    // Energy requirement (hunger for a lack of a better term)
    [Range(0f,100f)]
    public float energy = 0f;

    // Energy reserves
    [Range(0f,100f)]
    public float energyReserve = 0f;


    // Time    
    // float time = GameObject.FindGameObjectWithTag("SimulationManager").GetComponent<EcosystemManager>().seconds;


    // Thirst
    public float thirst = 0f;

    // Growth
    public float yGrowthConstant;
    public float xzGrowthConstant;
    // float yGrowthRate;
    // float xzGrowthRate;

    // Color
    // Can use 180 degree additions to get complementary colors of each Hue
    MeshRenderer treeMesh;
    Material[] leafMaterials;

    // Roots
    public static float rootRadius = 1f;


    // Leaves

    private float instantiatedAt;
    public float timeAlive;

    public float absHeight;
    public float absWidth;
    // private float growthDelta = .01f;
    private float currOffset;

    void Awake()
    {
        instantiatedAt = MyTime.InstantiatedAt();
        treeMesh = GetComponent<MeshRenderer>();
        leafMaterials = treeMesh.materials;
        absHeight = 1f;
        absWidth = .25f;
        currOffset = 0f;
        Color rgba = new Color(MyMath.GetRandomFloat(),MyMath.GetRandomFloat(),MyMath.GetRandomFloat(),1f);
        leafMaterials[0].color = rgba;
        treeMesh.materials = leafMaterials;
    }

    void Update()

    {
        timeAlive = MyTime.seconds - instantiatedAt;
        // rootRadius = transform.localScale.x;
    }


    // Base level functions
    // decrement energy?

    // Helpful plant functions
    private void ManageEnergy(float netChange,float min = 0f, float max = 100f)
    {
        // Let's wait on energy reserves
        // Need to see if there's leftover energy first
        // energyReserve = MyMath.Clamp(energy + netChange - max, min, max);

        // netChange can be positve or negative
        energy = MyMath.Clamp(energy + netChange, min, max);
    }

    



    // What do all plants do?
    // Take in and expend energy --> and store energy?
    // Each of these things require energy to perform, even if they are energy producing
    
    // Grow
    public void ManageGrowth(float minEnergyRequired, float energyCost)
    {
        if (MyMath.AtOrAboveThreshold(energy, minEnergyRequired))
        {
            Grow();
            energy -= energyCost;
        }
    }
    public void Grow()
    {        
        currOffset += Time.deltaTime / 100f; // In this case, it will grow .01f every second

        float yScale = (absHeight + currOffset)/absHeight;
        float xzScale = (absWidth + currOffset)/absWidth;
        
        transform.localScale = new Vector3(xzScale, yScale, xzScale);
    }


    // Photosynthesize

    public void Photosynthesize(GameObject plantObject)
    {
        // We want to access the surface area of the leaves to determine:
        // float leavesECost = 0f; // some cost of having leaves
        // float leavesEGain = 0f; // some gain of having leaves
        // I could also just have a profit variable that stores the net change in energy
        float netGain = 1f;
        if (energy == 100f)
        {
            energyReserve += netGain;
        } else
        {
            energy += netGain;
        }
    }


    // Drink


    // Die
}
