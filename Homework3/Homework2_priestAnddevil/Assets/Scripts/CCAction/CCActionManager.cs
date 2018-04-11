using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager,ISSActionCallback
{
    public GenGameObjects sceneController;
    public CCMoveToAction boat;
    public GetOnBoatAndShore role;
    public Dictionary<int, GetOnBoatAndShore> on_off = new Dictionary<int, GetOnBoatAndShore>();

    protected new void Start()
    {
        float speed = 5f;
        sceneController = (GenGameObjects)SSDirector.GetInstance().genGameObjects;
       // sceneController.actionManager = this;
    }
    protected new void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Devil" || hit.transform.tag == "Priest")
                {
                    role = GetOnBoatAndShore.GetSSAction(hit.collider.gameObject);
                    this.RunAction(hit.collider.gameObject, role, this);
                }
                else if (hit.transform.tag == "Boat")
                {
                    boat = CCMoveToAction.GetSSAction();
                    this.RunAction(hit.collider.gameObject, boat, this);
                }
            }
        }
        base.Update();
        //foreach(KeyValuePair<int,GameObject> obj in sceneController.getShoreLeft())
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
    }
}
