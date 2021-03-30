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
        public int level;
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
        objectsToCheck.Add("DustCloud");
        //send std platform if collider hits these
        respawnOtherObject.Add("TeleportPlatform");
        respawnOtherObject.Add("Spring");
        respawnOtherObject.Add("BrownPlatform");
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


        //checks the difficulty vs what is preset in the list in the editor, send the levelid and collision to levelgenerator script that
        //take the info to generate level
        LevelCheck(difficulty, collision);

    }

    private void LevelCheck(int difficulty, Collider2D collision)
    {
        foreach (Level level in levels)
        {
            if (difficulty >= level.difficultyFrom && difficulty <= level.difficultyTo & gameVariables.playerOnLevel == level.level)
            {
                //print(level.levelId);
                //send levelid, collider to levelgeneratorscript
                levelGeneratorEvent.Invoke(level.levelId, collision);
            }
        }
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
