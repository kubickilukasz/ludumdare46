using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

   [SerializeField]
    CameraMovement myCamera;
    
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float smooth = 0.1f;

    [SerializeField]
    float gravity = -9f;

     [SerializeField]
    float jump = 5f;

     [SerializeField]
    float maxJump = 5f;

    [SerializeField]
    CanvasGroup deathShow;


    CharacterController characterController;
    public Health health;
    Weapon weapon;

    Vector3 currentDirection;
    Vector3 vDirection;

    float _yVelocity = 0;

    public static PlayerMovement instance;

    public bool canGo = true;
    public bool canShoot = true;

    public bool isDead = false;

    bool jumped = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        weapon = GetComponentInChildren<Weapon>();

        health.onHit.AddListener(Hit);

    }

    // Update is called once per frame
    void Update()
    {

        currentDirection.y = 0;

       // Debug.Log(canGo);

        transform.rotation = Quaternion.Euler(new Vector3(0 , myCamera.Direction.y , 0));

        if(!canGo){
            weapon.SetType(0);
            currentDirection = Vector3.zero;
            return;
        }
        
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal") , 0 , Input.GetAxisRaw("Vertical"));

        direction = transform.rotation * direction;

        direction = direction.normalized;

        direction *= speed * Time.deltaTime;

        currentDirection = Vector3.SmoothDamp(currentDirection , direction , ref vDirection , smooth);

        if(characterController.isGrounded){

           // Debug.Log(direction.magnitude);

            if(direction.magnitude > 0.01f){

                weapon.SetType(0.5f);
            }else{

                weapon.SetType(0);
            }

            if(!jumped && Input.GetButton("Jump")){

                jumped = true;

                _yVelocity = Mathf.Sqrt(jump * -2 * gravity) * Time.deltaTime;
            }else{
                _yVelocity = -0.01f;
            }

        }else{

            jumped = false;

            weapon.SetType(1f);

            _yVelocity += gravity * Time.deltaTime * Time.deltaTime;

        }

        currentDirection.y = _yVelocity > maxJump ? maxJump : _yVelocity;

        characterController.Move(currentDirection);

        deathShow.alpha = 1 - health.GetHealth();
    }

    void Hit(){
        
        if(health.GetHealth() <= 0){
            isDead = true;
        }
        

    }
}
