using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour{

    static int count = 0;
    public Color diskColor;
    private Vector3 emitPosition;
    private Vector3 emitDirection;
    private float emitSpeed;
    private bool is_used;
    private int diskScale;
    private int id;
   // public GameObject diskTemplate;
    public GameModel()
    {
        id = 0;
        count++;
    }
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getID()
    {
        return id;
    }
    public void setColor(Color diskColor)
    {
        this.GetComponent<MeshRenderer>().material.color = diskColor;
    }
    public void setEmitPosition(Vector3 emitPosition_)
    {
        emitPosition = emitPosition_;
        this.transform.position = emitPosition_;
    }
    public void setEmitDirection(Vector3 emitDirection_)
    {
        emitDirection = emitDirection_;
        gameObject.GetComponent<Rigidbody>().AddForce(3000 * emitDirection_);
    }

    public void setState(bool state)
    {
        is_used = state;
        gameObject.SetActive(is_used);
    }
    public bool getState()
    {
        return is_used;
    }
    public bool is_outOfEdge()
    {
        if(transform.position.z>150 || transform.position.z<-150 
            || transform.position.x < -50 || transform.position.x >50
            || transform.position.y>10 || transform.position.y < -20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
