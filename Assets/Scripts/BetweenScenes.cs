using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetweenScenes : MonoBehaviour
{
    public Text score;
    public Text highScore;
    public Text coins;
    public Text jumps;
    public TextMeshProUGUI highScoreImg;

    public GameVariables gameVariables;
    //private int tempScore;


    // Start is called before the first frame update
    void Start()
    {
        //tempScore = 0;
        StartCoroutine(UpdateStats());
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdateStats()
    {
        //while(tempScore < gameVariables.score)
        //{
        //    tempScore = tempScore + 1;
        //    score.text = tempScore.ToString();
        //    yield return new WaitForSeconds(0.01f);
        //}

        score.text = gameVariables.score.ToString();
        highScore.text = gameVariables.highScore.ToString();
        coins.text = gameVariables.coin.ToString();

        //flash high score
        if (gameVariables.highScore <= gameVariables.score)
        {
             while(true)
            {
                yield return new WaitForSeconds(1f);
                highScore.gameObject.SetActive(false);
                highScoreImg.gameObject.SetActive(false);
                yield return new WaitForSeconds(1f);
                highScore.gameObject.SetActive(true);
                highScoreImg.gameObject.SetActive(true);
                yield return null;
            }
        }


        yield return null;
    }



}
