using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour,IUserAction{
    public GameObject player;
    private int enemyCount = 6;
    private bool gameOver = false;
    private GameObject[] enemys;
    private MyFactory myFactory;
    public GameDirector director;
    private void Awake()
    {
        director = GameDirector.getInstance();
        director.currentSceneController = this;
        enemys = new GameObject[enemyCount];
        gameOver = false;
        myFactory = Singleton<MyFactory>.Instance;
       
    }
   
    void Start () {
        player = myFactory.getPlayer();
        for (int i = 0; i < enemyCount; i++)
        {
            enemys[i]=myFactory.getEnemys();
        }
        Player.destroyEvent += setGameOver;
    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(player.transform.position.x, 18, player.transform.position.z);
    }

    //返回玩家坦克的位置
    public GameObject getPlayer()
    {
        return player;
    }

    //返回游戏状态
    public bool getGameOver()
    {
        return gameOver;
    }

    //设置游戏结束
    public void setGameOver()
    {
        gameOver = true;
    }

    public void moveForward()
    {
        player.GetComponent<Player>().moveForward();
    }
    public void moveBackWard()
    {
        player.GetComponent<Player>().moveBackWard();
    }

    //通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
    public void turn(float offsetX)
    {
        player.GetComponent<Player>().turn(offsetX);
    }

    public void shoot()
    {
        player.GetComponent<Player>().shoot(TankType.PLAYER);
    }

}
