using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Just play the song in the active scene
public class SceneMusic : MonoBehaviour
{
    AudioManager audioManager;
    public GameVariables gameVariables;
    //private RespawnColliderScript respawnColliderScript;

    private void Awake()
    {
        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();
        //respawnColliderScript = GameObject.Find("RespawnCollider").GetComponent<RespawnColliderScript>();
        //respawnColliderScript.levelGeneratorEvent.AddListener(PlaySceneMusic);
    }


    // Start is called before the first frame update
    void Start()
    {
      if(gameVariables.playerOnLevel == 1 && !gameVariables.theMountainsSongIsPlaying && gameVariables.allMusicOn)
        {
            audioManager.Play("TheMountainsSong");
            gameVariables.theMountainsSongIsPlaying = true;
        }


    }
 


}
