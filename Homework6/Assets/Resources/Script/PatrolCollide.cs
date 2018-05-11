using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class PatrolCollide:MonoBehaviour
{
    public delegate void CatchSuccess();
    public static event CatchSuccess catchSuccess;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            catchSuccess();        
        }

    }
}

