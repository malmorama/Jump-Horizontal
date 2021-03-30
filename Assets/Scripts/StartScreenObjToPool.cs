using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenObjToPool : MonoBehaviour
{
    private GameObject[] objectsFromStartScreen;
    public string nameOfObjToTakeFromPool;
    public string nameOfStartScreenTag;

    // Start is called before the first frame update
    void Start()
    {
        objectsFromStartScreen = GameObject.FindGameObjectsWithTag(nameOfStartScreenTag);
        foreach (GameObject objectsFromStartScreen in objectsFromStartScreen)
        {
            //send from pool and desroy what is on screen
            ObjectPooler.Instance.SpawnFromPool(nameOfObjToTakeFromPool, objectsFromStartScreen.transform.position, objectsFromStartScreen.transform.rotation);
            Destroy(objectsFromStartScreen); 
        }
    }

   
}
