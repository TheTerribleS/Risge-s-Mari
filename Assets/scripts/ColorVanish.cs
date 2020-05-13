using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorVanish : MonoBehaviour
{
    Color newColor;
    public Text ColorToVanish;
    public float vanishingFactor = 1;

    private void Start()
    {
        newColor.r = 1;
        newColor.g = 0;
        newColor.b = 0;
        newColor.a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ColorToVanish.color = newColor;
        newColor.a -= Time.deltaTime * vanishingFactor;
    }
}
