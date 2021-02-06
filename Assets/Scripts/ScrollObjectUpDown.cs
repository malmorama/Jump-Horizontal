using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObjectUpDown : MonoBehaviour
{
    public GameVariables gameVariables;
    float scrollSpeed;
    float scrollSpeedY = 8f;
    float yTop = 15f;
    float yLow = -8f;

    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = gameVariables.platformScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - scrollSpeed * Time.deltaTime, gameObject.transform.position.y + scrollSpeedY * Time.deltaTime);
        

        if (gameObject.transform.position.y > yTop)
        {
            scrollSpeedY = -8f;
        }
        if (gameObject.transform.position.y < yLow)
        {
            scrollSpeedY = 8f;
        }

    }
}
