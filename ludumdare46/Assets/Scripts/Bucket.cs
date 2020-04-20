using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{

    public GameObject toActiveObject;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        toActiveObject.SetActive(false);
        Boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Victory.fullCondition = true;

            Alert.Call("BACK TO ROBOTS");

            if(toActiveObject){
                toActiveObject.SetActive(true);
            }

            if(Boss && RobotMovement.deaths >= 11){
                Boss.SetActive(true);
                Boss.GetComponent<RobotDialog>().StartDialog();
            }

            Destroy(gameObject);
        }
    }
}
