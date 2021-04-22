using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//takes data from levergeneratorvariables a scriptableobject that is attached to this script. Is a game object
//under the resspawn collider and based on data in the scriptable object generate the leval based on the level ID and what
//hits the collider to the left
public class LevelGenerator : MonoBehaviour
{
    public GameVariables gameVariables;
    private int randomNumber;
    //private bool respawnOnce = false;

    //private GameObject tempHeart;

    public LevelGeneratorVariables levelGeneratorVariables;
    private RespawnColliderScript respawnColliderScript;



    private void Start()
    {
        respawnColliderScript = GameObject.Find("RespawnCollider").GetComponent<RespawnColliderScript>();
        respawnColliderScript.levelGeneratorEvent.AddListener(LevelGeneratorMetod);

        //tempHeart = GameObject.FindWithTag("Heart");

        gameVariables.respawnOnce = false;

        //check if what level the player is on to set the background scroll speed
        if(gameVariables.playerOnLevel == levelGeneratorVariables.Level)
        {
            gameVariables.platformScrollSpeed = levelGeneratorVariables.platformScrollSpeed;
            gameVariables.backgroundScrollSpeed = levelGeneratorVariables.backgroundScrollSpeed;
        }
        
    }


    //checks the levelid and match it with the attached scriptable object. if they match start the generator
    //that loop through the levelcontent class and respawn based on content in the call
    //based on the data in the list set in the inspector in the scriptable object attached to this script, we generate level
    public void LevelGeneratorMetod(int levelID, Collider2D collision)
    {
        
        //print(levelID + collision.gameObject.name);
        if (levelID == levelGeneratorVariables.levelIDHeader)
        {
            //only generate based on platform, all other is just deactivated as hit the collider
            if (collision.gameObject.name.StartsWith("Platform"))
            {
                LevelGeneratorLoop(collision);
            }
        }
    }

    //loop through the list in the inspector to generate the level
    private void LevelGeneratorLoop(Collider2D collision)
    {
        gameVariables.playerOnLevel = levelGeneratorVariables.Level;    //set the player level on the gamevariable same as the level level
        

        randomNumber = Random.Range(levelGeneratorVariables.randomNumberGeneratedFrom, levelGeneratorVariables.randomNumberGeneratedTo);
        checkIfRespawnHeart(); // set heart active to respawn or not only if life is 1
        IfActiveOnScreenDontRespawn();

        //if flag send it only once on top of a platform
        if (levelGeneratorVariables.levelContent[0].objOnTop == "Flag")
        {
            //deactivate collision object
            collision.gameObject.SetActive(false);

            //run metod that only sends one platform with flag on top
            if(gameVariables.respawnOnce == false)
            {
                Vector2 flagPlatformPos = new Vector2(Camera.main.transform.position.x + 65, 0);
                Vector2 flagPos = new Vector2(Camera.main.transform.position.x + 65, 0 + levelGeneratorVariables.levelContent[0].objOnTopHeight);
                ObjectPooler.Instance.SpawnFromPool(levelGeneratorVariables.levelContent[0].objRespawn, flagPlatformPos, Quaternion.identity); // the flagplatform
                ObjectPooler.Instance.SpawnFromPool(levelGeneratorVariables.levelContent[0].objOnTop, flagPos, Quaternion.identity); // the flag
                gameVariables.respawnOnce = true;
            }
            
        }
        //if not flag run the standard loop to generate level
        else
        {
            int length = levelGeneratorVariables.levelContent.Count;
            for (int i = 0; i < length; i++)
            {

                if (randomNumber >= levelGeneratorVariables.levelContent[i].randomNumberFrom
                    && randomNumber <= levelGeneratorVariables.levelContent[i].randomNumberTo
                    && levelGeneratorVariables.levelContent[i].methodName == "RespawnObjectWithObjectOnTop"
                    && levelGeneratorVariables.levelContent[i].methodActive == true)
                {
                    RespawnObjectWithObjectOnTop(collision, levelGeneratorVariables.levelContent[i].objRespawn, levelGeneratorVariables.levelContent[i].objOnTop,
                        levelGeneratorVariables.levelContent[i].objOnTopHeight);
                }
                else if (randomNumber >= levelGeneratorVariables.levelContent[i].randomNumberFrom
                    && randomNumber <= levelGeneratorVariables.levelContent[i].randomNumberTo
                    && levelGeneratorVariables.levelContent[i].methodName == "RespawnAnotherObject"
                    && levelGeneratorVariables.levelContent[i].methodActive == true)
                {
                    RespawnAnotherObject(collision, levelGeneratorVariables.levelContent[i].objRespawn);
                }
                else
                {
                    MoveObject(collision);
                }

                
            }
        }
    }

    //set an object in the edtior list active or deactive based ona rule
    private void checkIfRespawnHeart()
    {
        int length = levelGeneratorVariables.levelContent.Count;
        for (int i = 0; i < length; i++)
        {
            if (levelGeneratorVariables.levelContent[i].objOnTop == "Heart" && gameVariables.life > 1)
            {
                levelGeneratorVariables.levelContent[i].methodActive = false;
            }


            if (levelGeneratorVariables.levelContent[i].objOnTop == "Heart" && gameVariables.life == 1)
            {
                levelGeneratorVariables.levelContent[i].methodActive = true;
            }

        }

    }

    private void IfActiveOnScreenDontRespawn()
    {
        int length = levelGeneratorVariables.levelContent.Count;
        for (int i = 0; i < length; i++)
        {

            //if in the list and active on screen dont send it again
            if (GameObject.FindWithTag("Heart") != null)
            {
                if (levelGeneratorVariables.levelContent[i].objOnTop == "Heart" && GameObject.FindWithTag("Heart").activeInHierarchy == true)
                {
                    levelGeneratorVariables.levelContent[i].methodActive = false;
                }
            }

            // if (GameObject.FindWithTag("HeliPrefab") == null)
            // { 
            if (levelGeneratorVariables.levelContent[i].objOnTop == "HeliPrefab")
            {
                levelGeneratorVariables.levelContent[i].methodActive = true;
            }
            //  }

            //if in the list and active on screen dont send it again
            if (GameObject.FindWithTag("HeliPrefab") != null)
            {
                if (levelGeneratorVariables.levelContent[i].objOnTop == "HeliPrefab" && GameObject.FindWithTag("HeliPrefab").activeInHierarchy == true)
                {
                    levelGeneratorVariables.levelContent[i].methodActive = false;
                }
            }

           
        }
    }


    private void RespawnObjectWithObjectOnTop(Collider2D collision, string objectBelowToRespawn, string objectAboveToRespawn, int objectAboveToRespawnHeight)
    {
        //if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        //{
        //object below to inactivate and respawn
        collision.gameObject.SetActive(false);
        Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
        ObjectPooler.Instance.SpawnFromPool(objectBelowToRespawn, position, Quaternion.identity);

        //object above
        Vector2 position2 = new Vector2(position.x, position.y + objectAboveToRespawnHeight);
        ObjectPooler.Instance.SpawnFromPool(objectAboveToRespawn, position2, Quaternion.identity);
        //}
    }

    private void RespawnAnotherObject(Collider2D collision, string objectToRespawn)
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
