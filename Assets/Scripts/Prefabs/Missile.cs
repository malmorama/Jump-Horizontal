using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//moves the missle across the screen. attached to the prefab
public class Missile : MonoBehaviour
{
    public GameVariables gameVariables;
    //float scrollSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //scrollSpeed = gameVariables.platformScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //move the object as outside of screen
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - (gameVariables.missileSpeed * Time.deltaTime), gameObject.transform.position.y);
    }
}
