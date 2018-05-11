using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder : MonoBehaviour
{
    int Score = 0;
    void SetScore()
    {
        Score++;
    }
    // Use this for initialization  
    void Start()
    {
        AreaCollide.addScore += SetScore;//订阅事件  
    }

    public int GetScore()
    {
        return Score;
    }
}
