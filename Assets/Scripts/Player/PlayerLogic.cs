using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[System.Serializable] public class _UnityEventGameObject : UnityEvent<GameObject> { }

public class PlayerLogic : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D rb2d;
    public Text lifeText;
    public Text scoreText;
    public GameObject menu;
    //private float score = 0f;
    FadeScreen fadeScreen;
    public AudioSource coinCollectSound;
    public AudioSource playerJumpSound;
    public AudioSource teleportSound;
    public ParticleSystem dust;
    public GameVariables gameVariables;
    private bool collideWithPlatform = false;
    private float oldPlatformSpeed;
    private float oldBackgroundSpeed;
    public _UnityEventGameObject rightSideUpEvent;
     
    

    //float tempScore;

    private void Awake()
    {
        fadeScreen = GameObject.Find("FadeblackImage").GetComponent<FadeScreen>();
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        menu.SetActive(false);
        fadeScreen.SetOpacity(1);
        StartCoroutine(fadeScreen.FadeBlackToScreen());
        gameVariables.score = 0f;
        StartCoroutine(mainPlatformCollisionAction());
        StartCoroutine(PlayerFallOffScreen());
        StartCoroutine(UpdateScoreScroll());
        //tempScore = 0;
        gameVariables.life = 2;
        lifeText.text = gameVariables.life.ToString();
        gameVariables.platformScrollSpeed = 18;
        gameVariables.backgroundScrollSpeed = 0.08f;
        gameVariables.sendTelePortOnlyOnce = false;
        oldPlatformSpeed = gameVariables.platformScrollSpeed;
        oldBackgroundSpeed = gameVariables.backgroundScrollSpeed;


    }


    //if collide with a coin increase score with 100
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Gold"))
        {
            //score += 50f;
            gameVariables.score += 50f;
            scoreText.text = Mathf.Round(gameVariables.score).ToString();
            collision.gameObject.SetActive(false);
            coinCollectSound.Play();
        }
        //if collide with pink diamond 500 points score
        if (collision.gameObject.name.StartsWith("Pink"))
        {
            //score += 500f;
            gameVariables.score += 500f;
            scoreText.text = Mathf.Round(gameVariables.score).ToString();
            collision.gameObject.SetActive(false);
            coinCollectSound.Play();
        }

        //if collide with heart 1 lift point, max lift point is 2 
        if (collision.gameObject.name.StartsWith("Heart") && gameVariables.life == 1)
        {
            
            gameVariables.life += 1;
            lifeText.text = Mathf.Round(gameVariables.life).ToString();
            collision.gameObject.SetActive(false);
            //coinCollectSound.Play();
        }

        //If player hit rightsideupsign
        if (collision.gameObject.name.StartsWith("RightSignUp"))
        {
            rightSideUpEvent.Invoke(gameObject);
            collision.gameObject.SetActive(false);
        }

    }

    


    private void OnCollisionEnter2D(Collision2D collision)
    {


        //if hits a red platform player death, game restarts
        if (collision.collider.gameObject.name.StartsWith("Spring") && rb2d.velocity.y <= 0)
        {
            //menu.SetActive(true);
            //gameObject.SetActive(false);
            //send player below screen because trigger player outside screen
            gameObject.transform.position = new Vector2(0, -51);

        }

        //If player hit main platform jump 
        if (collision.collider.gameObject.name.StartsWith("Platform") && rb2d.velocity.y <= 0)
        {
            //at first collision set bool to true to can not jump twice. Then in a coroutne change it back with delay
            if (collideWithPlatform == false)
            {
                collideWithPlatform = true;
                rb2d.AddForce(Vector2.up * jumpForce);
                dust.Play();
                playerJumpSound.Play();
            }
        }

        if (collision.collider.gameObject.name.StartsWith("BrownPlatform") && rb2d.velocity.y <= 0)
        {
            //at first collision set bool to true to can not jump twice. Then in a coroutne change it back with delay
            if (collideWithPlatform == false)
            {
                collideWithPlatform = true;
                rb2d.AddForce(Vector2.up * jumpForce);
                dust.Play();
                playerJumpSound.Play();
            }
        }


        if (collision.collider.gameObject.name.StartsWith("TeleportPlatform") && rb2d.velocity.y <= 0)
        {
            teleportSound.Play();
            StartCoroutine(TelePortPlatformCoroutine());
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

    /*
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        //If player hit telport platform
        if (collision.collider.gameObject.name.StartsWith("Teleport") && rb2d.velocity.y <= 0)
        {
            //rb2d.AddForce(Vector2.up * 0);
            //dust.Play();
            //collision.gameObject.SetActive(false);
            rb2d.gravityScale = 0;
            gameObject.transform.position = new Vector2(-10, -45);
            while (gameVariables.platformScrollSpeed <= 40)
            {
                gameVariables.backgroundScrollSpeed += 0.0001f;
                gameVariables.platformScrollSpeed += 0.7f;
                yield return new WaitForSeconds(0.05f);
            }

            gameVariables.backgroundScrollSpeed += 0.01f;

            yield return new WaitForSeconds(2);
            while (gameVariables.platformScrollSpeed >= oldPlatformSpeed)
            {
                gameVariables.platformScrollSpeed -= 0.7f;
                //gameVariables.backgroundScrollSpeed -= 0.0001f;
                yield return new WaitForSeconds(0.05f);
            }

            gameVariables.platformScrollSpeed = 18f;
            //gameVariables.backgroundScrollSpeed = 0.08f;
            gameObject.transform.position = new Vector2(-10, 7);
            rb2d.gravityScale = 5;

        }

        yield return null;
    }*/



    //update score as scroll to the right
    private IEnumerator UpdateScoreScroll()
    {
        while (true)
        {
            if (rb2d.velocity.x > 0)
            {
                //score += transform.position.x * 0.05f;
                gameVariables.score += 0.5f;
                //tempScore = gameVariables.score;
            }

            if (gameVariables.score <= 0)
            {
                gameVariables.score = 0;
            }

            scoreText.text = Mathf.Round(gameVariables.score).ToString();
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    //send player flying back up if have life for it. Otherwise, launch menu
    private IEnumerator PlayerFallOffScreen()
    {
        while(true)
        {
            //reload scence if player below screen
            if (gameObject.transform.position.y < -50 && gameVariables.life >= 2)
            {

                //respawn player above a position of an active platform, reduce life with 1, move player to position in screen
                GameObject tempPlatform = GameObject.FindWithTag("Platform");
                Vector2 respawnPlayerPosition = new Vector2(tempPlatform.transform.position.x, tempPlatform.transform.position.y + 7);
                //Vector2 respawnPlayerPosition = new Vector2(-10, 25);
                //transform.position = respawnPlayerPosition;
                float moveSpeed = 35f;
                 
                while (transform.position.y < respawnPlayerPosition.y)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, moveSpeed);
                    //transform.position = Vector2.MoveTowards(transform.position, respawnPlayerPosition, moveSpeed );
                    yield return null;
                }

                gameVariables.life--;
                lifeText.text = gameVariables.life.ToString();

                //yield return new WaitForSeconds(3);

            }
                
            if (gameVariables.life == 1 && gameObject.transform.position.y < -50)
            {
                //StartCoroutine(fadeScreen.FadeToBlackScreen());
                menu.SetActive(true);
                //SceneManager.LoadScene("Gameplay");
            }
            
            yield return new WaitForSeconds(0.5f);
        }


    }


    private IEnumerator TelePortPlatformCoroutine()
    {
        gameVariables.sendTelePortOnlyOnce = true;
        float telePortScore = 5f;
        float telePortPlatformSpeed = 50;
        //float telePortIncreaseBackgroundSpeed = 0.0001f;
        float telePortIncreasePlatformSpeed = 0.75f;

        //hide the player and set gravity to zero

        //jumpForce = 0;
        rb2d.gravityScale = 0;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -45);
        
        
        //increase speed then wait 2,5
        while (gameVariables.platformScrollSpeed <= telePortPlatformSpeed)
        {
            //gameVariables.backgroundScrollSpeed += telePortIncreaseBackgroundSpeed;
            gameVariables.platformScrollSpeed += telePortIncreasePlatformSpeed;
            gameVariables.score += telePortScore;
            scoreText.text = Mathf.Round(gameVariables.score).ToString();
            yield return new WaitForSeconds(0.010f);
        }

         
        //gameVariables.score += telePortScore;
        //yield return new WaitForSeconds(1f);

        
        //reduce speed
        while (gameVariables.platformScrollSpeed >= oldPlatformSpeed)
        {
            gameVariables.platformScrollSpeed -= telePortIncreasePlatformSpeed;
            //gameVariables.backgroundScrollSpeed -= telePortIncreaseBackgroundSpeed;
            gameVariables.score += telePortScore;
            scoreText.text = Mathf.Round(gameVariables.score).ToString();
            yield return new WaitForSeconds(0.010f);
        }
        
        //set back speed
        gameVariables.platformScrollSpeed = oldPlatformSpeed;
        gameVariables.backgroundScrollSpeed = oldBackgroundSpeed;

        //yield return new WaitForSeconds(1.5f);

        //send in platform with teleport on top, the wait few seconds, then deactivate Teleport, send in player

        Vector2 position = new Vector2(30, 2); //platform position
        Vector2 position2 = new Vector2(30, 5); // portal position
        GameObject tempPlat = ObjectPooler.Instance.SpawnFromPool("Platform", position, Quaternion.identity);
        GameObject tempPlat2 = ObjectPooler.Instance.SpawnFromPool("Teleportstation", position2, Quaternion.identity);
        teleportSound.Play();

        yield return new WaitForSeconds(1.5f);
        tempPlat2.SetActive(false);
        gameObject.transform.position = tempPlat.transform.position;
        //yield return new WaitForSeconds(0.5f);
        rb2d.gravityScale = 5;
        jumpForce = 800;
        rb2d.AddForce(Vector2.up * jumpForce);

        gameVariables.sendTelePortOnlyOnce = false;

        yield return null;


    }
}
