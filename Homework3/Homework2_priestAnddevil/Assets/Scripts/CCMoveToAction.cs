using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
 
    public GenGameObjects gameObjects = SSDirector.GetInstance().genGameObjects;
    public static CCMoveToAction GetSSAction()
    {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();

      
        return action;
    }
	// Use this for initialization
	public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
       
        if (gameObjects.boatCapcity() < 2)
        {
            if (gameObjects.boatSign == 1)
            {
                gameObjects.boat.transform.position = gameObjects.boatPositionRight;
            }
            else
            {
                gameObjects.boat.transform.position = gameObjects.boatPositionLeft;
            }
            gameObjects.boatSign = -gameObjects.boatSign;
        }
        gameObjects.check();
    }
}
