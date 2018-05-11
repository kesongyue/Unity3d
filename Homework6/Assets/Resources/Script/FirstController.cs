using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum GameState { BEGIN, END };
public class FirstController: MonoBehaviour ,ISceneController,IUserAction
{
   
    private PatrolFactory patrolFactory;
    public GameObject player;
    public List<GameObject> patrols;
    public GameObject plane;
    public GameState gameState;
    public ScoreRecorder scoreRecorder;

    GameObject rush;
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        patrolFactory = Singleton<PatrolFactory>.Instance;
        LoadResources();
        gameState = GameState.BEGIN;
        PatrolCollide.catchSuccess += Gameover;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
    }
    public void LoadResources()
    {
        plane = Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        plane.transform.position = Vector3.zero;
        patrols = patrolFactory.GetPatrols();
        player= Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player.transform.position = new Vector3(1.8f, -0.35f,1.8f);
        //player.transform.Rotate(0, 180, 0);
    }
    public void ReStart()
    {     
        rush = player;
        rush.SetActive(false);
        player = null;
        player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player.transform.position = new Vector3(1.8f, -0.35f, 1.8f);
        patrolFactory.destoryFactory();
        patrols = patrolFactory.GetPatrols();
        DestroyImmediate(rush);

        gameState = GameState.BEGIN;
    }
    public void Gameover()
    {
        gameState = GameState.END;
    }

    public void PlayerMove(Vector3 pos) 
    {
            player.transform.Translate(pos);
    }
}

