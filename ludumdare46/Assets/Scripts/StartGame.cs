using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : Menu
{
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        CheckActiveWindow();   

    }

    float Vtime = 0f;
    float Vtime2 = 0f;

    float time = 3f;
    float time2 = 2f;

    bool called = false;

    

    // Update is called once per frame
    void Update()
    {



        Vtime += Time.deltaTime;

        if(Vtime > time){

            panelMenu.alpha = (1 - Vtime2 / time2);

            Vtime2 += Time.deltaTime;

            if(Vtime2 > time2 && !called){

                called = true;

                active = false;
                CheckActiveWindow();
                Alert.Call("WHERE AM I?");

            }

            
        }
        
    }
}
