using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float speed = 50f;

    [SerializeField]
    GameObject particle;

    public AudioSource audioSource;

    void Start()
    {
        Destroy(gameObject,5);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate( Vector3.forward * speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider collider){

        Health hl = collider.GetComponent<Health>();

        if(hl != null ){
            hl.Hit(5);
        }

        var obje = Instantiate(particle , transform.position , Quaternion.identity);

        //audioSource.PlayOneShot(audioSource.clip );

        Destroy(obje , 2f);

        Destroy(gameObject);

    }
}
