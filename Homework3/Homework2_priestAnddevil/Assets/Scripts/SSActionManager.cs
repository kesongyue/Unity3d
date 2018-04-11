using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {

    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();

	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
        foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();

        foreach(KeyValuePair<int,SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destory)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable == true)
            {
                ac.Update();
                ac.destory = true;
            }
        }

        foreach(int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
	}

    //初始化一个动作?
    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback callback)
    {
        action.gameobject = gameObject;
        action.transform = gameObject.transform;
        action.callback = callback;
        waitingAdd.Add(action);
        action.Start();
    }
}
