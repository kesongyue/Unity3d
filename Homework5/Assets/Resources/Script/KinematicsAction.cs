using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicsAction : SSAction {

    public static float speed = 100.0f;
    private static GameObject role;
    private static Vector3 emitDirection;
    public static KinematicsAction GetSSAction(GameObject role_, Vector3 emitPosition_, Vector3 emitDirection_)
    {
        Fly(role_, emitPosition_, emitDirection_);
        KinematicsAction action = ScriptableObject.CreateInstance<KinematicsAction>();

        return action;
    }
    // Use this for initialization
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
       
       // transform.position += Vector3.forward * Time.deltaTime * speed;

        if (role != null && (role.GetComponent<GameModel>().is_outOfEdge() || role.GetComponent<GameModel>().getState() == false))
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }     
    }

    public static void Fly(GameObject role_, Vector3 emitPosition_, Vector3 emitDirection_)
    {
        role = role_;
        role_.transform.position = emitPosition_;
        role_.transform.Translate(emitDirection_ * Time.deltaTime * speed);
        role.GetComponent<Rigidbody>().velocity = emitDirection_*50;
    }
}
