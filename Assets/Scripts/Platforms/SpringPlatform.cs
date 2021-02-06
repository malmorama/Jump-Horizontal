using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If collision with platform the player is sent to below screen. In player logic there is a script that if player below screen and no lifes
    //game over otherwise player flies up
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 && collision.gameObject.name.StartsWith("Player"))
        {
            collision.gameObject.transform.position = new Vector2(0, -51);

        }
    }

}
