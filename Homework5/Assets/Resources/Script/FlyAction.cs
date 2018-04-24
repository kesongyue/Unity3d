using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAction : SSAction {

    private static GameObject role;
    public static FlyAction GetSSAction(GameObject role_, Vector3 emitPosition_, Vector3 emitDirection_)
    {
        Fly(role_,emitPosition_,emitDirection_);
        FlyAction action = ScriptableObject.CreateInstance<FlyAction>();
        
        return action;
    }
    // Use this for initialization
    public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		if(role != null && (role.GetComponent<GameModel>().is_outOfEdge() || role.GetComponent<GameModel>().getState() == false))
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
	}

    public static void Fly(GameObject role_,Vector3 emitPosition_,Vector3 emitDirection_)
    {
        role = role_;
        role_.transform.position = emitPosition_;
        role_.GetComponent<Rigidbody>().AddForce(3000 * emitDirection_);
    }
}
