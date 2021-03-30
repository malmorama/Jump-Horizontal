using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable] public class _UnityEventString : UnityEvent<string> { }

public class TeleportCoroutine : MonoBehaviour
{
    public GameVariables gameVariables;
    public GameObject player;
    private Rigidbody2D rb2d;
    //public _UnityEventString audioManager;
    private float oldPlatformSpeed;
    private float oldBackgroundSpeed;
    //public float jumpForce;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GameObject.Find("Player_fox_right").GetComponent<Rigidbody2D>();
        oldPlatformSpeed = gameVariables.platformScrollSpeed;
        oldBackgroundSpeed = gameVariables.backgroundScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTelePortCoroutine()
    {
        StartCoroutine(TelePortPlatformCoroutine());
    }

    private IEnumerator TelePortPlatformCoroutine()
    {
        audioManager.Play("TeleportSound");
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
            //scoreText.text = Mathf.Round(gameVariables.score).ToString();
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
            //scoreText.text = Mathf.Round(gameVariables.score).ToString();
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
        audioManager.Play("TeleportSound");

        yield return new WaitForSeconds(1.5f);
        tempPlat2.SetActive(false);
        gameObject.transform.position = tempPlat.transform.position;
        //yield return new WaitForSeconds(0.5f);
        rb2d.gravityScale = 5;
        //jumpForce = 800;
        rb2d.AddForce(Vector2.up * gameVariables.jumpForce);

        gameVariables.sendTelePortOnlyOnce = false;

        yield return null;


    }
}
