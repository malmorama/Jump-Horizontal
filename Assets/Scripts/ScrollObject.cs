using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
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
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - (gameVariables.platformScrollSpeed * Time.deltaTime) , gameObject.transform.position.y);
    }
    

}
