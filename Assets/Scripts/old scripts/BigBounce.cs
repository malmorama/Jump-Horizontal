using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    //if hits a red platform player death, game restarts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player_fox_right" && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }*/
}
