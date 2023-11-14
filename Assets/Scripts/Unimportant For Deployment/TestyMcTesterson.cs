using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestyMcTesterson : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        // float test2 = 0f;
        // print("Testing natural dropoff function\n\n");
        // for (int i = -2; i < 2; i++)
        // {
        //     test2 = MyMath.NaturalDropOff(i);
        //     print(test2);
        // }
        
        // print("Testing natural dropoff function (without normalization)\n\n");
        // for (int i = -2; i < 2; i++)
        // {
        //     test2 = MyMath.NaturalDropOff(i, normalized:false);
        //     print(test2);
        // }
        
        // print("Testing power dropoff function\n\n");
        // for (int i = -2; i < 2; i++)
        // {
        //     test2 = MyMath.PowerDropOff(i,maxScalar:2f);
        //     print(test2);
        // }
        
        // print("Testing power dropoff function (without normalization)\n\n");
        // for (int i = -2; i < 2; i++)
        // {
        //     test2 = MyMath.PowerDropOff(i,maxScalar:2f,normalized:false);
        //     print(test2);
        // }

        // time = GameObject.FindGameObjectWithTag("SimulationManager").GetComponent<EcosystemManager>().seconds;
        // print(time.GetType());
        // print(time);
        

        float[] test = MyMath.CreatePseudoNormalDistribution();
        // foreach (float var in test)
        // {
        //     print(var);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // print(EcosystemManager.seconds);
    }
}
