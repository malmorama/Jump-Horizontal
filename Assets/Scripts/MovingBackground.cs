using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public GameVariables gameVariables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //script to move the background
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(gameVariables.backgroundScrollSpeed * Time.time, 0);
    }
}
