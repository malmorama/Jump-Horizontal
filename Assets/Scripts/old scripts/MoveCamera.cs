using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float scrollSpeed = 0.0025f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + scrollSpeed, Camera.main.transform.position.y , Camera.main.transform.position.z);
    }
}
