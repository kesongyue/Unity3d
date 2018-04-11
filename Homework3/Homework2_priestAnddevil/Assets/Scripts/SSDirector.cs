using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SSDirector : System.Object
{
    private static SSDirector _instance;
    public GenGameObjects genGameObjects;
    public SceneController CurrentScenceController { get; set; }
    public static SSDirector GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }
    private SSDirector() { }
}
