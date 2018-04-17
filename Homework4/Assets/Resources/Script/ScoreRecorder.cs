using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder {

    private int score;
    public ScoreRecorder()
    {
        score = 0;
    }
    public void addScore(int add)
    {
        score+=add;
    }
    public void subScore(int sub)
    {
        score=score-sub;
    }
    public int getScore()
    {
        return score;
    }
    public void resetScore()
    {
        score = 0;
    }
}
