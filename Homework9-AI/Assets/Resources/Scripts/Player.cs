using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank{
    // player被摧毁时发布信息；
    public delegate void DestroyPlayer();
    public static event DestroyPlayer destroyEvent;
    void Start () {
        setHP(500);
	}
	
	// Update is called once per frame
	void Update () {
		if(getHP() <= 0)    // Tank is destoryed
        {
            this.gameObject.SetActive(false);
            destroyEvent();
        }
	}

    //向前移动
    public void moveForward()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 30;
    }
    //向后移动
    public void moveBackWard()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -30;
    }

    //通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
    public void turn(float offsetX)
    {
        float x = gameObject.transform.localEulerAngles.x;
        float y = gameObject.transform.localEulerAngles.y + offsetX*2;
        gameObject.transform.localEulerAngles = new Vector3(x, y, 0);
    }
}
