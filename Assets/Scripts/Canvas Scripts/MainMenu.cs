using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is used on all menues in all scenes
public class MainMenu : MonoBehaviour
{
     
   //private bool menuPressedPlay = false;
    FadeScreen fadeScreen;
    //public double transitionTime = 0.5f;
    public GameVariables gameVariables;
    AudioManager audioManager;
    public Toggle myToogle;



    void Awake()
    {
        // fadeScreen.SetOpacity(0);
        // fadeScreen.gameObject.SetActive(false);
        fadeScreen = GameObject.Find("FadeblackImage").GetComponent<FadeScreen>();
        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();
        
    }

    private void Start()
    {
        myToogle.onValueChanged.AddListener((value) =>
        {
            MusicOnOff(value);

        });
    }

    /* void Update()
     {
       if (menuPressedPlay)
         {
             StartCoroutine(FadeBlackPlayScene());
             //StartCoroutine(fadeScreen.FadeToBlackScreen());
         }
     }*/

    public void MusicOnOff(bool change)
    {
        if(!change)
        {
            audioManager.Stop("StartScreenMusic");
            gameVariables.allMusicOn = false;
        }
        else
        {
            audioManager.Play("StartScreenMusic");
            gameVariables.allMusicOn = true;
        }
    }

    


    public void PlayGame()
    {
        audioManager.Stop("StartScreenMusic");
        StartCoroutine(FadeBlackPlayScene());
        
    }



    public void Instructions()
    {
        StartCoroutine(FadeBlackInstructions());
       
    }

    public void Menu()
    {
        StartCoroutine(FadeBlackMenu());
        audioManager.Stop("TheMountainsSong");
        gameVariables.theMountainsSongIsPlaying = false;
        if (gameVariables.allMusicOn)
        {
            audioManager.Play("StartScreenMusic");
        }
    }

    public void PlayFromStatsScene()
    {
        StartCoroutine(FadeBlackPlayFromStatsScene());

    }

    public void NextLevel()
    {
        StartCoroutine(FadeBlackNextLevel());
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeBlackPlayScene()
    {
        StartCoroutine(fadeScreen.FadeToBlackScreen());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Gameplay");
    }

    private IEnumerator FadeBlackInstructions()
    {
        StartCoroutine(fadeScreen.FadeToBlackScreen());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Instructions");
    }

    private IEnumerator FadeBlackMenu()
    {
        StartCoroutine(fadeScreen.FadeToBlackScreen());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator FadeBlackPlayFromStatsScene()
    {
        if(gameVariables.life > 0)
        {
            gameVariables.difficulty++;
            StartCoroutine(fadeScreen.FadeToBlackScreen());
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("Gameplay");
        }
        else
        {
            gameVariables.score = 0f;
            gameVariables.difficulty = 0;
            gameVariables.life = 1;
            gameVariables.CurrentScoreToDifficulity = 0;
            StartCoroutine(fadeScreen.FadeToBlackScreen());
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("Gameplay");
        }

 
    }


    //not used
    private IEnumerator FadeBlackNextLevel()
    {
        gameVariables.difficulty++;
        StartCoroutine(fadeScreen.FadeToBlackScreen());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Gameplay");
    }


}
