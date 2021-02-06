using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameVariables gameVariables;
    //public Text gameInstructions;
    private int oldDifficulty;
    public TextMeshProUGUI gameInstructions;

    // Start is called before the first frame update
    void Start()
    {
        //gameInstructions = GameObject.Find("Canvas").GetComponent<GameInstructions>();
        //gameVariables.score = 0f;
        gameInstructions.gameObject.SetActive(false);
        oldDifficulty = gameVariables.difficulty;
        //StartCoroutine(GameInstructions("TAP LEFT OR RIGHT SIDE OF THE SCREEN"));
        //StartCoroutine(GameLogic());
    }

    // Update is called once per frame
    void Update()
    {
        gameVariables.difficulty = SetDifficulity(gameVariables.score);

    }

    //function to set difficulity based on score
    private int SetDifficulity(float score)
    {
        int difficulty = Mathf.RoundToInt(score / 200);
        return difficulty;
    }



    private IEnumerator GameInstructions (string text)
    {
        gameInstructions.text = text;
        Color color = gameInstructions.color;
        color.a = 1;
        gameInstructions.color = color;
        //yield return new WaitForSeconds(0.5f);
        gameInstructions.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        gameInstructions.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(2);
        gameInstructions.gameObject.SetActive(false);
        
    }

    private IEnumerator GameLogic()
    {
        while (true)
        {
            if (difficultyChange())
            {
                switch(gameVariables.difficulty)
                {
                    case 0:
                        //do nothing
                        break;
                    case 1:
                        //do nothing
                        break;
                    case 2:
                        //yield return StartCoroutine(GameInstructions("CATCH THE COINS FOR EXTRA POINTS!"));
                        break;
                    default:
                        break;
                }
            }
            yield return null;

        }
    }

   //if difficulty change return true GameLogic change in the loop 
   private bool difficultyChange()
    {
        if(oldDifficulty != gameVariables.difficulty)
        {
            oldDifficulty = gameVariables.difficulty;
            return true;
        }
        return false;
    }

}
