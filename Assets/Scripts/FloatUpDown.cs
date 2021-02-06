using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    private float yTop;
    private float yLow;
    private float scrollSpeedY;


    // Start is called before the first frame update
    void Start()
    {
        yTop = gameObject.transform.position.y + 4f;
        yLow = gameObject.transform.position.y - 0f;
        scrollSpeedY = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + scrollSpeedY * Time.deltaTime);


        if (gameObject.transform.position.y > yTop)
        {
            scrollSpeedY = -4f;
        }
        if (gameObject.transform.position.y < yLow)
        {
            scrollSpeedY = 4f;
        }
    }
}
