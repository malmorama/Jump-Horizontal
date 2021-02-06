using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCloud : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;


    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //move the object as outside of screen
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - scrollSpeed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        if(gameObject.transform.position.x < -12f)
        {
            Vector3 position2 = new Vector3(gameObject.transform.position.x + 25, gameObject.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.position = position2;
        }
    }
}
