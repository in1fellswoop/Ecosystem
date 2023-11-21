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
    public float energy;

    // Energy reserves
    public float glucose;

    // Water
    public float water;

    // Size ~ Scale
    private Vector3 currScale;

    // Cost/Production variables
    // Cost
    private float basalMetabolism;
    [SerializeField]
    private float growthCost;
    [SerializeField]
    private float basalWater;
    // private float photosynthesisCost; // Shelving this for now
    private float reproductionCost;
    

    // Production
    // Should I change to leafProd and rootProd?
    private float photosynthesisProd;
    private float waterProd;


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
    [SerializeField]
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

        energy = 10f;

        // resources
        energy = glucose = water = 0f;

        // Cost val
        basalMetabolism = basalWater = growthCost = reproductionCost = 0f;
    }

    void Update()

    {
        timeAlive = MyTime.seconds - instantiatedAt;
        // rootRadius = transform.localScale.x;
        currScale = transform.localScale;
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
    // Should minEnegeryRequired be the growthCost?
    // At some point I might need to create a decision tree ... no pun intended; idk if this is something that plants do
    public void ManageGrowth()
    {
        if (MyMath.AtOrAboveThreshold(glucose, growthCost) && MyMath.AtOrAboveThreshold(water, growthCost))
        {
            Grow();
            // Should I make these integers? To have a quantized growth cost in moles? How will this scale smoothly?
            glucose -= growthCost;
            water -= growthCost;
            GrowthRamifications();
        }
    }
    public void Grow()
    {        
        currOffset += Time.deltaTime / 100f; // In this case, it will grow .01m (abs height) every second

        float yScale = (absHeight + currOffset)/absHeight;
        float xzScale = (absWidth + currOffset)/absWidth;
        
        transform.localScale = new Vector3(xzScale, yScale, xzScale);
    }

    private void GrowthRamifications()
    {
        // Increase cost of being larger
        // Making correlated with vol as # of cells increase with vol and vol increases with scale
        // In this case, .5 is some arbitrary constant, as a tree is not a cube, and therefore would not
        // be exactly equal to the value otherwise       
        float arbConst = .05f;
        // As localScale changes over time, so do the cost of abc... etc
        float vol = MyMath.CalcVol(transform.localScale);
        // Will probably need unique constants for each cost var as photoSyntehsisCost shouldn't be the same as basalCost lmao
        basalMetabolism = vol * arbConst;
        growthCost = vol * arbConst; 
        basalWater = vol * arbConst * .005f;   
        // photosynthesisCost = vol * .000005f;        
        reproductionCost = vol * arbConst;

        // Growth should cost more than just energy, it should cost other resources
        // nutrients? water? what else?

        // Production
        // Assuming less loss as scale increases compared to vol
        arbConst = .75f;
        // float area = MyMath.CalcArea(transform.localScale);
        float conicalArea = MyMath.CalcAreaOfCone(transform.localScale);
        photosynthesisProd = conicalArea * arbConst;
        waterProd = vol * arbConst; // And argument could be made that vol (with a diff const) would be more accurate for roots

    }


    // Photosynthesis

    public void ObtainSunlightEnergy()
    {
        // This function can be upgrade by taking into account the following:
        // Plant specific efficiency (absorption spectra) and more accurate conversion to kcal -- anything else?
        energy += photosynthesisProd * .5f;
    }

    public void Photosynthesis()
    {
        // We want to access the surface area of the leaves to determine:
        // float leavesECost = 0f; // some cost of having leaves
        // float leavesEGain = 0f; // some gain of having leaves
        // I could also just have a profit variable that stores the net change in energy
        // float netGain = 1f;
        // if (energy == 100f)
        // {
        //     energyReserve += netGain;
        // } else
        // {
        //     energy += netGain;
        // }

        // 6CO2 + 6H20 + 450kcal --> C6H12O6 + 6O2 -- ignoring CO2 for now
        if (water >= 6f && energy >= 75f)
        {
            water -= 6f;
            energy -= 75f;
            glucose += 1f;            
        }
    }



    // Drink

    public void Drink()
    {
        // Can have it such that a base level of water needs to be maintained, below that thirst will increase
        // This will hinder photosynthesis and therefore energy production and growth

        
        // energy -= drinkCost;
        // This should be in moles
        water += waterProd;
    }

    public void Thirst()
    {
        thirst = -(water - basalWater); // Will need to fix this
    }


    // Die
}
