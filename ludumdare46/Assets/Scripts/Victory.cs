using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : Menu
{
    // Start is called before the first frame update

    public Victory instance;

    public TextAsset winDialog;

    public RobotDialog robotDialog;

    public static bool fullCondition = false;

    bool callVictory = false;

    void Awake()
    {
        instance = this; 
        CheckActiveWindow();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && fullCondition && !callVictory){

            callVictory = true;

            // wygrana
            Alert.Call("HERO OF THE ROBOTS");

            if(robotDialog){

                robotDialog.dialogBasic = winDialog;
                robotDialog.LoadDialogs();
                robotDialog.StartDialog();
                robotDialog.onEndDialog.AddListener(ShowVictoryScreen);
                //Destroy(gameObject);

            }

        } 
    }

    void ShowVictoryScreen(){
        active = true;
        CheckActiveWindow();
    }
}
