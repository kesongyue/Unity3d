using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    private float hp =500.0f;
    // 初始化
    public Tank()
    {
        hp = 500.0f;
    }

    public float getHP()
    {
        return hp;
    }

    public void setHP(float hp)
    {
        this.hp = hp;
    }

    public void beShooted()
    {
        hp -= 100;
    }
    
    public void shoot(TankType type)
    {
        GameObject bullet = Singleton<MyFactory>.Instance.getBullets(type);
        bullet.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward * 1.5f;
        bullet.transform.forward = transform.forward; //方向
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }
}
