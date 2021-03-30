using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameVariables", order = 1)]
public class GameVariables : ScriptableObject
{
    public float score;
    public float highScore;
    public int life;
    public int coin;
    public int difficulty;
    public float platformScrollSpeed;
    public float backgroundScrollSpeed;
    public bool maxTwoLife;
    public bool sendTelePortOnlyOnce;
    public bool respawnOnce;
    public int CurrentScoreToDifficulity;
    public int playerOnLevel;
    public float jumpForce;




}
