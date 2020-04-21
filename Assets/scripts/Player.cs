using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private Text uiTextForSize;

    public LayerMask Layer;

    public int attachedObjects = 0;

    public static float linearSizeFactor = 0.01f;

    bool isTouchingGround = true;

    public static bool CameraNeedsToSeparateMore = false;
    readonly float movingForce = 50;

    void Update()
    {
        float axisXForce = 0f;
        float axisZForce = 0f;

        if (GameManager.HasTheGameStarted)
        {
            if (isTouchingGround)
            {
                

                //change force adders depending on the pressed key
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    axisXForce += movingForce;
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    axisXForce -= movingForce;
                }
                else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    axisZForce += movingForce;
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    axisZForce -= movingForce;
                }
                //add force changers to player's ball
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(axisXForce, 0, axisZForce));

                //return force adders to 0
                axisXForce = 0f; axisZForce = 0f;
            }
        }

        uiTextForSize.text = gameObject.transform.localScale.x.ToString("F");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }

        //else if the collision object has a parent with the MinimalSize class
        else if (collision.gameObject.GetComponentInParent<MinimalSize>() != null)
        {
            //if the player's ball has reached the minimal size required to get this object attached
            if (transform.localScale.x >= collision.gameObject.GetComponentInParent<MinimalSize>().minimalBallSize)
            {
                attachedObjects++;

                //scale player
                gameObject.transform.localScale = new Vector3(
                    gameObject.transform.localScale.x + linearSizeFactor,
                    gameObject.transform.localScale.y + linearSizeFactor,
                    gameObject.transform.localScale.z + linearSizeFactor);

                CameraNeedsToSeparateMore = true;

                //make object a child of the player
                collision.gameObject.transform.parent = transform;

                //make object kinematic
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision.gameObject.GetComponent<Rigidbody>().useGravity = false;

                //dactivateCollider
                collision.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (isTouchingGround)
        {
            isTouchingGround = false;
        }
    }*/
}
