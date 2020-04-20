using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup panelMenu;

    protected bool active = false; 
    // Start is called before the first frame update

    protected bool tempCanShoot = true;

    void Start()
    {
        CheckActiveWindow();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerMovement.instance.isDead == false){

            active = !active;
            CheckActiveWindow();

        }

    }


    protected void CheckActiveWindow(){

    

        if(active){

            Time.timeScale = 0f;
            PlayerMovement.instance.canGo = false;
            panelMenu.alpha = 1f;
            panelMenu.interactable = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            panelMenu.blocksRaycasts = true;

            tempCanShoot = PlayerMovement.instance.canShoot ;

            PlayerMovement.instance.canShoot = false;
          //  panelMenu.active = true;

        }else{

            Time.timeScale = 1;
             PlayerMovement.instance.canGo = true;
             panelMenu.blocksRaycasts = false;
             panelMenu.alpha = 0f;
             panelMenu.interactable = false;
              Cursor.visible = false;
               Cursor.lockState = CursorLockMode.Locked;
               PlayerMovement.instance.canShoot = tempCanShoot;
           // panelMenu. = true;
        }

    }

    public void Quit(){

        Application.Quit();

    }

    public void Resume(){
        active = false;
        CheckActiveWindow();
    }
}
