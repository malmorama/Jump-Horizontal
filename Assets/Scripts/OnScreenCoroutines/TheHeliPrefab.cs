using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the script that lets the player fly with the heliprefab. It is on the on screencoroutine gameobject. communicated to from event
//on playerlogic sript
public class TheHeliPrefab : MonoBehaviour
{
    public GameObject heliAnimation;
    AudioManager audioManager;
    private Rigidbody2D rb2d;
    public GameVariables gameVariables;
    private float oldPlatformSpeed;
    private float oldBackgroundSpeed;


    private void Awake()
    {

        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();
        //heliAnimation = GameObject.Find("HeliAnimation");
        rb2d = GameObject.Find("Player_fox_right").GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void StartCourtineTheHelicopter()
    {
        StartCoroutine(TheHelicopter());
    }

    private IEnumerator TheHelicopter()
    {
        oldPlatformSpeed = gameVariables.platformScrollSpeed;
        oldBackgroundSpeed = gameVariables.backgroundScrollSpeed;

        Vector2 respawnPlayerPosition = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y + 20);
        //Vector2 respawnPlayerPosition = new Vector2(-10, 25);
        //transform.position = respawnPlayerPosition;
        float moveSpeed = 22f;

        //start the helianimation
        heliAnimation.SetActive(true);
        audioManager.Play("HelicopterSound");


        while (rb2d.transform.position.y < respawnPlayerPosition.y)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveSpeed);
            //transform.position = Vector2.MoveTowards(transform.position, respawnPlayerPosition, moveSpeed );
            yield return null;
        }

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

        gameVariables.platformScrollSpeed = gameVariables.platformScrollSpeed * 2f;
        //gameVariables.backgroundScrollSpeed = gameVariables.backgroundScrollSpeed * 2;

        rb2d.isKinematic = true;
        GameObject.Find("Player_fox_right").GetComponent<BoxCollider2D>().enabled = false;

        //wait until added 100 points score beforing killing the helicopter
        float addScore = gameVariables.score + 50f;
        while(gameVariables.score < addScore)
        {
            gameVariables.score++;
            yield return new WaitForSeconds(0.065f);
        }


        //yield return new WaitForSeconds(5);

        gameVariables.platformScrollSpeed = oldPlatformSpeed;
        //gameVariables.backgroundScrollSpeed = oldBackgroundSpeed;

        rb2d.isKinematic = false;
        GameObject.Find("Player_fox_right").GetComponent<BoxCollider2D>().enabled = true;
        heliAnimation.SetActive(false);
        audioManager.Stop("HelicopterSound");

    }


    }
