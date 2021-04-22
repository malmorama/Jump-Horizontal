using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

//for the event platform send missile. start if player hit it via interface call 

public class AttentionSendMissile : MonoBehaviour, IEventPlatforms
{
    private bool hitPlatform;
    private float height;
    private GameObject attention;
    //_UnityEventGameObject timerThenSendMissile;

    // Start is called before the first frame update
    void Start()
    {
        hitPlatform = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EventPlatformAction()
    {
        if (hitPlatform == false)
        {
            hitPlatform = true;
            height = Random.Range(-10f, 2f);
            Vector2 position = new Vector2(Camera.main.transform.position.x + 38, 10f + height);
            attention = ObjectPooler.Instance.SpawnFromPool("Attention", position, Quaternion.identity);
            CountDown2();
        }
    }
    //flash the 
    private IEnumerator CountDown()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        Vector2 position2 = new Vector2(Camera.main.transform.position.x + 42, attention.transform.position.y);
        ObjectPooler.Instance.SpawnFromPool("Missile", position2, Quaternion.identity);
        attention.gameObject.SetActive(false);
    }

    private async void CountDown2()
    {
        for (int i = 0; i < 2; i++)
        {
            await Task.Delay(1000);
        }
        Vector2 position2 = new Vector2(Camera.main.transform.position.x + 42, attention.transform.position.y);
        ObjectPooler.Instance.SpawnFromPool("Missile", position2, Quaternion.identity);
        attention.gameObject.SetActive(false);
        hitPlatform = false;
    }
        
}