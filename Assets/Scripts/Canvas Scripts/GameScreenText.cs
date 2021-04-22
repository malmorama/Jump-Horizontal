using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//print text like during game play like score, high score, life and also the intro text what level you ar one.
public class GameScreenText : MonoBehaviour
{
    [System.Serializable]
    public class GameInstructionsClass
    {
        
        public int levelId;
        public string instructionsText1;
        public string instructionsText2;


    }

    public List<GameInstructionsClass> gameInstructionsClass;

    public GameVariables gameVariables;
    public Text printDifficulty;
    public Text scoreText;
    public Text lifeText;
    public Text highScoreText;
    public Text coinText;
    private int oldDifficulty;
    public TextMeshProUGUI gameInstructions;
    public GameObject phone;
    bool runningGameInstructionsCoroutine;
    //private int oldDifficulty;
    //int oldLevelId;


    // Start is called before the first frame update
    void Start()
    {
        
        gameInstructions.gameObject.SetActive(false);
        oldDifficulty = gameVariables.difficulty;
        StartCoroutine(PrintScreenText());              //print the screen text difficulty, score, high score etc
        if (gameVariables.playerOnLevel == 1)           //only the level do we print the instructions
        {
            StartCoroutine(Level1GameInstruction());
        }
        runningGameInstructionsCoroutine = false;       //this is set to false every time we run load the scen and allow metod to print game instruction tex
        //StartCoroutine(GameLogic());


    }

    // Update is called once per frame
    
    void Update()
    {
       

    }

    //the method that prints text based on what level you are on, input from even on levelgenerator and print based on class in script
    public void PrintStartNewLevelText(int levelId, Collider2D collision)
    {
        
        foreach(GameInstructionsClass gs in gameInstructionsClass)
        { 

            if (gs.levelId == levelId && runningGameInstructionsCoroutine == false)
            {
          
                StartCoroutine(GameInstructionsRoutine(gs.instructionsText1, gs.instructionsText2, 1.5f));
                //oldLevelId = levelId;
            }
           
        }


    }

    //print only on start of game how to move player
    private IEnumerator Level1GameInstruction()
    {
        StartCoroutine(GameInstructions("TAP THE SCREEN!", 1.3f));
        yield return new WaitForSeconds(6);
        phone.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        phone.gameObject.SetActive(false);
        //yield return new WaitForSeconds(1f);
        //StartCoroutine(GameInstructions("LEVEL 1", 1.5f));
        runningGameInstructionsCoroutine = false;
    }



    // gameplay UI text
    private IEnumerator PrintScreenText()
    {
        while (true)
        {
            printDifficulty.text = Mathf.RoundToInt(gameVariables.difficulty).ToString();
            scoreText.text = Mathf.Round(gameVariables.score).ToString();
            lifeText.text = gameVariables.life.ToString();
            coinText.text = gameVariables.coin.ToString();
            UpdateHighscore();
            highScoreText.text = Mathf.Round(gameVariables.highScore).ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    //update highscore if score larger than HS
    private void UpdateHighscore()
    {
        if(gameVariables.score > gameVariables.highScore)
        {
            gameVariables.highScore = gameVariables.score;
        }
    }

   


    //coroutine that prints two messages after waiting for the first one
    private IEnumerator GameInstructionsRoutine (string text1, string text2, float textSize)    
    {
        runningGameInstructionsCoroutine = true;
        yield return StartCoroutine(GameInstructions(text1, textSize));
        Coroutine b = StartCoroutine(GameInstructions(text2, textSize));
        yield return b;
        //yield return new WaitForSeconds(30); //wait long enough so move from level 111 to 112
        //runningGameInstructionsCoroutine = false;
    }


    //the method that shows the name of each level as start a new level (scale up and fade out the text)
    private IEnumerator GameInstructions (string text, float textSize)
    {
        //runningGameInstructionsCoroutine = true;
        gameInstructions.text = text;
        Color color = gameInstructions.color;
        color.a = 1;
        gameInstructions.color = color;
        gameInstructions.gameObject.transform.localScale = new Vector2(0, 0);
        gameInstructions.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        float i = 0;
        while (gameInstructions.gameObject.transform.localScale.x < textSize)
        {
            i += 0.05f;
            gameInstructions.gameObject.transform.localScale = new Vector2(gameInstructions.gameObject.transform.localScale.x + i,
                gameInstructions.gameObject.transform.localScale.y + i);
            yield return new WaitForSeconds(0.05f);
        }
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(3);
        gameInstructions.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(2);
        gameInstructions.gameObject.SetActive(false);
        //runningGameInstructionsCoroutine = false;

    }

   

   //if difficulty change return true GameLogic change in the loop - not used
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
