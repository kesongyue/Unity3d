using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankType { PLAYER , ENEMY};
public class MyFactory : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject bullet;
    public ParticleSystem explosion;

    private List<GameObject> usingTanks;
    private List<GameObject> freeTanks;
    private List<GameObject> usingBullets;
    private List<GameObject> freeBullets;
    private GameObject role;
    private List<ParticleSystem> particles;

    private void Awake()
    {
        usingTanks = new List<GameObject>();
        freeTanks = new List<GameObject>();
        usingBullets = new List<GameObject>();
        freeBullets = new List<GameObject>();
        particles = new List<ParticleSystem>();

        role = GameObject.Instantiate<GameObject>(player) as GameObject;
        role.SetActive(true);
        role.transform.position = Vector3.zero;
    }
    // Use this for initialization
    void Start () {
        Enemy.recycleEnemy += recycleEnemy;
    }
	
	// Update is called once per frame
	public GameObject getPlayer()
    {      
        return role;
    }

    public GameObject getEnemys()
    {
        GameObject newTank = null;
        if (freeTanks.Count <= 0)
        {
            newTank = GameObject.Instantiate<GameObject>(enemy) as GameObject;
            usingTanks.Add(newTank);
            newTank.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        }
        else
        {
            newTank = freeTanks[0];
            freeTanks.RemoveAt(0);
            usingTanks.Add(newTank);
        }
        newTank.SetActive(true);
        return newTank;
    }

    public GameObject getBullets(TankType type)
    {
        GameObject newBullet;
        if(freeBullets.Count <= 0)
        {
            newBullet = GameObject.Instantiate<GameObject>(bullet) as GameObject;
            usingBullets.Add(newBullet);
            newBullet.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        }
        else
        {
            newBullet = freeBullets[0];
            freeBullets.RemoveAt(0);
            usingBullets.Add(newBullet);
        }
        newBullet.GetComponent<Bullet>().setTankType(type);
        newBullet.SetActive(true);
        return newBullet;
    }

    public ParticleSystem getParticleSystem()
    {
        foreach(var particle in particles)
        {
            if (!particle.isPlaying)
            {
                return particle;
            }
        }
        ParticleSystem newPS = GameObject.Instantiate<ParticleSystem>(explosion);
        particles.Add(newPS);
        return newPS;
    }

    public void recycleEnemy(GameObject enemyTank)
    {
        usingTanks.Remove(enemyTank);
        freeTanks.Add(enemyTank);
        enemyTank.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemyTank.SetActive(false);
    }

    public void recycleBullet(GameObject Bullet)
    {
        usingBullets.Remove(Bullet);
        freeBullets.Add(Bullet);
        Bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Bullet.SetActive(false);
    }
}
