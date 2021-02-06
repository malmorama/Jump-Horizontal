using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowUp : MonoBehaviour
{
    public Transform followTransform;
    private float y;
    public GameObject player;
    private bool check;

    private void Start()
    {
        y = transform.position.y;   //this is the start position of the camera as we follow player down dont go below
    }

    // Update is called once per frame
    void Update()
    {
        if (followTransform.position.y > (transform.position.y + 10))
        {
            check = true;
            transform.position = new Vector3(transform.position.x, followTransform.position.y - 10, transform.position.z);
        }
        if (followTransform.position.y > (y + 10) && player.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 && check == true)
           {
               transform.position = new Vector3(transform.position.x, followTransform.position.y - 10, transform.position.z);
               if (followTransform.position.y == (y + 10))
               {
                   check = false;
                print("false");
               }

           }
    }
}
