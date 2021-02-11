using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RightSignUp : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendCoins(GameObject hitGameObject)
    {
        float offset = 2f;
        int offsetOnce = 4;
        Vector2 position = new Vector2(hitGameObject.transform.position.x + offsetOnce, hitGameObject.transform.position.y + offsetOnce);
        for (int i = 0; i < 5; i++)
        {         
            ObjectPooler.Instance.SpawnFromPool("Gold", position, Quaternion.identity);
            position = new Vector2(position.x + offset, position.y + offset);
            //offset += offset;
            //offsetOnce = 0;
        }
       // gameObject.SetActive(false);
        
    }


}
