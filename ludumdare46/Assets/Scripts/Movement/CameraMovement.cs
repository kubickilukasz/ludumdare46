using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 500f;

    [SerializeField]
    float smoothTime = 0.7f;

    Vector3 direction = Vector3.zero;

    Vector3 vDirection;
    Vector3 currentDirection;

      public Vector3 Direction
    {
        get
        {
            return currentDirection;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        direction += new Vector3(-Input.GetAxis("Mouse Y") , Input.GetAxis("Mouse X") , 0 ) *speed * Time.deltaTime;

        direction.x = Mathf.Clamp(direction.x, -80, 70);

        currentDirection = Vector3.SmoothDamp(currentDirection , direction , ref vDirection ,smoothTime);

        transform.localRotation = Quaternion.Euler(new Vector3(currentDirection.x, 0, 0));

        
    }
}
