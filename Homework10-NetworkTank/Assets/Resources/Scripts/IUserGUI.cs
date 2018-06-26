using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUserGUI : MonoBehaviour {
    IUserAction action;

	// Use this for initialization
	void Start () {
        action = GameDirector.getInstance().currentSceneController as IUserAction;
	}
	
	// Update is called once per frame
	void Update () {
        if (!action.getGameOver())
        {
            if (Input.GetKey(KeyCode.W))
            {
                action.moveForward();
            }

            if (Input.GetKey(KeyCode.S))
            {
                action.moveBackWard();
            }

           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                action.shoot();
            }
            //获取水平轴上的增量，目的在于控制玩家坦克的转向
            float offsetX = Input.GetAxis("Horizontal");
            action.turn(offsetX);
        }
    }

    void OnGUI()
    {
        //gameover时生成提示
        if (action.getGameOver())
        {
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.fontSize = 30;
            fontStyle.normal.textColor = new Color(0, 0, 0);
            GUI.Button(new Rect(Screen.width/2-50, Screen.height/2-50, 200, 50), "GameOver!");
        }
    }
}
