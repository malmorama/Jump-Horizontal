using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//controll the movements of the player with the input on iphone or laptop
public class Controller : MonoBehaviour
{
     
    private Rigidbody2D rb2d;
    private float moveInput;
    private float speed = 15f;
    private float movePlayerOnPhone = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
      //  FadeScreen = GameObject.Find("FadeblackImage").GetComponent<FadeScreen>();
    }

    // Update is called once per frame
    void Update()
    {

        //move the player sprite right or left depending on key inputs
        // if (moveInput < 0)
        // {
        //     this.GetComponent<SpriteRenderer>().flipX = true;
        // } else
        // {
        //     this.GetComponent<SpriteRenderer>().flipX = false;
        // }

    }

    //move the player on key inputs
    void FixedUpdate()
    {
        MovePlayerKeyboard();
        MovePlayerOnTouch();

    }

   // private class PlayerControll : Controller
   // {
        //Move player with keyboard
        private void MovePlayerKeyboard()
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
        }

        //Move player with touch screen
        private void MovePlayerOnTouch()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    movePlayerOnPhone = -1f;
                    rb2d.velocity = new Vector2(movePlayerOnPhone * speed, rb2d.velocity.y);
                    //Debug.Log("Left click");
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    movePlayerOnPhone = 1f;
                    rb2d.velocity = new Vector2(movePlayerOnPhone * speed, rb2d.velocity.y);
                    //Debug.Log("Right click");
                }
            }
        }

  //  }

   


}


