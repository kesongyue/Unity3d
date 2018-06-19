using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Tank {
    public delegate void RecycleEnemy(GameObject enemy);
    //当enemy被摧毁时，通知工厂回收；
    public static event RecycleEnemy recycleEnemy;
    // player 的位置
    private Vector3 playerLocation;
    //游戏是否结束
    private bool gameover;
    private void Start()
    {
        playerLocation = GameDirector.getInstance().currentSceneController.getPlayer().transform.position;
        StartCoroutine(shoot());
    }

    void Update() {
        playerLocation = GameDirector.getInstance().currentSceneController.getPlayer().transform.position;
        gameover = GameDirector.getInstance().currentSceneController.getGameOver();
        if (!gameover)
        {
            if (getHP() <= 0 && recycleEnemy != null)
            {
                recycleEnemy(this.gameObject);
            }
            else
            {
                // 自动向player移动
                NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
                agent.SetDestination(playerLocation);
               // gameObject.transform.position = new Vector3(transform.position.x+0.01f,transform.position.y,transform.position.z);
            }
        }
        else
        {
            //游戏结束，停止寻路
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }
    // 协程实现每隔1s进行射击，开始喜欢协程了
    IEnumerator shoot()
    {
        while (!gameover)
        {
            for(float i =1;i> 0; i -= Time.deltaTime)
            {
                yield return 0;
            }
            if(Vector3.Distance(playerLocation,gameObject.transform.position) < 14)
            {
                shoot(TankType.ENEMY);
            }
        }
    }
}
