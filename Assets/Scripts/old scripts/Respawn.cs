using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    GameObject ObjectToMove;
   
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectToMove = GetComponent<CheckOffScreen>().Check();
        if (ObjectToMove.name.StartsWith("Platform"))
        {
            Destroy(ObjectToMove);
        }
    }
}
