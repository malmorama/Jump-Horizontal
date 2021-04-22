using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used attached to pink diamond prefab.
public class FloatUpDown : MonoBehaviour
{
    private float yTop;
    private float yLow;
    private float scrollSpeedY;
    private float scrollSpeedYTurn;
    private float scrollSpeedYStart;
    public GameVariables gameVariables;


    // Start is called before the first frame update
    void Start()
    {
        yTop = gameObject.transform.position.y + 4f;
        yLow = gameObject.transform.position.y - 0f;
        scrollSpeedY = gameVariables.brownPlatformScrollSpeed;
        scrollSpeedYStart = scrollSpeedY; 
        scrollSpeedYTurn = -1f * scrollSpeedY;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + scrollSpeedY * Time.deltaTime);


        if (gameObject.transform.position.y > yTop)
        {
            scrollSpeedY = scrollSpeedYTurn;
        }
        if (gameObject.transform.position.y < yLow)
        {
            scrollSpeedY = scrollSpeedYStart;
        }
    }
}
