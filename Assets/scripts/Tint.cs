using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tint : MonoBehaviour
{

    MeshRenderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        mRenderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        mRenderer.material.color = Color.yellow;
       
    }

    private void OnCollisionExit(Collision collision)
    {
        mRenderer.material.color = Color.red;
    }
}
