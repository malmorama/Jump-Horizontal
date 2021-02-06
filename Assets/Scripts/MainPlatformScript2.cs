using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatformScript2 : MonoBehaviour
{
    float xOffset = 45f;

    private GameObject newPlat;
    public GameObject springPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if gameobject outside screen respawn
        if (gameObject.transform.position.x <= (Camera.main.transform.position.x - xOffset))
        {
            if (Random.Range(1, 15) == 1)
            {
                Destroy(gameObject);
                Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
                newPlat = Instantiate(springPrefab, position, Quaternion.identity);
            }
            else
            {
                gameObject.transform.position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            }
        }
    }



    //If collision with platform the player jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800f);
        }
    }

}

