using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PatrolFactory:MonoBehaviour
{
    private List<GameObject> used = new List<GameObject>();    // the used patrol
    private Vector3[] PatrolPos = new Vector3[3];
    private bool isProduce = false;
    FirstController firstController;
    private void Start()
    {
        firstController = SSDirector.getInstance().currentScenceController as FirstController;
    }
    public List<GameObject> GetPatrols()
    {
        firstController = SSDirector.getInstance().currentScenceController as FirstController;
        if (!isProduce)
        {
            int index = 0;
            float[] posZ = { 3.75f, -3.75f };
            float[] posX = { 3.75f, -3.75f };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if(posX[j] > 0 && posZ[i] > 0)
                    {
                        continue;
                    }
                    PatrolPos[index++] = new Vector3(posX[j], 0, posZ[i]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                GameObject patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
                patrol.transform.parent = firstController.plane.transform;
                patrol.transform.position = PatrolPos[i];
                patrol.GetComponent<Patrol>().sign = i + 1;
                patrol.GetComponent<Patrol>().startPos = PatrolPos[i];
                used.Add(patrol);
            }
            isProduce = true;
        }     
        return used;
    }

    public void destoryFactory()
    {
        foreach(var a in used)
        {
            DestroyImmediate(a);
        }
        used = new List<GameObject>();
        isProduce = false;
    }
}
