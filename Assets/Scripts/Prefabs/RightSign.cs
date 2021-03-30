using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RightSign : MonoBehaviour
{
    public float offset = 2f;
    public int offsetOnce = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendCoinsUp(GameObject hitGameObject)
    {
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

    public void SendCoinsDown(GameObject hitGameObject)
    {
        Vector2 position = new Vector2(hitGameObject.transform.position.x + offsetOnce, hitGameObject.transform.position.y - offsetOnce);
        for (int i = 0; i < 5; i++)
        {
            ObjectPooler.Instance.SpawnFromPool("Gold", position, Quaternion.identity);
            position = new Vector2(position.x + offset, position.y - offset);
            //offset += offset;
            //offsetOnce = 0;
        }
        // gameObject.SetActive(false);

    }


}
