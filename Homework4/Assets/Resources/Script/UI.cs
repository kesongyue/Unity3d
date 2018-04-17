using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    private SceneController sceneController = SceneController.getInstance();
	// Use this for initialization
	void Start () {
		
	}
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            sceneController.emitDisk();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.transform.tag == "Disk")
                {
                    sceneController.destroyDisk(hit.collider.gameObject);
                }
            }
        }
    }
    private void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 25;
        fontStyle.normal.textColor = new Color(0, 0, 0);
        
        if (GUI.Button(new Rect(0, 0, 100, 40), "Round1"))
        {
            sceneController.setGameState(GameState.ROUND1);
        }
        else if(GUI.Button(new Rect(0, 42, 100, 40), "Round2"))
        {
            sceneController.setGameState(GameState.ROUND2);
        }
        else if(GUI.Button(new Rect(0, 84, 100, 40), "End")){
            sceneController.setGameState(GameState.END);
        }
        GUI.Label(new Rect(Screen.width / 3, 0, 100, 50), "Round: " + sceneController.getRound(), fontStyle);
        GUI.Label(new Rect(Screen.width/3+150 , 0, 100, 50), "Score: " + sceneController.getScore(), fontStyle);
       
        if (sceneController.getGameState() == GameState.END)
        {
            if(GUI.Button(new Rect(Screen.width / 3, Screen.height / 3, 150, 80), "GameOver!\nEnter")){
                sceneController.setGameState(GameState.BEFORESTART);
            }
        }
    }
}
