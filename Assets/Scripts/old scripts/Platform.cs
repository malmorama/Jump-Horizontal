using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bounce : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    { 
    }



        //If collision with platform the player jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.name.StartsWith("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 725f);
        }
    }
    
}
