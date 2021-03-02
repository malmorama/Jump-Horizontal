using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScreenText : MonoBehaviour
{
    public GameVariables gameVariables;
    public Text printDifficulty;
    private int oldDifficulty;
    public TextMeshProUGUI gameInstructions;
    bool runningGameInstructionsCoroutine;
    //private int oldDifficulty;


    // Start is called before the first frame update
    void Start()
    {
        //gameInstructions = GameObject.Find("Canvas").GetComponent<GameInstructions>();
        //gameVariables.score = 0f;
        gameInstructions.gameObject.SetActive(false);
        oldDifficulty = gameVariables.difficulty;
        StartCoroutine(PrintScreenText());
        runningGameInstructionsCoroutine = false;
        //StartCoroutine(GameLogic());


    }

    // Update is called once per frame
    
    void Update()
    {
       

    }

    public void PrintStartNewLevelText(int levelId, Collider2D collision)
    {

        if (levelId == 110 && runningGameInstructionsCoroutine == false)
        {
            StartCoroutine(GameInstructions("LEVEL 1"));
        }

        if (levelId == 121 && runningGameInstructionsCoroutine == false)
        {
            StartCoroutine(GameInstructions("LEVEL 2"));
        }

        if (levelId == 111 || levelId == 122)
        {
            runningGameInstructionsCoroutine = false;
        }

    }

    private IEnumerator PrintScreenText()
    {
        while (true)
        {
            printDifficulty.text = Mathf.RoundToInt(gameVariables.difficulty).ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator GameInstructions (string text)
    {
        runningGameInstructionsCoroutine = true;
        gameInstructions.text = text;
        Color color = gameInstructions.color;
        color.a = 1;
        gameInstructions.color = color;
        gameInstructions.gameObject.transform.localScale = new Vector2(0, 0);
        gameInstructions.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        float i = 0;
        while (gameInstructions.gameObject.transform.localScale.x < 1.5f)
        {
            i += 0.05f;
            gameInstructions.gameObject.transform.localScale = new Vector2(gameInstructions.gameObject.transform.localScale.x + i,
                gameInstructions.gameObject.transform.localScale.y + i);
            yield return new WaitForSeconds(0.05f);
        }
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(4);
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
