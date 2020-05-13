using UnityEngine;
using UnityEngine.UI;

public class RainAndThunderControl : MonoBehaviour
{
    public Image FalseFlash;
    public AudioSource ThunderSound;
    public AudioSource RainSound;
    public GameObject Title;
    public Light ThunderLight;
    public Text Button0, Button1;
    public GameObject FalseFlashObject, Button0GameObject, Button1GameObject;

    static public bool AnotherScriptWantsToTurnOnFalseFlash = false;
    

    public float timeUntilNextThunder;
    public float randomFactor;
    public float timeUntilThunderSound;
    public bool thunderSoundPlayedAlready = false;
    public bool isThisStartThunder;
    Color tempColorFalseFlash;
    Color tempColorPlayButton;
    Color tempColorExitButton;


    void Start()
    {
        timeUntilNextThunder = 2;
        isThisStartThunder = true;
        
        tempColorFalseFlash = FalseFlash.color;
        tempColorFalseFlash.a = 0;
        tempColorPlayButton.a = -3.1f;
        tempColorExitButton.a = -3.5f;

        //set full red to avoid aesthetic troubles
        tempColorPlayButton.r = 1;
        tempColorExitButton.r = 1;

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

        if (AnotherScriptWantsToTurnOnFalseFlash)
        {
            AnotherScriptWantsToTurnOnFalseFlash = false; 
            FalseFlashObject.SetActive(true);
            InvokeFalseFlash();
           
        }

        if (timeUntilNextThunder <= 0)
        {
            
            randomFactor = Random.Range(.5f, 4);

            if (isThisStartThunder)
            {
                isThisStartThunder = false;
                InvokeFalseFlash();
                Title.SetActive(true);
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
        tempColorPlayButton.a += Time.deltaTime;
        tempColorExitButton.a += Time.deltaTime;
        
        //set modified alpha values 
        FalseFlash.color = tempColorFalseFlash;
        Button0.color = tempColorPlayButton;
        Button1.color = tempColorExitButton;

        if (tempColorFalseFlash.a <= 0 && !isThisStartThunder)
        {
            FalseFlashObject.SetActive(false);
        }
    }

    public void InvokeFalseFlash()
    {
        randomFactor = 4;
        tempColorFalseFlash.a = 1;
        FalseFlash.color = tempColorFalseFlash;
        ThunderSound.volume = 1;
        if (GameManager.HasTheGameStarted)
        {
            Title.SetActive(false);
            Button0GameObject.SetActive(false);
            Button1GameObject.SetActive(false);
            
        }
        timeUntilNextThunder = 0;
    }

}
