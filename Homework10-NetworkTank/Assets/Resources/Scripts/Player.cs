using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank{
    // player被摧毁时发布信息；
    /* public delegate void DestroyPlayer();
     public static event DestroyPlayer destroyEvent;*/

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
    void Start () {
        setHP(500);
       
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, 18, gameObject.transform.position.z);
       /* if (getHP() <= 0)    // Tank is destoryed
        {
            this.gameObject.SetActive(false);
            //destroyEvent();
        }*/

        if (Input.GetKey(KeyCode.W))
        {
            moveForward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveBackWard();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            CmdFire(TankType.PLAYER);
        }
        //获取水平轴上的增量，目的在于控制玩家坦克的转向
        float offsetX = Input.GetAxis("Horizontal");
        turn(offsetX);
    }

    //向前移动
    public void moveForward()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 25;
    }
    //向后移动
    public void moveBackWard()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -25;
    }

    //通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
    public void turn(float offsetX)
    {
        float x = gameObject.transform.localEulerAngles.x;
        float y = gameObject.transform.localEulerAngles.y + offsetX*2;
        gameObject.transform.localEulerAngles = new Vector3(x, y, 0);
    }
}
