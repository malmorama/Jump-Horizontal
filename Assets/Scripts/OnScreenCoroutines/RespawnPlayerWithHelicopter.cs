using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when player dies and have lift left this is activites via an event on the player logic script
public class RespawnPlayerWithHelicopter : MonoBehaviour
{
    public GameObject heliAnimation;
    AudioManager audioManager;
    public GameObject player;
    private Rigidbody2D rb2d;
    public GameVariables gameVariables;
    private float oldPlatformSpeed;
    private float oldBackgroundSpeed;

    private void Awake()
    {
        
        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GameObject.Find("Player_fox_right").GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCourtineRespawnPlayerWithHelicopter()
    {
        StartCoroutine(RespawnPlayerWithHelicopterCourtine());
    }

    private IEnumerator RespawnPlayerWithHelicopterCourtine()
    {
        //respawn player above a position of an active platform, reduce life with 1, move player to position in screen
        GameObject tempPlatform = GameObject.FindWithTag("Platform");
        Vector2 respawnPlayerPosition = new Vector2(tempPlatform.transform.position.x, tempPlatform.transform.position.y + 7);
        //Vector2 respawnPlayerPosition = new Vector2(-10, 25);
        //transform.position = respawnPlayerPosition;
        float moveSpeed = 35f;

        //start the helianimation
        heliAnimation.SetActive(true);
        audioManager.Play("HelicopterSound");


        while (player.transform.position.y < respawnPlayerPosition.y)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveSpeed);
            //transform.position = Vector2.MoveTowards(transform.position, respawnPlayerPosition, moveSpeed );
            yield return null;
        }

        //wait 2 seconds beforing killing the helicopter
        yield return new WaitForSeconds(1.15f);
        heliAnimation.SetActive(false);
        audioManager.Stop("HelicopterSound");
        gameVariables.life = 1;
    }
}
