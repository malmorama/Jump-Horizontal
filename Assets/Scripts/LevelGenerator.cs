﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//takes data from levergeneratorvariables a scriptableobject and based on this generate the level ID
public class LevelGenerator : MonoBehaviour
{
    /*
        [System.Serializable]
        public class LevelContent
        {
            public string methodName;
            public bool methodActive;
            public int randomNumberFrom;
            public int randomNumberTo;
            public string objRespawn;
            public string objOnTop;
        }

        */

    //public int levelIDHeader;
    //public int randomNumberGeneratedFrom;
    //public int randomNumberGeneratedTo;
   // public List<LevelContent> levelContent;
    public GameVariables gameVariables;
    private int randomNumber;
    //private bool respawnOnce = false;

    public LevelGeneratorVariables levelGeneratorVariables;

    private void Start()
    {
        gameVariables.respawnOnce = false;  
    }


    //checks the levelid and match it with the attached scriptable object. if they match start the generator 
    //based on the data in the list set in the inspector in the scriptable object attached to this script, we generate level
    public void LevelGeneratorMetod(int levelID, Collider2D collision)
    {
        
        print(levelID + collision.gameObject.name);
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
        gameVariables.playerOnLevel = levelGeneratorVariables.Level;
        randomNumber = Random.Range(levelGeneratorVariables.randomNumberGeneratedFrom, levelGeneratorVariables.randomNumberGeneratedTo);
        checkIfRespawnHeart(); // set heart active to respawn or not only if life is 1

        //if flag send it only once on top of a platform
        if (levelGeneratorVariables.levelContent[0].objOnTop == "Flag")
        {
            //deactivate collision object
            collision.gameObject.SetActive(false);

            //run metod that only sends one platform with flag on top
            if(gameVariables.respawnOnce == false)
            {
                RespawnObjectWithObjectOnTop(collision, levelGeneratorVariables.levelContent[0].objRespawn, levelGeneratorVariables.levelContent[0].objOnTop,
                    levelGeneratorVariables.levelContent[0].objOnTopHeight);
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
