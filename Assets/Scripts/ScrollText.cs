using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    // Start is called before the first frame update
    float scrollSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move the object as outside of screen
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - scrollSpeed * Time.deltaTime, gameObject.transform.position.y);
    }
}
