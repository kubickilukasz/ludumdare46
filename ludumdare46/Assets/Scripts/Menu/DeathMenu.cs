using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : Menu
{
    void Start()
    {
        CheckActiveWindow();

        PlayerMovement.instance.health.onDeath.AddListener(onDeath);

    }

//UPADE MUSI ZOSTAC
      void Update(){
          //zostawww
    }

    public void RestartGame(){
        activeWindows = 0;
        SceneManager.LoadScene(0);
    }

    void onDeath(){
        ChangeActive(true);
        Alert.Call("YOU ARE DEAD!");
        CheckActiveWindow();
    }






}
