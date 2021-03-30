using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DDOL : MonoBehaviour
{
    public GameVariables gameVariables;
    //private int CurrentScoreToDifficulity;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        gameVariables.jumpForce = 725f;
        gameVariables.playerOnLevel = 1;
        gameVariables.score = 0f;
        gameVariables.highScore = 0f;
        gameVariables.difficulty = 0;
        gameVariables.life = 1;
        gameVariables.coin = 0;
        gameVariables.backgroundScrollSpeed = 0.08f;
        gameVariables.CurrentScoreToDifficulity = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        StartCoroutine(GameManagerLoop());
        //SceneManager.LoadScene("Gameplay");
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GameManagerLoop()
    {
        while (true)
        {
            if (gameVariables.respawnOnce == false)
            {
                gameVariables.difficulty += IncreaseDifficulity(gameVariables.score);
      
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    //function to set difficulity based on score
    private int IncreaseDifficulity(float score)
    {
        int ScoreToDifficulity = Mathf.RoundToInt(score / 150);

        if (ScoreToDifficulity > gameVariables.CurrentScoreToDifficulity)
        {
            gameVariables.CurrentScoreToDifficulity = ScoreToDifficulity;
            return 1;
        }
        else
        {
            return 0;
        }


    }

}
