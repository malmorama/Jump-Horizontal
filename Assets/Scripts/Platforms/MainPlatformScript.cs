using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatformScript : MonoBehaviour
{
    
    public AudioSource playerJumpSound;
    private bool collideWithPlatform = false;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mainPlatformCollisionAction());

    }

    // Update is called once per frame
    void Update()
    {     
    }

    //If collision with platform the player jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 && collision.gameObject.name.StartsWith("Player"))
        {
            //at first collision set bool to true to can not jump twice. Then in a coroutne change it back with delay
            if (collideWithPlatform == false)
            {
                collideWithPlatform = true;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800f);
                playerJumpSound.Play();
            }

        }
    }

    //coroutine endless loop started in the start function to delay the bool update
    private IEnumerator mainPlatformCollisionAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            collideWithPlatform = false;
        }
    }
}



