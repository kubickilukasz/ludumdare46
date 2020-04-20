using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : Menu
{

    float Vtime = 0f;
    float Vtime2 = 0f;

    float time = 3f;
    float time2 = 2f;

    bool called = false;

    void Start()
    {
        ChangeActive(true);
        CheckActiveWindow();   

    }

    // Update is called once per frame
    void Update()
    {   

        Vtime += Time.unscaledDeltaTime;

        if(Vtime > time){

            panelMenu.alpha = (1 - Vtime2 / time2);   

            Vtime2 += Time.unscaledDeltaTime;

            if(Vtime2 > time2 && !called){

                called = true;

                ChangeActive(false);
                CheckActiveWindow();
                Alert.Call("WHERE AM I?");

            }

            
        }
        
    }
}
