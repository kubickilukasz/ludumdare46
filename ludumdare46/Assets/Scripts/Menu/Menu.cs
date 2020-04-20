using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup panelMenu;

    private bool active = false; 
    // Start is called before the first frame update

    protected bool tempCanShoot = true;
    protected bool tempCanGo = true;

    protected static int activeWindows = 0;

    void Start()
    {
        //activeWindows = 0;
        CheckActiveWindow();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(activeWindows);

        if(Input.GetKeyDown(KeyCode.Escape) && PlayerMovement.instance.isDead == false){

            ChangeActive();
            CheckActiveWindow();

        }

    }


    protected void CheckActiveWindow(){

        if(active){

            Time.timeScale = 0f;
            panelMenu.alpha = 1f;
            panelMenu.interactable = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            panelMenu.blocksRaycasts = true;

            tempCanShoot = PlayerMovement.instance.canShoot ;
            tempCanGo = PlayerMovement.instance.canGo ;

            PlayerMovement.instance.canShoot = false;
            PlayerMovement.instance.canGo = false;
          //  panelMenu.active = true;

        }else{

            if(activeWindows == 0){

                Time.timeScale = 1;
                //PlayerMovement.instance.canGo = true;
                PlayerMovement.instance.canGo = tempCanGo;
                panelMenu.blocksRaycasts = false;
                panelMenu.alpha = 0f;
                panelMenu.interactable = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerMovement.instance.canShoot = tempCanShoot;

            }
           // panelMenu. = true;
        }

    }

    public void Quit(){

        Application.Quit();

    }

    public void Resume(){
        //active = false;
        ChangeActive(false);
        CheckActiveWindow();
    }

    protected void ChangeActive(){

        active = !active;

        if(active){
            activeWindows++;
        }else{
            activeWindows--;
        }

    }

    protected void ChangeActive(bool newActive){

        if(newActive != active){

            active = newActive;

            if(active){
                activeWindows++;
            }else{
                activeWindows--;
            }

        }

    }
}
