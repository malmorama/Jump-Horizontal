using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    public AudioSource playerJumpSound;
    private Rigidbody2D rb2d;
    public GameVariables gameVariables;
    private float oldPlatformSpeed;
    private float oldBackgroundSpeed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //oldPlatformSpeed = gameVariables.platformScrollSpeed;
        //oldBackgroundSpeed = gameVariables.platformScrollSpeed;
        oldPlatformSpeed = 18;
        oldBackgroundSpeed = 0.08f;

    }


    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb2d.velocity.y <= 0 && collision.gameObject.name.StartsWith("Player"))
        {

            //collision.gameObject.SetActive(false);
            gameVariables.platformScrollSpeed = 22;
            gameVariables.backgroundScrollSpeed = 0.13f;
            yield return new WaitForSeconds(2f);
            print(oldBackgroundSpeed);
            gameVariables.platformScrollSpeed = oldPlatformSpeed;
            gameVariables.backgroundScrollSpeed = oldBackgroundSpeed;
            //StartCoroutine(TeleportPlatformCoroutine(collision));

        }

        yield return null;
    }



}