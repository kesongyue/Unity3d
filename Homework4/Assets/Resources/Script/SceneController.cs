using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { BEFORESTART,ROUND1,ROUND2,END};
public class SceneController : MonoBehaviour{

    private static SceneController _instance;
    private DiskFactory diskFactory= DiskFactory.getInstance();
    private GameObject disk;
    private GameState gamestate = GameState.BEFORESTART;
    private ScoreRecorder scoreRecorder = new ScoreRecorder();
    private int cout=0;

    private float time = 0;
    public static SceneController getInstance()
    {
        if(_instance == null)
        {
            _instance = new SceneController();
        }
        return _instance;
    }
    
	void Awake () {
        _instance = this;
        
        _instance.gamestate = GameState.END;

    }
	
	// Update is called once per frame
	void Update () {
        int count =diskFactory.updateList();
        scoreRecorder.subScore(count);
    }

    public void emitDisk()
    {
        if (gamestate == GameState.BEFORESTART)
        {

        }
        else if (gamestate == GameState.ROUND1)
        {
            disk = diskFactory.getDiskObject();
            float x = Random.Range(0.1f, 1);
            float y = Random.Range(-1, 1)/10;
            float z = Random.Range(0.1f, 1);
            
            disk.GetComponent<GameModel>().setColor(selectColor());
            disk.GetComponent<GameModel>().setEmitPosition(new Vector3(-8, 0, 5));
            disk.GetComponent<GameModel>().setEmitDirection(new Vector3(x, y, z));
        }
        else if(gamestate == GameState.ROUND2)
        {
            disk = diskFactory.getDiskObject();
            float x = Random.Range(-0.8f, 1);
            float y = Random.Range(-1, 1) / 10;
            float z = Random.Range(0.1f, 1);
            disk.GetComponent<GameModel>().setColor(selectColor());
            disk.GetComponent<GameModel>().setEmitPosition(new Vector3(-8, 0, 5));
            disk.GetComponent<GameModel>().setEmitDirection(new Vector3(x, y, z));
        }
        else if (gamestate == GameState.END)
        {
            diskFactory.clear();
            scoreRecorder.resetScore();
            //print(cout);
        }
    }

    public void destroyDisk(GameObject obj)
    {
        if(gamestate == GameState.ROUND1)
        {
            diskFactory.removeDiskObject(obj);
            scoreRecorder.addScore(1);
        } 
    }
    public void setGameState(GameState state)
    {
        gamestate = state;
    }
    public GameState getGameState()
    {
        return gamestate;
    }
    public int getScore()
    {
        return scoreRecorder.getScore();
    }
    private Color selectColor()
    {
        int randomNumber = Random.Range(0, 5);
        Color color=Color.green;
        switch (randomNumber)
        {
            case 0:color = Color.red;
                break;
            case 1:color = Color.blue;
                break;
            case 2: color = Color.green;
                break;
            case 3: color = Color.yellow;
                break;
            case 4: color = Color.grey;
                break;
        }
        return color;
    }
    public int getRound()
    {
        int round=0;
        switch (gamestate)
        {
            case GameState.ROUND1:
                round = 1;
                break;
            case GameState.ROUND2:
                round = 2;
                break;
        }
        return round;
    }

 
}
