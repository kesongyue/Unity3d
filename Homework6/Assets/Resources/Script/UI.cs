using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void ReStart();//重新开始  
}
public class UI : MonoBehaviour
{
    public GameObject player;
    public IUserAction action;
    public FirstController sceneController;
    private float speed = 3f;
    void Start()
    {
        sceneController = SSDirector.getInstance().currentScenceController as FirstController;
        action = (FirstController)SSDirector.getInstance().currentScenceController as IUserAction;
        player = sceneController.player;
    }
    void OnGUI()
    {
        GUIStyle fontstyle1 = new GUIStyle();
        fontstyle1.fontSize = 50;
        fontstyle1.normal.textColor = new Color(255, 255, 255);
        if (GUI.Button(new Rect(0, 0, 120, 40), "RESTART"))
        {
            action.ReStart();
        }
        if(sceneController.gameState == GameState.END)
        {
            GUI.Label(new Rect(Screen.width / 2-100, Screen.height/2-100, 200, 80),"Game Over!", fontstyle1);
        }
        GUI.Label(new Rect(Screen.width / 2, 0, 200, 80), "Score: " + sceneController.scoreRecorder.GetScore().ToString(), fontstyle1);
    }

    private void Update()
    {
        if(sceneController.gameState == GameState.BEGIN)
        {
            float translationX = Input.GetAxis("Horizontal") * speed;
            float translationZ = Input.GetAxis("Vertical") * speed;
            translationX *= Time.deltaTime;
            translationZ *= Time.deltaTime;
            sceneController.PlayerMove(new Vector3(translationX,0,translationZ));
        }
    }
}