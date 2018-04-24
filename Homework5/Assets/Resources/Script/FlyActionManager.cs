using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager,ISSActionCallback {

    private FlyAction fly;
    public SceneController sceneController;
    
	// Use this for initialization
	protected  new void Start () {
        sceneController = SceneController.getInstance();
       // sceneController.flyActionManager = this;
	}
	
	// Update is called once per frame
	public void Fly(GameObject role_, Vector3 emitPosition_, Vector3 emitDirection_)
    {
        fly = FlyAction.GetSSAction(role_, emitPosition_, emitDirection_);
        this.RunAction(role_, fly, this);
    }
   public void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null)
    { }
}
