using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOffScreen : MonoBehaviour
{
    float xOffset = 38f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject Check()
        {
            if (gameObject.transform.position.x <= (Camera.main.transform.position.x - xOffset))
            {
                //print("tets");
                return gameObject;
            }
            else
            { return null; }
        }

    
}
