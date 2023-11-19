using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    
    // Start is called before the first frame update
    void Start()
    {
        yGrowthConstant = 1f;
        xzGrowthConstant = .15f;
        
    }

    // Update is called once per frame
    void Update()
    {
        Photosynthesize();
        Drink();
        // Thirst();
        ManageGrowth();
        // energy += .02f; // This is why energy increases regardless of timescale
        // print(transform.localScale.y);
        print(rootRadius);
    }
}
