using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Text;
using UnityEngine.Audio;

public class RobotDialog : MonoBehaviour
{
    
    public TextAsset dialogBasic;
    TextAsset dialogMyMan;
    TextAsset dialogHero;

    public UnityEvent onEndDialog;

    public static RobotDialog currentDialog; 

    public float tonne = 0.5f;

    public bool requiredAttention = false;

    public TextMeshProUGUI textObj;

    public string nameRobot = "Rbo-232"; 

    AudioSource audioSource;

    Animator animator;

    RobotEmotion emotion;

    RobotMovement movement;


    bool tempCanShoot;
    bool tempCanGo;

    int statusText = 0;

    int line = 0;

    string[] lines;

    IEnumerator coroutine ;

    private void Start()
    {
        emotion = GetComponent<RobotEmotion>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        movement = GetComponent<RobotMovement>();

        LoadDialogs();
    }

    public void LoadDialogs(){

        line = 0;

        if(dialogBasic != null){

            lines = dialogBasic.text.Split('\n');

        }else{

            lines = new string[1];

            float r = Random.Range(0 , 50);

            if(r < 15){
                lines[0] = "***** ***";
            }else if(r < 35){
                lines[0] = "Rain on concrete, rain on concrete";
            }else{
                lines[0] = "I really like T";
            }

        }
    }

    private void Update()
    {

        if(Input.GetButtonDown("Fire1") && coroutine != null){

            if(statusText == 1){

                statusText = 2;

            }else if(statusText == 2){

                StopCoroutine(coroutine);

                coroutine = null;

                textObj.text = lines[line - 1];

                audioSource.Stop();

                animator.SetBool("talk" , false);

                OnEndText();

            }

        }
        
    }

    public void StopDialog(){

        StopCoroutine(coroutine);

        coroutine = null;

        textObj.text = lines[line - 1];

        audioSource.Stop();

        animator.SetBool("talk" , false);

        line = lines.Length;

         OnEndText();
        
    }

    public void StartDialog(){

        if(line >= lines.Length || currentDialog == this){
            return;
        }

        if(currentDialog != null ){

            if(!(currentDialog.requiredAttention == false && requiredAttention == true)){

                return;

            }else{

                currentDialog.StopDialog();

            }


        }

        if(requiredAttention){

            tempCanShoot = PlayerMovement.instance.canShoot ;
            tempCanGo= PlayerMovement.instance.canGo ;

            movement.Stop();

            PlayerMovement.instance.canShoot = false;
            PlayerMovement.instance.canGo = false;
            transform.LookAt(new Vector3(PlayerMovement.instance.transform.position.x , transform.position.y , PlayerMovement.instance.transform.position.z));

        }

        currentDialog = this;


        ShowNextLine();

    
    }

    public void ShowNextLine(){

        if(currentDialog != this || line >= lines.Length || textObj == null || coroutine != null){
            return;
        }

        if(lines[line].Contains("#alert")){

            Alert.Call(lines[line].Replace("#alert" , "").Trim());

            line++;

            OnEndText();

        }else{

            if(lines[line].Contains("#emotion")){

                //string emotionStr = lines[line].Replace("&alert" , "");

                int ind = lines[line].IndexOf("#emotion") + 8;

                string emotionStr = lines[line].Substring(ind).Trim();

                lines[line] = lines[line].Remove(ind - 8);

                emotion.ChangeEmotion(emotionStr);

            }

            coroutine = showText(lines[line]);

            StartCoroutine(coroutine);

            line++;

        }

    }

    void OnEndText(){

        if(line >= lines.Length){

            movement.Resume();

            currentDialog = null;

            onEndDialog.Invoke();

            animator.SetBool("talk" , false);     
            
            textObj.text = "";

            if(requiredAttention){

                PlayerMovement.instance.canShoot = tempCanShoot;
                PlayerMovement.instance.canGo = tempCanGo;

            }

        }else{

            ShowNextLine();

        }

    }

    IEnumerator showText(string text){

        textObj.text = "";

        StringBuilder builder = new StringBuilder(nameRobot + ": ");

        int textLength = text.Length;

        int i = 0;

        statusText = 1;

        while(true){

            if(i < textLength && statusText == 1){

                animator.SetBool("talk" , true);

                audioSource.Stop();

                audioSource.pitch = Random.Range( tonne - 0.3f , tonne + 0.3f);

                audioSource.Play();

                builder.Append(text[i]);

                textObj.text = builder.ToString();
                i++;

                yield return new WaitForSeconds(0.09f);

            }else{

                textObj.text = nameRobot + ": " + text;

                audioSource.Stop();

                animator.SetBool("talk" , false);

                statusText = 2;

                //if(line  >= lines.Length && requiredAttention){

                   // yield return null;

                //}else{

                    yield return new WaitForSeconds(2f);

               // }
                
                break;

            }
      
        }

        statusText = 0;

        coroutine = null;

        OnEndText();

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            StartDialog();
        }
    }


}
