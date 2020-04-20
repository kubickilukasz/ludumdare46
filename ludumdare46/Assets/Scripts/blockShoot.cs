using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class blockShoot : MonoBehaviour
{

    static int triggers = 0;

    public CanvasGroup canv;
   
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement.instance.canShoot = false;
        canv.alpha = 1;
        triggers++;
    }

    private void OnTriggerExit(Collider other)
    {
        triggers--;
        PlayerMovement.instance.canShoot = triggers == 0;

        canv.alpha = triggers == 0 ? 0 : 1;
    }

}
