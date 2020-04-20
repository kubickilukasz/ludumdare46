using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEmotion : MonoBehaviour
{

    public enum Emotion{
        Normal = 0, 
        Smile = 1, 
        Angry = 2, 
        Death = 3,
        What = 4,
        Sad = 5,
        Mood = 6
    }

    public Renderer m_renderer;

    public Texture [] textures;

    public Emotion normalEmotion;

    Emotion emotion;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponentInChildren<Renderer>();

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeEmotion(Emotion newEmotion){

        emotion = newEmotion;

       //  m_renderer.material.SetTexture("emotions" , textures[(int)Emotion.Normal]);

       //string nameEmo = "Textureemotions";
       //string nameEmo = "Textureemotions";
     /*  string [] nameEmo = m_renderer.material.GetTexturePropertyNames();

       for(int i =0 ; i < nameEmo.Length; i++){
           Debug.Log(nameEmo[i]);
       }*/

       Texture loadedTexture = null;

        switch(newEmotion){

            case Emotion.Normal:

                loadedTexture = Resources.Load("normal") as Texture;

            break; 
            case Emotion.Smile:

              loadedTexture =Resources.Load("smile") as Texture;

            break; 
            case Emotion.Angry:

            loadedTexture = Resources.Load("angry") as Texture;

            break; 
            case Emotion.Death:

             loadedTexture = Resources.Load("death") as Texture;

            break; 
            case Emotion.What:

             loadedTexture = Resources.Load("what") as Texture;

            break; 
            case Emotion.Sad:

             loadedTexture = Resources.Load("sad") as Texture;

            break; 
            case Emotion.Mood:

             loadedTexture = Resources.Load("mood") as Texture;

            break; 

        }

        if(loadedTexture != null){
            m_renderer.material.SetTexture( "Texture2D_5F8D853A" , loadedTexture);    
        }else{
            Debug.Log("Nie ma takiej tekstury ");
        }

         

    }

    public void ChangeEmotion(string newEmotion){

        switch(newEmotion){
            case "angry":
            ChangeEmotion(RobotEmotion.Emotion.Angry);
            break;
            case "smile":
            ChangeEmotion(RobotEmotion.Emotion.Smile);
            break;
            case "normal":
            ChangeEmotion(RobotEmotion.Emotion.Normal);
            break;
            case "death":
            ChangeEmotion(RobotEmotion.Emotion.Death);
            break;
            case "what":
            ChangeEmotion(RobotEmotion.Emotion.What);
            break;
            case "sad":
            ChangeEmotion(RobotEmotion.Emotion.Sad);
            break;
            case "mood":
            ChangeEmotion(RobotEmotion.Emotion.Mood);
            break;
            case "reset":
            Reset();
            break;
        }

    }

    public void Reset(){
        
        ChangeEmotion(normalEmotion);


    }
}
