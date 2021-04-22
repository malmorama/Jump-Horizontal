using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


//control what happens when player lands on different platforms or collectables
[System.Serializable] public class _UnityEventGameObject : UnityEvent<GameObject> { }

public class PlayerLogic : MonoBehaviour
{
    //public float jumpForce;
    private Rigidbody2D rb2d;
    public GameObject menu;
    FadeScreen fadeScreen;
    AudioManager audioManager;
    public ParticleSystem dust;
    public GameVariables gameVariables;
    private bool collideWithPlatform = false;
    public _UnityEventGameObject rightSideUpEvent;
    public _UnityEventGameObject rightSideDownEvent;
    public GameObject heliAnimation;
    public UnityEvent teleportEvent;
    public UnityEvent respawnWithHelicopterEvent;
    public UnityEvent heliPrefabEvent;
    private List<string> stdPlatformAction = new List<string>();
    //private List<string> eventPlatforms = new List<string>();

    //float tempScore;

    private void Awake()
    {
        fadeScreen = GameObject.Find("FadeblackImage").GetComponent<FadeScreen>();
        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();

        gameVariables.sendTelePortOnlyOnce = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        menu.SetActive(false);
        fadeScreen.SetOpacity(1);
        StartCoroutine(fadeScreen.FadeBlackToScreen());
        
        StartCoroutine(mainPlatformCollisionAction());
        StartCoroutine(PlayerFallOffScreen());
        StartCoroutine(UpdateScoreScroll());
        

        //if hit these perform std action = jump, smoke, sound
        stdPlatformAction.Add("Platform");
        stdPlatformAction.Add("FlagPlatform");
        stdPlatformAction.Add("BrownPlatform");
        stdPlatformAction.Add("SendCoinsEventPlatform");
        stdPlatformAction.Add("SendMissileEventPlatform");


        //if hit these activity on screen happens
        //eventPlatforms.Add("CoinScrollFromRight");

        //make sure life is always 1 if less than 1 when start the game.   
        if (gameVariables.life < 1)
        {
            gameVariables.life = 1;
        }




    }


    //if collide with a coin increase score with 100
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Gold"))
        {
            //score += 50f;
            gameVariables.coin += 1;
            //scoreText.text = Mathf.Round(gameVariables.score).ToString();
            collision.gameObject.SetActive(false);
            audioManager.Play("CoinCollectSound");
            
        }
        //if collide with pink diamond 500 points score
        if (collision.gameObject.name.StartsWith("Pink"))
        {
            //score += 500f;
            gameVariables.coin += 25;
            //scoreText.text = Mathf.Round(gameVariables.score).ToString();
            collision.gameObject.SetActive(false);
            audioManager.Play("HeartCollectSound");
        }

        //if collide with heart 1 lift point, max lift point is 2 
        if (collision.gameObject.name.StartsWith("Heart") && gameVariables.life == 1)
        {
            
            gameVariables.life += 1;
            //lifeText.text = Mathf.Round(gameVariables.life).ToString();
            collision.gameObject.SetActive(false);
            audioManager.Play("HeartCollectSound");
        }

        //If player hit rightsideupsign
        if (collision.gameObject.name.StartsWith("RightSignUp"))
        {
            rightSideUpEvent.Invoke(gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.name.StartsWith("RightSignDown"))
        {
            rightSideDownEvent.Invoke(gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.name.StartsWith("Flag"))
        {
            gameVariables.playerOnLevel++;
            gameVariables.difficulty = 0;   //reset difficulty for next level
            gameVariables.CurrentScoreToDifficulity = 0;
            StartCoroutine(FadeBlackLoadAScene("BetweenScenesStats"));
        }

        if (collision.gameObject.name.StartsWith("HeliPrefab"))
        {
            heliPrefabEvent.Invoke();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.name.StartsWith("Missile"))
        {
            gameObject.transform.position = new Vector2(0, -51);
        }

    }


    

    private void OnCollisionEnter2D(Collision2D collision)
    {


        //if hits a red platform player death, game restarts
        if (collision.collider.gameObject.name.StartsWith("Spring") || collision.collider.gameObject.name.StartsWith("UpDownKill")
            && rb2d.velocity.y <= 0)
        {
            //menu.SetActive(true);
            //gameObject.SetActive(false);
            //send player below screen because trigger player outside screen
            gameObject.transform.position = new Vector2(0, -51);

        }


        //perform the same actions if land on the platforms in the list
        for (int i = 0; i < stdPlatformAction.Count; i++)
        {
            string tempName = stdPlatformAction[i];

            //If player hit main platform jump 
            if (collision.collider.gameObject.name.StartsWith(tempName) && rb2d.velocity.y <= 0)
            {
                //at first collision set bool to true to can not jump twice. Then in a coroutne change it back with delay
                if (collideWithPlatform == false)
                {
                    collideWithPlatform = true;
                    rb2d.AddForce(Vector2.up * gameVariables.jumpForce);
                    //TrailDust
                    TrailDust(collision);

                    audioManager.Play("PlayerJump");

                }
            }
        }

        //this runs a coroutine on the gameobject CoroutineOnScreens. Like this since the GameObj always is active, otherwise
        //coroutine stops
        if (collision.collider.gameObject.name.StartsWith("TeleportPlatform") && rb2d.velocity.y <= 0)
        {
            //at first collision set bool to true to can not jump twice. Then in a coroutne change it back with delay
            if (collideWithPlatform == false)
            {
                collideWithPlatform = true;
                rb2d.AddForce(Vector2.up * gameVariables.jumpForce);
                //dust.Play();
                audioManager.Play("PlayerJump");
            }
            teleportEvent.Invoke();
            //StartCoroutine(TelePortPlatformCoroutine());
        }

        //if the collision collider has an interface IEventPlatforms on it, run the EventPlatformActon method
        IEventPlatforms eventPlatforms = collision.collider.gameObject.GetComponent<IEventPlatforms>();
        if (eventPlatforms != null)
        {
            eventPlatforms.EventPlatformAction();
        }
    }

    //find the contact point on the impact oncollision and spawn the dust here
    private void TrailDust(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int numContacts = collision.GetContacts(contacts);
        
        Vector2 pos = new Vector2(contacts[0].point.x - 2, contacts[0].point.y);
        GameObject cloudObj = ObjectPooler.Instance.SpawnFromPool("DustCloud", pos, Quaternion.identity);
        ParticleSystem cloud = cloudObj.GetComponent<ParticleSystem>();
        //ParticleSystem cloud = Instantiate(dust, pos, collision.gameObject.transform.rotation);
        cloud.Play();
    }


    
    //coroutine endless loop started in the start function to delay the bool update
    private IEnumerator mainPlatformCollisionAction()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            collideWithPlatform = false;
        }
    }


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

            //scoreText.text = Mathf.Round(gameVariables.score).ToString();
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
                
                respawnWithHelicopterEvent.Invoke();
                

            }
                
            if (gameVariables.life == 1 && gameObject.transform.position.y < -50)
            {
                audioManager.Play("PlayerDeathSound");
                gameVariables.life--;
                StartCoroutine(fadeScreen.FadeToBlackScreen());
                SceneManager.LoadScene("BetweenScenesStats");
            }
            
            yield return new WaitForSeconds(0.5f);
        }


    }


    private IEnumerator FadeBlackLoadAScene(string SceneToLoad)
    {
        StartCoroutine(fadeScreen.FadeToBlackScreen());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneToLoad);
    }


}
