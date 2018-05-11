using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Patrol :MonoBehaviour
{
    public enum PatrolState { PATROL,FOLLOW};
    public int sign;        //the patrol in which area
    public bool isFollowPlayer = false;
    public GameObject player=null;       //the player
    public Vector3 startPos,nextPos;
    private float minPosX,minPosZ;  // the range of this patrol can move;
    private bool isMoving = false;
    private float distance;
    private float speed = 1.2f;
    PatrolState state = PatrolState.PATROL;
    private void Start()
    {
        minPosX = startPos.x - 2.5f;
        minPosZ = startPos.z - 2.5f;
        isMoving = false;
        AreaCollide.canFollow += changeStateToFollow;
    }

    public void FixedUpdate()
    {
        if((SSDirector.getInstance().currentScenceController as FirstController).gameState == GameState.END)
        {
            return;
        }
        if(state == PatrolState.PATROL)
        {
            GoPatrol();
        }
        else if(state == PatrolState.FOLLOW)
        {
            Follow();
        }
    }
    public void GoPatrol()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, nextPos, speed * Time.deltaTime);
            distance = Vector3.Distance(this.transform.position, nextPos);
            if(distance < 0.5)
            {
                isMoving = false;
            }
            return;
        }
        float posX = Random.Range(0f, 5f);
        float posZ = Random.Range(0f, 5f);
        nextPos = new Vector3(minPosX+posX, 0, minPosZ+posZ);
        isMoving = true;    
    }

    public void Follow()
    {
        if(player != null)
        {
            nextPos = player.transform.position;
            transform.position = Vector3.MoveTowards(this.transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    public void changeStateToFollow(int sign_,bool isEnter)
    {
        if(sign == sign_ )
        {
            if (isEnter)
            {
                state = PatrolState.FOLLOW;
                player = (SSDirector.getInstance().currentScenceController as FirstController).player;
                isFollowPlayer = true;
            }           
            else
            {
                isFollowPlayer = false;
                state = PatrolState.PATROL;
                player = null;
                isMoving = false;
            }
        }
        
    }
}

