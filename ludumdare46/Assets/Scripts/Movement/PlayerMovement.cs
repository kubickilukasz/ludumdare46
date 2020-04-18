using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    CharacterController characterController;

    Vector3 currentDirection;
    Vector3 vDirection;

    float _yVelocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        currentDirection.y = 0;

        transform.rotation = Quaternion.Euler(new Vector3(0 , myCamera.Direction.y , 0));
        
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal") , 0 , Input.GetAxisRaw("Vertical"));

        direction = myCamera.transform.rotation * direction;

        direction = direction.normalized;

        direction *= speed * Time.deltaTime;

        currentDirection = Vector3.SmoothDamp(currentDirection , direction , ref vDirection , smooth);

        if(characterController.isGrounded){

            if(Input.GetButtonDown("Jump")){
                _yVelocity = Mathf.Sqrt(jump * -2.0f * gravity) * Time.deltaTime;
            }else{
                _yVelocity = -0.01f;
            }

        }else{
            _yVelocity += gravity * Time.deltaTime * Time.deltaTime;
        }

        currentDirection.y = _yVelocity;

        characterController.Move(currentDirection);


    }
}
