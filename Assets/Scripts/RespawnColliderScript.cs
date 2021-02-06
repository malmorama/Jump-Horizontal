using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnColliderScript : MonoBehaviour
{
    public GameVariables gameVariables;
    private List<string> objectsToCheck = new List<string>();
    //private Dictionary<string, Queue<int>> objectsProbability;
    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        //objectsProbability = new Dictionary<string, Queue<int>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //detect collision on RespawnCollider that is a object in the hierarchy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        respawnBasedOnDifficulty(gameVariables.difficulty, collision);
    }

    //based on difficuly in game, different respawnn decision
    private void respawnBasedOnDifficulty(int difficulty, Collider2D collision)
    {
        if (difficulty == 0)
        {
            //make it inactive and send the standard platform
            collision.gameObject.SetActive(false);
            RespawnPlatform();
        }
        if (difficulty >= 1 && difficulty <= 3)
        {
            //send the standard platform and send gold based on random number. Send some spring platforms

            //objects to detactivate
            objectsToCheck.Add("Gold");
            objectsToCheck.Add("Heart");
            ObjectsToDeactivate(collision, objectsToCheck);
            objectsToCheck.Clear();

            //objects to respawn
            randomNumber = Random.Range(1, 40);
            if (randomNumber >= 1 && randomNumber <= 15)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Gold");
            }
            else if (randomNumber == 16 || randomNumber == 17)
            {
                RespawnAnotherObject(collision, "Platform", "Spring");
            }
            else if (randomNumber == 21 && gameVariables.life == 1)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Heart");
            }
            //else if (randomNumber == 12 || randomNumber == 13)
            //{
            //    RespawnAnotherObject(collision, "Platform", "BrownPlatform");
            //}
            else
            {
                MoveObject(collision);
            }

            //if respawn collider hit spring send std platform
            if (collision.gameObject.name.StartsWith("Spring"))
            {
                RespawnAnotherObject(collision, "Spring", "Platform");
            }

        }


        if (difficulty >= 4 && difficulty <= 8)
        {
            //send the standard platform and send gold based on random number. Send some spring platforms

            //objects to detactivate
            objectsToCheck.Add("Gold");
            objectsToCheck.Add("Heart");
            ObjectsToDeactivate(collision, objectsToCheck);
            objectsToCheck.Clear();

            //objects to respawn
            randomNumber = Random.Range(1, 40);
            if (randomNumber >= 1 && randomNumber <= 15)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Gold");
            }
            else if (randomNumber == 16 || randomNumber == 17)
            {
                RespawnAnotherObject(collision, "Platform", "Spring");
            }
            else if (randomNumber == 18 || randomNumber == 19)
            {
                RespawnAnotherObject(collision, "Platform", "BrownPlatform");
            }
            else if (randomNumber == 21 && gameVariables.life == 1)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Heart");
            }
            else
            {
                MoveObject(collision);
            }

            //if respawn collider hit spring send std platform
            if (collision.gameObject.name.StartsWith("Spring"))
            {
                RespawnAnotherObject(collision, "Spring", "Platform");
            }

            //if respawn collider hit spring send std platform
            if (collision.gameObject.name.StartsWith("BrownPlatform"))
            {
                RespawnAnotherObject(collision, "BrownPlatform", "Platform");
            }

        }
        if (difficulty >= 9 && difficulty <= 20)
        {
            //send the standard platform and send gold based on random number. Send some spring platforms

            //objects to detactivate
            objectsToCheck.Add("Gold");
            objectsToCheck.Add("Pink");
            objectsToCheck.Add("Heart");
            ObjectsToDeactivate(collision, objectsToCheck);
            objectsToCheck.Clear();

            //objects to respawn
            randomNumber = Random.Range(1, 40);
            if (randomNumber >= 1 && randomNumber <= 15)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Gold");
            }
            else if (randomNumber == 16 || randomNumber == 18)
            {
                RespawnAnotherObject(collision, "Platform", "Spring");
            }
            else if (randomNumber == 19 || randomNumber == 21)
            {
                RespawnAnotherObject(collision, "Platform", "BrownPlatform");

            }
            else if (randomNumber == 22 && gameVariables.life == 1)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Heart");
            }
            else if (randomNumber == 23 || randomNumber == 24)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Pink");
            }
            else
            {
                MoveObject(collision);
            }

            //if respawn collider hit spring send std platform
            if (collision.gameObject.name.StartsWith("Spring"))
            {
                RespawnAnotherObject(collision, "Spring", "Platform");
            }

            //if respawn collider hit spring send std platform
            if (collision.gameObject.name.StartsWith("BrownPlatform"))
            {
                RespawnAnotherObject(collision, "BrownPlatform", "Platform");
            }

        }

        if (difficulty >= 21)
        {
            //same as difficulity 4 but add brown platform and pink collection and instantate heart

            //objects to detactivate
            objectsToCheck.Add("Gold");
            objectsToCheck.Add("Pink");
            objectsToCheck.Add("Heart");
            objectsToCheck.Add("Teleportstation");
            ObjectsToDeactivate(collision, objectsToCheck);
            objectsToCheck.Clear();

            //objects to respawn
            randomNumber = Random.Range(1, 40);
            if (randomNumber >= 1 && randomNumber <= 15)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Gold");
            }
            else if (randomNumber >= 16 && randomNumber <= 18)
            {
                RespawnAnotherObject(collision, "Platform", "Spring");
            }
            else if (randomNumber == 19 || randomNumber == 23 && gameVariables.sendTelePortOnlyOnce == false)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Teleportplatform", "Teleportstation");
            }
            else if (randomNumber == 20)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Pink");
            }
            else if (randomNumber == 21 && gameVariables.life == 1)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Heart");
            }
            else if (randomNumber == 22)
            {
                RespawnAnotherObject(collision, "Platform", "BrownPlatform");
            }

            else
            {
                MoveObject(collision);
            }


            //Respawn standardplatform after collision with these
            if (collision.gameObject.name.StartsWith("BrownPlatform"))
            {
                RespawnAnotherObject(collision, "BrownPlatform", "Platform");
            }

            if (collision.gameObject.name.StartsWith("Spring"))
            {
                RespawnAnotherObject(collision, "Spring", "Platform");
            }


            if (collision.gameObject.name.StartsWith("Teleportplatform"))
            {
                RespawnAnotherObject(collision, "Teleportplatform", "Platform");
            }


        }


    }

    private void BaseRespawnMetod(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Platform"))
        {
            RespawnRandomObjects(collision.gameObject);
        }
        else if (collision.gameObject.name.StartsWith("Gold"))
        {
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.StartsWith("Spring"))
        {
            collision.gameObject.SetActive(false);
            RespawnPlatform();
        }
    }

    

    //sends standard platform, spring and coin based on random number
    private void RespawnRandomObjects(GameObject gameObject)
    {
        int randomNumber;
        randomNumber = Random.Range(1, 15);
        if (randomNumber == 1)
        {
            //Send the kill platform (spring prefab)
            //Destroy(gameObject);
            gameObject.SetActive(false);
            Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            //newPlat = Instantiate(springPrefab, position, Quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool("Spring", position, Quaternion.identity);
        }
        else if (randomNumber == 2 || randomNumber == 3 || randomNumber == 4 || randomNumber == 5)
        {
            //send standard platform and instatiate coin on top of new object
            //std platform
            //Destroy(gameObject);
            gameObject.SetActive(false);
            Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            //newPlat = Instantiate(platformPrefab, position, Quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool("Platform", position, Quaternion.identity);

            //instantiate new coin
            Vector2 position2 = new Vector2(position.x, position.y + 3);
            //newCoin = Instantiate(coinPrefab, position2, Quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool("Gold", position2, Quaternion.identity);
        } else
        {
           //move the standard platform
           gameObject.transform.position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
        }
        //}
    }

    //sends only the std platform
    private void RespawnPlatform()
    {
        //send the standard platform
        Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
        ObjectPooler.Instance.SpawnFromPool("Platform", position, Quaternion.identity);

        //gameObject.transform.position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
    }


    private void ObjectsToDeactivate(Collider2D collision, List<string> objectsToCheck)
    {
        for (int i = 0; i < objectsToCheck.Count; i++)
        {
            string tempName = objectsToCheck[i];
            //objects to detactivate
            if (collision.gameObject.name.StartsWith(tempName))
            {
                collision.gameObject.SetActive(false);
            }
        }

        //name.Clear();

       
    }

    private void RespawnObjectWithObjectOnTop(Collider2D collision, string objectThatHitCollider, string objectBelowToRespawn, string objectAboveToRespawn)
    {
        if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        {
            //object below to inactivate and respawn
            collision.gameObject.SetActive(false);
            Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            ObjectPooler.Instance.SpawnFromPool(objectBelowToRespawn, position, Quaternion.identity);

            //object above
            Vector2 position2 = new Vector2(position.x, position.y + 3);
            ObjectPooler.Instance.SpawnFromPool(objectAboveToRespawn, position2, Quaternion.identity);
        }
    }

    private void RespawnAnotherObject(Collider2D collision, string objectThatHitCollider, string objectToRespawn)
    {
        if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        {
            collision.gameObject.SetActive(false);
            Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            //newPlat = Instantiate(springPrefab, position, Quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool(objectToRespawn, position, Quaternion.identity);
        }
    }

    private void MoveObject(Collider2D collision)
    {
        collision.gameObject.transform.position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
    }


    private void TestTheCode(Collider2D collision)
    {
        
            int randomNumber;
            randomNumber = Random.Range(1, 15);
            if (randomNumber == 1)
            {
                RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Coin");
            }

    }

}
