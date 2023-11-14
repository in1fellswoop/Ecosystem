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
        ManageGrowth(10f, .5f);
        energy += .02f;
        print(transform.localScale.y);
    }
}
