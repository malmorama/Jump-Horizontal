using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public bool shouldExpand;

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary; // this is the dictonary that is the Object pool, consists of string and a queue
    //public List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void OnEnable()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotaton)
    {

        //check if does not contain the tag, print error message
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist");
            return null;
        }


        /*
        //old version
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotaton;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
        */
        
        Queue<GameObject> objectOutFromPoolQueue = new Queue<GameObject>();     //temp queue from the poolDictonary queue
        objectOutFromPoolQueue = poolDictionary[tag];

        //loop through the queue to make sure it is not active on the screen
        for (int i = 0; i < objectOutFromPoolQueue.Count; i++)
        {
            //GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            GameObject objectToSpawn = objectOutFromPoolQueue.Dequeue();
            if (!objectToSpawn.activeInHierarchy)
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotaton;
                poolDictionary[tag].Enqueue(objectToSpawn);
                return objectToSpawn;
            }
            poolDictionary[tag].Enqueue(objectToSpawn); //if obj active on screen add it back to the queue and check if next not active on screen
        }

        
        if (shouldExpand)
        {
            int lenght = pools.Count;
            for (int i = 0; i < lenght; i++)
            {
                if (pools[i].tag == tag)
                {
                    GameObject obj = Instantiate(pools[i].prefab, position, rotaton);
                    obj.SetActive(true);              
                    poolDictionary[tag].Enqueue(obj);                    
                    return obj;
                }
            }         

        }
        

        Debug.LogWarning("Pool with tag " + tag + " does not contain enough objects");
        return null;
        
    }

    public void AddToPool(string tag, GameObject objectToAdd)
    {
        poolDictionary[tag].Enqueue(objectToAdd);   
    }


}
