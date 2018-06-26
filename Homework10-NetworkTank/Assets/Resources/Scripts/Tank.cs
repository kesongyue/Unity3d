using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tank : NetworkBehaviour
{
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

    [Command]
    public void CmdFire(TankType type)
    {
        GameObject bullet = Singleton<MyFactory>.Instance.getBullets(type);
        
        bullet.transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z) + gameObject.transform.forward * 1.5f;
        bullet.transform.forward = gameObject.transform.forward; //方向
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 20, ForceMode.Impulse);

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
