using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent onDeath;
    public UnityEvent onHit;

    [SerializeField]
    int healthPoints = 10;

    [SerializeField]
    int maxHealthPoints = 10;

    [SerializeField]
    bool regeneration = false;

    [SerializeField]
    float time = 6f;

    float vTime = 0f;

    // Update is called once per frame
    void Update()
    {

        if(regeneration && healthPoints < maxHealthPoints){

            vTime += Time.deltaTime;

            if(vTime > time ){

                healthPoints+=2;
                vTime = 0;

            }

        }

    }

    public float GetHealth(){
        return ((float)healthPoints) / maxHealthPoints;
    }


    public void Hit(int hitPoints){

        healthPoints -= hitPoints;

        vTime = 0;

        if(healthPoints <= 0){

            onDeath.Invoke();

        }else{

            onHit.Invoke();

        }

    }
}
