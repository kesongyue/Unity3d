using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager
{
    public GenGameObjects sceneController;
    public CCMoveToAction moveToLeft, moveToRight;
    public Dictionary<int, CCOn_OffAction> on_off = new Dictionary<int, CCOn_OffAction>();

    protected new void Start()
    {
        float speed = 5f;
        sceneController = (GenGameObjects)SSDirector.GetInstance().genGameObjects;
        sceneController.actionManager = this;

        moveToLeft = CCMoveToAction.GetSSAction(sceneController.boatPositionLeft, speed);
        moveToRight = CCMoveToAction.GetSSAction(sceneController.boatPositionRight, speed);

        //foreach(KeyValuePair<int,GameObject> obj in sceneController.getShoreLeft())
    }
}
