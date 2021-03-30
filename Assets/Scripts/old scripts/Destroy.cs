using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    public GameObject player;
    public GameObject platformPrefab;
    private GameObject newPlat;
    public GameObject springPrefab;
     
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    

        /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.name.StartsWith("Platform") || collision.gameObject.name.StartsWith("Spring"))
        {
            if(Random.Range(1,15) == 1)
            {
                Destroy(collision.gameObject);
                Vector2 position = new Vector2(player.gameObject.transform.position.x + (30 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
                newPlat = Instantiate(springPrefab, position, Quaternion.identity);
            } else
            {
                collision.gameObject.transform.position = new Vector2(player.gameObject.transform.position.x + (30 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            }



        }
    //Vector2 position = new Vector2(Random.Range(-10f, 10f), player.gameObject.transform.position.y + (22 * Random.Range(1f, 1.1f)));
    //  newPlat = Instantiate(platformPrefab, position, Quaternion.identity);
    //Destroy(collision.gameObject);

    }*/
}
