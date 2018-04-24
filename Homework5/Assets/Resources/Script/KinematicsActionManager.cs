using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicsActionManager : SSActionManager ,ISSActionCallback{

    private KinematicsAction fly;
    public SceneController sceneController;

    // Use this for initialization
    protected new void Start()
    {
        sceneController = SceneController.getInstance();
       // sceneController.flyActionManager = this;
    }
    protected new void Update()
    {
        if(fly!=null)
            fly.Update();
    }

    // Update is called once per frame
    public void Fly(GameObject role_, Vector3 emitPosition_, Vector3 emitDirection_)
    {
        fly = KinematicsAction.GetSSAction(role_, emitPosition_, emitDirection_);
        this.RunAction(role_, fly, this);
    }
    public void SSActionEvent(SSAction source,
         SSActionEventType events = SSActionEventType.Competeted,
         int intParam = 0,
         string strParam = null,
         Object objectParam = null)
    { }
}
