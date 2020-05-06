using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class RainAndThunderControl : MonoBehaviour
{
    public Image FalseFlash;
    public AudioSource ThunderSound;
    public AudioSource RainSound;
    public GameObject Title;
    public Light ThunderLight;
    public Text Button0, Button1;
    public GameObject FalseFlashObject;

    public float timeUntilNextThunder;
    public float randomFactor;
    public float timeUntilThunderSound;
    public bool thunderSoundPlayedAlready = false;
    public bool isThisStartThunder;
    Color tempColorFalseFlash;
    Color tempColorButton0;
    Color tempColorButton1;


    void Start()
    {
        timeUntilNextThunder = 2;
        isThisStartThunder = true;
        
        tempColorFalseFlash = FalseFlash.color;
        tempColorFalseFlash.a = 0;
        tempColorButton0.a = -3.1f;
        tempColorButton1.a = -3.5f;

        //set full red to avoid aesthetic troubles
        tempColorButton0.r = 1;
        tempColorButton1.r = 1;

        FalseFlash.color = tempColorFalseFlash;
        RainSound.volume = 0;
        Title.SetActive(false);
        ThunderSound.volume = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (RainSound.volume < 1)
        {
            RainSound.volume += Time.deltaTime / 2;
        }


        if (timeUntilNextThunder <= 0)
        {
            
            randomFactor = Random.Range(.5f, 4);

            if (isThisStartThunder)
            {
                randomFactor = 4;
                isThisStartThunder = false;
                tempColorFalseFlash.a = 1;
                FalseFlash.color = tempColorFalseFlash;
                Title.SetActive(true);
                ThunderSound.volume = 1;
                
            }
            
            thunderSoundPlayedAlready = false;
            
            ThunderLight.intensity = randomFactor / 2;
            timeUntilThunderSound = 2 - (randomFactor / 2);
            ThunderSound.volume = randomFactor / 4;

            timeUntilNextThunder += Random.Range(4, 8.5f);
        }
        else
        {
            ThunderLight.intensity -= Time.deltaTime;
        }

        if (timeUntilThunderSound <= 0 && !thunderSoundPlayedAlready)
        {
            ThunderSound.GetComponent<AudioSource>().Play();
            thunderSoundPlayedAlready = true;
        }

        timeUntilNextThunder -= Time.deltaTime;
        timeUntilThunderSound -= Time.deltaTime;
        tempColorFalseFlash.a -= Time.deltaTime / 2;
        tempColorButton0.a += Time.deltaTime;
        tempColorButton1.a += Time.deltaTime;
        
        //set modified alpha values 
        FalseFlash.color = tempColorFalseFlash;
        Button0.color = tempColorButton0;
        Button1.color = tempColorButton1;

        if (tempColorFalseFlash.a <= 0 && !isThisStartThunder)
        {
            FalseFlashObject.SetActive(false);
        }
    }
}
