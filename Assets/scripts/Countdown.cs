using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    public GameObject objectWithTimer;

    public static float StartTimer = 180f;
    

    float timer = StartTimer;
    bool canCount = true;
    bool doOnce = false;
    public static float staticTimer;
    

    private void Start()
    {
        mainTimer = StartTimer;
        
        timer = mainTimer;

        staticTimer = StartTimer;
    }


    private void Update()
    {
        if (!GameManager.HasTheGameStarted)
        {
            timer = mainTimer;
        }
        

        
        if (timer >= 0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
            staticTimer = timer;
        }
        else if (timer <= 0f && !doOnce)
        {
            Debug.Log("I am entering this on the wake");
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0f;
        }
    }

    public void ResetButton()
    {
        timer = mainTimer;
        canCount = true;
        doOnce = false;
        
    }

    public static float ReturnTimer()
    {
        return staticTimer;
        
    }
}

