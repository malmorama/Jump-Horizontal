using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//from event platform scroll coins right. starts via interface from player logic scripts
public class CoinScrollFromRight : MonoBehaviour, IEventPlatforms
{

    GameVariables GameVariables;
    private int count;
    private float offset = 3f;
    //private int layers;
    private int lenght;
    private int height;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        active = false;
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EventPlatformAction()
    {
        StopCoroutine(ChangeBool());

        if (active == false)
        {
            active = true;
            //layers = Random.Range(1, 3);
            lenght = Random.Range(5, 15);
            height = Random.Range(1, 5);
          
            Vector2 position = new Vector2(Camera.main.transform.position.x + 42, 10f + height);
            //Vector2 position2 = new Vector2(Camera.main.transform.position.x + 42, 10f + height + offset);
            for (int i = 0; i < lenght; i++)
            {
                ObjectPooler.Instance.SpawnFromPool("Gold", position, Quaternion.identity);
                position = new Vector2(position.x + offset, position.y);

                //if (layers >= 2)
                //{
                //    for (int ii = 0; ii < layers; ii++)
                //    {
                //        ObjectPooler.Instance.SpawnFromPool("Gold", position2, Quaternion.identity);
                //        position2 = new Vector2(position2.x + offset, position2.y);
                //    }
             //   }
            }
        }
        StartCoroutine(ChangeBool());
    }


    private IEnumerator ChangeBool()
    {
        yield return new WaitForSeconds(1f);
        active = false;
    }

    //not used
    public void StartCourtineCoinFromRight()
    {
       
    }

    //not used
    private IEnumerator CourtineCoinFromRight()
    {
        while (count > 10)
        {
            Vector2 position = new Vector2(Camera.main.transform.position.x + 42, 10f);
            ObjectPooler.Instance.SpawnFromPool("Gold", position, Quaternion.identity);
            count++;
            yield return new WaitForSeconds(0.10f);
        }
        yield return null;
    }

}
