using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject Player;
    public GameObject TimeController;
    public GameObject WelcomeScreen;
    public GameObject GameplayScreen;
    public GameObject WinScreen;
    public GameObject LooseScreen;
    public GameObject WelcomeMusic;
    public GameObject GameplayMusic;
    public GameObject WinMusic;
    public GameObject LooseMusic;
    public GameObject pauseMenuScreen;
    public GameObject pauseMenuMusic;

    //enum of scenes to make management easy
    enum ImportantScreens
    {
        welcome,
        gameplay,
        win,
        loose
    }

    public float CountdownTimer;

    public static bool HasTheGameStarted = false;
    public static float ScaleObjective = 1.5f;
    public static bool HasUserWonOrLost = false, didUserAskToRestart = false;
    public bool isGameRestarting = false;
    public bool isPauseMenuActive = false;
    bool doneAlready = false;

    private void Start()
    {
        StartTheGame();
        CountdownTimer = Countdown.ReturnTimer();
        RainAndThunderControl.AnotherScriptWantsToTurnOnFalseFlash = true;
    }

    // Update is called once per frame
    void Update()
    {
        CountdownTimer = Countdown.ReturnTimer();

        if (HasUserWonOrLost || didUserAskToRestart)
        {
            ScreenSwitch(ImportantScreens.welcome, ImportantScreens.gameplay);
            HasUserWonOrLost = false;
            
        }

        //if the timer is over, the game has started AND is not restarting
        if (CountdownTimer <= 0 && HasTheGameStarted && !isGameRestarting)
        {
            if (!doneAlready)
            {
                RainAndThunderControl.AnotherScriptWantsToTurnOnFalseFlash = true;
                doneAlready = true;
            }
            
            //if player reached the target scale (scaleObjective) 
            if (Player.transform.localScale.x >= ScaleObjective)
            {
                //switch to win scene
                ScreenSwitch(ImportantScreens.gameplay, ImportantScreens.win);
            }
            else
            {
                //switch to loose scene
                ScreenSwitch(ImportantScreens.gameplay, ImportantScreens.loose);
            }
            HasUserWonOrLost = true;

        }
        else
            isGameRestarting = false;
        
        if (Input.GetKeyDown(KeyCode.Escape)&& HasTheGameStarted)
        {
            if (!isPauseMenuActive)
            {
                PauseMenuManager(true);
            }
            
            else
            {
                PauseMenuManager(false);
            }
                
        }
    }

    public void StartTheGame()
    {
        ScreenSwitch(ImportantScreens.welcome, ImportantScreens.gameplay);



        HasTheGameStarted = true;
    }

    public void RestartGame()
    {
        ScreenAndMusicManager(ImportantScreens.loose, false);

        ScreenAndMusicManager(ImportantScreens.win, false);

        if (HasTheGameStarted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ScreenAndMusicManager(ImportantScreens.gameplay, true);
        }
        if (isPauseMenuActive)
        {
            PauseMenuManager(false);

            didUserAskToRestart = true;

        }

        //DEBUG
        CountdownTimer = 30;

        HasTheGameStarted = true;

    }

    public void QuitPauseMenu()
    {
        PauseMenuManager(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchScenes()
    {
        SceneManager.LoadScene("Katamari");
    }

    void ScreenAndMusicManager(ImportantScreens sceneToManage, bool activate)
    {
        switch (sceneToManage)
        {
            case ImportantScreens.welcome:
                WelcomeScreen.SetActive(activate);
                WelcomeMusic.SetActive(activate);
                break;
            case ImportantScreens.gameplay:
                GameplayScreen.SetActive(activate);
                GameplayMusic.SetActive(activate);
                break;
            case ImportantScreens.win:
                WinScreen.SetActive(activate);
                WinMusic.SetActive(activate);
                break;
            case ImportantScreens.loose:
                LooseScreen.SetActive(activate);
                LooseMusic.SetActive(activate);
                break;
            default:
                break;
        }
    }

    ///deactivate and activate two different scenes
    void ScreenSwitch(ImportantScreens sceneToDeactivate, ImportantScreens sceneToActivate)
    {
        switch (sceneToDeactivate)
        {
            case ImportantScreens.welcome:
                ScreenAndMusicManager(ImportantScreens.welcome, false);
                break;
            case ImportantScreens.gameplay:
                ScreenAndMusicManager(ImportantScreens.gameplay, false);
                break;
            case ImportantScreens.win:
                ScreenAndMusicManager(ImportantScreens.win, false);
                break;
            case ImportantScreens.loose:
                ScreenAndMusicManager(ImportantScreens.loose, false);
                break;
            default:
                break;
        }
        switch (sceneToActivate)
        {
            case ImportantScreens.welcome:
                ScreenAndMusicManager(ImportantScreens.welcome, true);
                break;
            case ImportantScreens.gameplay:
                ScreenAndMusicManager(ImportantScreens.gameplay, true);
                break;
            case ImportantScreens.win:
                ScreenAndMusicManager(ImportantScreens.win, true);
                break;
            case ImportantScreens.loose:
                ScreenAndMusicManager(ImportantScreens.loose, true);
                break;
            default:
                break;
        }
    }

    void PauseMenuManager(bool ActivatePause)
    {
        if (ActivatePause)
        {
            isPauseMenuActive = true;
            pauseMenuScreen.SetActive(true);
            Time.timeScale = 0;
            GameplayMusic.GetComponent<AudioSource>().Pause();
        }
        else
        {
            isPauseMenuActive = false;
            pauseMenuScreen.SetActive(false);
            Time.timeScale = 1;
            GameplayMusic.GetComponent<AudioSource>().Play();
        }
    }

    
   
}
