using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorLight : MonoBehaviour
{
    public Light newLight;

    void Update()
    {
        if (Random.Range(1f,0f) <= 0.99)
        {
            newLight.intensity = 0;
        }
        else
        {
            newLight.intensity = Random.Range(5f, 3f);
        }
    }
}
