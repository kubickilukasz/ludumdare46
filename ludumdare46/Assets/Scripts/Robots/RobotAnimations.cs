using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;

public class RobotAnimations : MonoBehaviour
{

    public UnityEvent attackEvent;

    Animator anim;

    public float normalizedAttackTime = 0.5f;
    bool wasAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update(){

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if(!wasAttack && state.IsName("attack1") && state.normalizedTime >= normalizedAttackTime){
            attackEvent.Invoke();
            wasAttack = true;
        }else if(state.IsName("attack1") && state.normalizedTime < normalizedAttackTime){
            wasAttack = false;
        }

    }

    // Update is called once per frame
    public void  TriggerAttack(){

        anim.SetTrigger("attack");

    }

    public void SetTalk(bool talk){

        anim.SetBool("talk" , talk);

    }

    public void SetIdle2(bool idle2){

        anim.SetBool("idle2" , idle2);

    }

    public void TriggerHurt(){

        anim.SetTrigger("hurt" );

    }

    public void SetMove(bool move){

        anim.SetBool("move" , move);

    }

    public void SetDeath(bool death){

        anim.SetBool("death" , death);

    }

    
}
