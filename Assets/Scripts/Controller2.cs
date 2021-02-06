using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller2 : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float moveInput;
    private float speed = 15f;
    //private float topScore = 0f;
    //public Text ScoreText;
    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    void Update()
    {
        //move the player sprite right or left depending on key inputs
        if (moveInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        //count score
       // if (rb2d.velocity.x > 0 && transform.position.x > topScore)
       // {
       //     topScore = transform.position.x;
      //  }
      //  ScoreText.text = "Score: " + Mathf.Round(topScore).ToString();


        //reload scence
        if (gameObject.transform.position.y < -30)
        {
            SceneManager.LoadScene("Gameplay");
        }

    }

    //move the player on key inputs
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
       
    }
}
