using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCOn_OffAction : SSAction {

    private GenGameObjects firstSceneControl;
   // enum po
	// Use this for initialization
	public override void Start () {
        firstSceneControl = (GenGameObjects)SSDirector.GetInstance().genGameObjects;
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public static CCOn_OffAction GetSSAction()
    {
        CCOn_OffAction action = ScriptableObject.CreateInstance<CCOn_OffAction>();
        return action;
    }
}
