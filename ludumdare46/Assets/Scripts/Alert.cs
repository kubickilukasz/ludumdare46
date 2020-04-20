using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Alert : MonoBehaviour
{

    public static Alert instance;

    [SerializeField]
    TextMeshProUGUI textObj;

    [HideInInspector]
    public CanvasGroup gr;

    [SerializeField]
    float fadeTime = 2f; 
    float vTime = 0f;

    [HideInInspector]
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gr = textObj.GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        vTime += Time.deltaTime;
        if(vTime > fadeTime){

            gr.alpha -= Time.deltaTime; 

        }
    }

    public static void Call(string alert){

        Alert.instance.textObj.text = alert;

        Alert.instance.audioSource.Play();

        Alert.instance.gr.alpha = 1;

        Alert.instance.vTime = 0f;

    }

}
