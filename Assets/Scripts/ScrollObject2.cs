using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject2 : MonoBehaviour
{
    float scrollSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //move the object as
    void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - scrollSpeed, gameObject.transform.position.y);
    }
}
