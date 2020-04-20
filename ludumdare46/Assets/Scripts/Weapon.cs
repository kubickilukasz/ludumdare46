using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{

    Animator anim;

    [SerializeField]
    GameObject shootParticleEffect;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform transformCamera;

    AudioSource asource;

    bool canShoot = true;
    bool shooted = false;

    float currentType = 0f;
    float goalType = 0f;
    float vType = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        
        if(PlayerMovement.instance.canShoot){

            if( /*(state.IsName("shoot") &&  state.normalizedTime >= 0.9f ) ||*/ ( !state.IsName("shoot") && !state.IsName("idle_gun"))){
                canShoot = true;
            }else{
                anim.SetBool("shoot" , false);

                if(!shooted){

                    asource.PlayOneShot(asource.clip);

                    var obiekt = Instantiate(shootParticleEffect , transform.position , transform.rotation , transform);
                    Instantiate(bullet , transformCamera.position , transformCamera.rotation);

                    Destroy(obiekt , 2f);

                    shooted = true;
                }


            }

            if(Input.GetButton("Fire1") && canShoot){

                shooted = false;

                anim.SetBool("shoot" , true);

                canShoot = false;

            }

        }
             
        currentType = Mathf.SmoothDamp(currentType , goalType , ref vType , 0.08f );
        
        anim.SetFloat("type" , currentType);


    }

    public void  TriggerFall(){

        anim.SetTrigger("fall");

    }

    public void SetType(float type){

        goalType = type;

    }

    public void TriggerShoot(){

        anim.SetTrigger("shoot");

    }

}
