using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAdapter : MonoBehaviour , ActionManagerInterface{

    private FlyActionManager flyActionManager;
    private KinematicsActionManager kinematics;
    private SceneController sceneController;

    // Use this for initialization
    void Start () {
        sceneController = SceneController.getInstance();
        sceneController.flyActionManager = this;
        flyActionManager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        kinematics = gameObject.AddComponent<KinematicsActionManager>() as KinematicsActionManager;
	}
	
    public void Fly(GameObject role_, Vector3 emitPosition_, Vector3 emitDirection_)
    {
        if(sceneController.getIsPhysics())
        {
            flyActionManager.Fly(role_, emitPosition_, emitDirection_);
        }
        else
        {
            kinematics.Fly(role_, emitPosition_, emitDirection_);
        }
    }
	// Update is called once per frame
	void Update () {
        
    }
}
