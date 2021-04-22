using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Controls the progress bar on the top of the canvas in gameplay

//get the list from the respawncollider script and add a bool to this one which is hte flag level. then loop through thelist
//to get the end difficulity based on the player is on level variable. then can control the progressbar

public class GameplayProgressBar : MonoBehaviour
{
    RespawnColliderScript respawnColliderScript;
    public GameVariables gameVariables;
    private float levelEndDifficulty;
    public Slider progressBar;
    private float progress;

    private void Awake()
    {
        
        
    }


    // Start is called before the first frame update
    void Start()
    {
        respawnColliderScript = GameObject.Find("RespawnCollider").GetComponent<RespawnColliderScript>();
        respawnColliderScript.levelGeneratorEvent.AddListener(UpdateProgressBar);


        progressBar.value = 0;
        //find the flag levels difficilty from. This is the finish number for the progressbar
        foreach(RespawnColliderScript.Level level in respawnColliderScript.levels)
        {
            if (level.level == gameVariables.playerOnLevel && level.flagLevel == true)
            {
                levelEndDifficulty = (float)level.difficultyFrom;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void UpdateProgressBar(int levelID, Collider2D collision)
    {
        if (gameVariables.difficulty != 0)
        {
            progress = Mathf.Clamp01(gameVariables.difficulty / levelEndDifficulty);
        }

        print(levelEndDifficulty);
        print(progress);
        progressBar.value = progress;
    }


}
