using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class UserInterface : MonoBehaviour {
	GameScenceController my;
	IUserActions action;

	// Use this for initialization
	void Start () {
		my = GameScenceController.getInstance ();
		action = GameScenceController.getInstance () as IUserActions;
	}
	
	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.tag=="Devil" || hit.transform.tag=="Priest"){
					my.clickWhichOne (hit.collider.gameObject);
					//Debug.Log ("P and D");
				}
				else if(hit.transform.tag == "Boat"){
					my.moveBoat ();
					//Debug.Log ("Boat");
				}
			}
		}
	}

	void OnGUI(){
		if(GUI.Button(new Rect(0,0,80,80),"Restart")){
			action.Restart();
		}
		if(my.state == State.WIN){
			GUI.Label(new Rect(Screen.width/6,Screen.height/6,80,80),"WIN!");
		}
		else if(my.state == State.LOSE){
			GUI.Label(new Rect(Screen.width/6,Screen.height/6,80,80),"LOSE!");
		}
	}
}
