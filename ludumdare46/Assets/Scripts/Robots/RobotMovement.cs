using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.AI;

public class RobotMovement : MonoBehaviour
{

    public enum Type{
        Enemy, 
        Friend
    }

    public enum IdleAction{
        idle, 
        talk,
        bored
    }

    [SerializeField]
    Type typeRobot = Type.Enemy;

    [SerializeField]
    float distance = 10f;

    [SerializeField]
    float attackDistance = 1f;

    [SerializeField]
    Transform helpfulTarget;

    [SerializeField]
    IdleAction idleAction = IdleAction.idle;

    [SerializeField]
    RobotMovement [] friends;

    private bool canMove = true;

    NavMeshAgent agent;

    Vector3 startPosition;

    RobotAnimations robotAnimations;
    RobotEmotion robotEmotion;

    RobotDialog dialog;

    Health health;

    float timeStopped = 0f;

    CapsuleCollider colliderR;

    [HideInInspector]
    public bool angry = false;

    public static int deaths = 0;
    
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        robotAnimations = GetComponent<RobotAnimations>();
        health = GetComponent<Health>();
        robotEmotion = GetComponent<RobotEmotion>();
        startPosition = transform.position;
        colliderR = GetComponent<CapsuleCollider>();

        robotAnimations.attackEvent.AddListener(OnAttack);

        dialog = GetComponent<RobotDialog>();

        if(health){

            health.onHit.AddListener(delegate (){

                robotAnimations.TriggerHurt();

                //agent.SetDestination( PlayerMovement.instance.transform.position);

                for(int i =0; i < friends.Length; i++){
                    
                    friends[i].angry = true;

                }

                agent.Stop();

                angry = true;

            });

            health.onDeath.AddListener(delegate (){

                robotEmotion.ChangeEmotion(RobotEmotion.Emotion.Death);

                agent.Stop();

                deaths++;

                for(int i =0; i < friends.Length; i++){
                    
                    friends[i].angry = true;

                }

                robotAnimations.SetDeath(true);

                canMove = false;

                Destroy(agent);
                Destroy(dialog);
                Destroy(colliderR);
                Destroy(this);

                //Destroy(gameObject , 0.5f);

            });

        }

        agent.speed = Random.Range(6f , 8f);
        agent.acceleration = Random.Range(120f , 2000f);

    }

    void Update(){

        if(canMove && agent.isStopped){

            timeStopped += Time.deltaTime;

            if(timeStopped > 1f){

                agent.Resume();

                timeStopped = 0;

            }

        }

        

        agent.stoppingDistance = attackDistance - 0.4f;

        robotAnimations.SetMove(agent.velocity.magnitude > 2f);

        float tempDistance = Vector3.Distance(PlayerMovement.instance.transform.position , transform.position);

        if(typeRobot == Type.Enemy){

            if(distance >= tempDistance && tempDistance > attackDistance && canMove){

                 robotAnimations.SetTalk(false);
                robotAnimations.SetIdle2(false);

                robotEmotion.ChangeEmotion(RobotEmotion.Emotion.Angry);

               // if(agent.CalculatePath(transform.position , path)){

                if(!agent.pathPending){

                    if(!agent.SetDestination(PlayerMovement.instance.transform.position)){

                        agent.SetDestination(startPosition);
                        robotEmotion.Reset();

                    }

                }

                //}

            }else if( tempDistance <= attackDistance && canMove){

                 robotAnimations.SetTalk(false);
                robotAnimations.SetIdle2(false);

                //atakuj

                robotAnimations.TriggerAttack();

                Vector3 _direction = (PlayerMovement.instance.transform.position - transform.position).normalized;
                transform.LookAt(new Vector3(PlayerMovement.instance.transform.position.x , transform.position.y , PlayerMovement.instance.transform.position.z));

            }else{

                if(angry && canMove){

                    robotEmotion.ChangeEmotion(RobotEmotion.Emotion.Angry);

                    agent.SetDestination(PlayerMovement.instance.transform.position);

                    //angry = false;

                }else if(Vector3.Distance(startPosition , transform.position) < attackDistance){

                    if(dialog != RobotDialog.currentDialog){

                        robotEmotion.Reset();

                        switch(idleAction){
                        case IdleAction.idle:
                            robotAnimations.SetTalk(false);
                            robotAnimations.SetIdle2(false);
                            break;
                        case IdleAction.bored:
                            robotAnimations.SetTalk(false);
                            robotAnimations.SetIdle2(true);
                            break;
                        case IdleAction.talk:
                            robotAnimations.SetTalk(true);
                            robotAnimations.SetIdle2(false);
                            break;
                        }

                    }else{
                            robotAnimations.SetIdle2(false);

                    }

                }else if(canMove){

                    agent.SetDestination(startPosition);

                }

            }

        }else{

        
            if(Vector3.Distance(startPosition , transform.position) < attackDistance){

                if(dialog != RobotDialog.currentDialog){

                    robotEmotion.Reset();

                    switch(idleAction){
                        case IdleAction.idle:
                            robotAnimations.SetTalk(false);
                            robotAnimations.SetIdle2(false);
                            break;
                        case IdleAction.bored:
                            robotAnimations.SetTalk(false);
                            robotAnimations.SetIdle2(true);
                            break;
                        case IdleAction.talk:
                            robotAnimations.SetTalk(true);
                            robotAnimations.SetIdle2(false);
                            break;
                    }

                }else{
                        robotAnimations.SetIdle2(false);

                }

            }else if(canMove){

                agent.SetDestination(startPosition);

            }


        }


    }

    public void MoveToTheHelpfulTarget(){

        if(helpfulTarget == null)
            return;

        startPosition = helpfulTarget.position;

    }

    public void Stop(){

        agent.Stop();
        canMove = false;

    }

    public void Resume(){

        agent.Resume();
        canMove = true;

    }

    public void OnAttack(){

        float tempDistance = Vector3.Distance(PlayerMovement.instance.transform.position , transform.position);

        if(tempDistance <= attackDistance){

            Health hl = PlayerMovement.instance.GetComponent<Health>();

            if(hl != null){

                hl.Hit(1);

            }

        }

    }


}
