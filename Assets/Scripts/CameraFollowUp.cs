using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//follow the player as he jumps up and does not go to far down. 
public class CameraFollowUp : MonoBehaviour
{
    public Transform followTransform;
    private float y;
    public GameObject player;
    private bool check;
    private Rigidbody2D rb2d;

    private void Start()
    {
        y = transform.position.y;   //this is the start position of the camera as we follow player down dont go below
        rb2d = player.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(CameraFollowUpLoop());
    }


    private IEnumerator CameraFollowUpLoop()
    {
        while (true)
        {
            if (followTransform.position.y > (transform.position.y + 10))
            {
                check = true;
                transform.position = new Vector3(transform.position.x, followTransform.position.y - 10, transform.position.z);
            }
            if (followTransform.position.y > (y + 10) && rb2d.velocity.y <= 0 && check == true)
            {
                transform.position = new Vector3(transform.position.x, followTransform.position.y - 10, transform.position.z);
                if (followTransform.position.y == (y + 10))
                {
                    check = false;
                    //print("false");
                }

            }


            yield return new WaitForFixedUpdate();
        }


    }
}
