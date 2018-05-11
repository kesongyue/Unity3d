using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaCollide : MonoBehaviour
{
    public int sign;
    public delegate void CanFollow(int state,bool isEnter);
    public static event CanFollow canFollow;

    public delegate void AddScore();
    public static event AddScore addScore;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            canFollow(sign,true);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            canFollow(sign,false);
            addScore();
        }
    }
}