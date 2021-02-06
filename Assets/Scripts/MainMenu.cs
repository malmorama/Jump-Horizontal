using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     
   //private bool menuPressedPlay = false;
   FadeScreen fadeScreen;
   //public double transitionTime = 0.5f;


    void Awake()
    {
        // fadeScreen.SetOpacity(0);
        // fadeScreen.gameObject.SetActive(false);
        fadeScreen = GameObject.Find("FadeblackImage").GetComponent<FadeScreen>();
    }

   /* void Update()
    {
      if (menuPressedPlay)
        {
            StartCoroutine(FadeBlackPlayScene());
            //StartCoroutine(fadeScreen.FadeToBlackScreen());
        }
    }*/

    public void PlayGame()
    {
        StartCoroutine(FadeBlackPlayScene());
        //SceneManager.LoadScene("Gameplay");
       
       // menuPressedPlay = true;
         
    }

    public void Instructions()
    {
        StartCoroutine(FadeBlackInstructions());
        //SceneManager.LoadScene("Gameplay");

        // menuPressedPlay = true;

    }

    public void Menu()
    {
        StartCoroutine(FadeBlackMenu());
        //SceneManager.LoadScene("Gameplay");

        // menuPressedPlay = true;

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


}
