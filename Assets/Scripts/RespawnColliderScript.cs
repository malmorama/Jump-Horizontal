using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class _UnityEventLevelGenerator : UnityEvent<int, Collider2D> { }

public class RespawnColliderScript : MonoBehaviour
{
   [System.Serializable]
    public class Level
    {
        public int levelId;
        public int difficultyFrom;
        public int difficultyTo;
         
    }

    public List<Level> levels;
    public _UnityEventLevelGenerator levelGeneratorEvent;

    public GameVariables gameVariables;
    private List<string> objectsToCheck = new List<string>();
    private List<string> respawnOtherObject = new List<string>();
    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        

        //objectsProbability = new Dictionary<string, Queue<int>>();
        //objects to deactivate if they hit the respawn collider
        objectsToCheck.Add("Gold");
        objectsToCheck.Add("Pink");
        objectsToCheck.Add("Heart");
        objectsToCheck.Add("Teleportstation");
        objectsToCheck.Add("RightSignUp");
        respawnOtherObject.Add("Teleportplatform");
        respawnOtherObject.Add("Spring");
        respawnOtherObject.Add("BrownPlatform");
    }


    private void LevelCheck(int difficulty, Collider2D collision)
    {
        foreach (Level level in levels)
        {
            if (difficulty >= level.difficultyFrom && difficulty <= level.difficultyTo)
            {
                //print(level.levelId);
                //send levelid, collider to levelgeneratorscript
                levelGeneratorEvent.Invoke(level.levelId, collision);

            }
            else
            {
                print("no diffculty or level defined");
            }
        }
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
        //if these objects in this list hit the collider, just deactivate (heart etc collectables)
        ObjectsToDeactivate(collision, objectsToCheck);


        //Respawn standardplatform after respawncollider collide with these
        for (int i = 0; i < respawnOtherObject.Count; i++)
        {
            string tempName = respawnOtherObject[i];
            if (collision.gameObject.name.StartsWith(tempName))
            {
                RespawnAnotherObject(collision, tempName, "Platform");
            }
        }

        //make it inactive and send the standard platform, pool some platforms
        if (difficulty == 0)
        {
            
            if (collision.gameObject.name == "Platform (1)" || collision.gameObject.name == "Platform (2)"
                || collision.gameObject.name == "Platform (3)" || collision.gameObject.name == "Platform (4)"
                || collision.gameObject.name == "Platform (5)" || collision.gameObject.name == "Platform (6)"
                || collision.gameObject.name == "Platform (7)" || collision.gameObject.name == "Platform (8)")
            {
                collision.gameObject.name = "Platform(Clone)";
                ObjectPooler.Instance.AddToPool("Platform", collision.gameObject);
            }
            collision.gameObject.SetActive(false);
            RespawnPlatform();
        }

        //checks the difficulty vs what is preset in the list in the editor, send the levelid and collision to levelgenerator script that
        //take the info to generate level
        LevelCheck(difficulty, collision);


        //send the standard platform and send gold based on random number. Send some spring platforms
        if (difficulty >= 1 && difficulty <= 3)
        {
            if (collision.gameObject.name.StartsWith("Platform"))
            {
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
                else if (randomNumber == 22)
                {
                    RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "RightSignUp");
                }
                //else if (randomNumber == 12 || randomNumber == 13)
                //{
                //    RespawnAnotherObject(collision, "Platform", "BrownPlatform");
                //}
                else
                {
                    MoveObject(collision);
                }
            }

            //if respawn collider hit spring send std platform
            //if (collision.gameObject.name.StartsWith("Spring"))
            //{
            //    RespawnAnotherObject(collision, "Spring", "Platform");
            //}

        }

        //send the standard platform and send gold based on random number. Send some spring platforms
        if (difficulty >= 4 && difficulty <= 8)
        {
            

            if (collision.gameObject.name.StartsWith("Platform"))
            {
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
                else if (randomNumber >= 23 || randomNumber <= 40)
                {
                    RespawnAnotherObject(collision, "Platform", "BrownPlatform");
                }
                else if (randomNumber == 21 && gameVariables.life == 1)
                {
                    RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "Heart");
                }
                else if (randomNumber == 22)
                {
                    RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "RightSignUp");
                }
                else
                {
                    MoveObject(collision);
                }
            }

            //if respawn collider hit spring send std platform
            //if (collision.gameObject.name.StartsWith("Spring"))
            //{
            //    RespawnAnotherObject(collision, "Spring", "Platform");
            //}

            //if respawn collider hit spring send std platform
            //if (collision.gameObject.name.StartsWith("BrownPlatform"))
            //{
            //    RespawnAnotherObject(collision, "BrownPlatform", "Platform");
            //}

        }

        //send the standard platform and send gold based on random number. Send some spring platforms
        if (difficulty >= 9 && difficulty <= 20)
        {
            

            if (collision.gameObject.name.StartsWith("Platform"))
            {
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
                else if (randomNumber == 25)
                {
                    RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "RightSignUp");
                }
                else
                {
                    MoveObject(collision);
                }
            }

            //if respawn collider hit spring send std platform
            //if (collision.gameObject.name.StartsWith("Spring"))
            //{
            //    RespawnAnotherObject(collision, "Spring", "Platform");
            //}

            //if respawn collider hit spring send std platform
            //if (collision.gameObject.name.StartsWith("BrownPlatform"))
            //{
            //    RespawnAnotherObject(collision, "BrownPlatform", "Platform");
            //}

        }

        //same as difficulity 4 but add brown platform and pink collection and instantate heart
        if (difficulty >= 21)
        {
            

            if (collision.gameObject.name.StartsWith("Platform"))
            {
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
                else if (randomNumber == 23)
                {
                    RespawnObjectWithObjectOnTop(collision, "Platform", "Platform", "RightSignUp");
                }

                else
                {
                    MoveObject(collision);
                }
            }


            //Respawn standardplatform after collision with these
            //if (collision.gameObject.name.StartsWith("BrownPlatform"))
            //{
            //    RespawnAnotherObject(collision, "BrownPlatform", "Platform");
            //}

            //if (collision.gameObject.name.StartsWith("Spring"))
            //{
            //    RespawnAnotherObject(collision, "Spring", "Platform");
            //}


            //if (collision.gameObject.name.StartsWith("Teleportplatform"))
            //{
            //    RespawnAnotherObject(collision, "Teleportplatform", "Platform");
            //}


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
        //if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        //{
            //object below to inactivate and respawn
            collision.gameObject.SetActive(false);
            Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            ObjectPooler.Instance.SpawnFromPool(objectBelowToRespawn, position, Quaternion.identity);

            //object above
            Vector2 position2 = new Vector2(position.x, position.y + 3);
            ObjectPooler.Instance.SpawnFromPool(objectAboveToRespawn, position2, Quaternion.identity);
        //}
    }

    private void RespawnAnotherObject(Collider2D collision, string objectThatHitCollider, string objectToRespawn)
    {
        //if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        //{
            collision.gameObject.SetActive(false);
            Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
            //newPlat = Instantiate(springPrefab, position, Quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool(objectToRespawn, position, Quaternion.identity);
        //}
    }

    private void MoveObject(Collider2D collision)
    {
        collision.gameObject.transform.position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
    }


}
