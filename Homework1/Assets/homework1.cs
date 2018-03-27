using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homework1 : MonoBehaviour {

	private const int row=3,col=3;
	private int [,] state=new int[3,3];
	public Texture2D background,bird, pig;
	private int turn =1;
	// Use this for initialization
	void Start () {
		reset ();
	}

	void OnGUI(){
		GUIStyle backgroundStyle = new GUIStyle ();
		GUIStyle birdStyle = new GUIStyle ();
		GUIStyle fontStyle = new GUIStyle ();
		backgroundStyle.normal.background = background;
		fontStyle.fontSize = 30;
		fontStyle.normal.textColor = new Color (0, 0, 0);


		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "", backgroundStyle);
		GUI.Label (new Rect (300, 50, 100, 100), "My first Game",fontStyle);

		for(int i=0;i<row;i++){
			for(int j=0;j<col;j++){
				if(state[i,j]==1){
					GUI.Button (new Rect (250 + 80 * i, 100 + 80 * j, 80, 80), bird);
				}
				else if(state[i,j]==-1){
					GUI.Button (new Rect (250 + 80 * i, 100 + 80 * j, 80, 80), pig);
				}
				else if(GUI.Button(new Rect (250 + 80 * i, 100 + 80 * j, 80, 80),"")){
					state[i,j]=turn;
					turn=-turn;
				}
			}
		}
		if(GUI.Button(new Rect(325,350,90,50),"Reset")){
			reset();
		}

		int result = checkTheResult();
		if(result==1){
			GUI.Label(new Rect(50,200,100,50),"Bird wins!",fontStyle);
		}
		else if(result == -1){
			GUI.Label(new Rect(550,200,100,50),"Pig wins!",fontStyle);
		}
	}

	void reset(){
		for(int i=0;i<3;i++){
			for(int j=0;j<3;j++){
				state [i, j] = 0;
			}
		}
	}

	int checkTheResult(){
		for(int i=0;i<row;i++){
			if(state[i,0]!=0 && state[i,0]==state[i,1] && state[i,1]==state[i,2]){
				return state [i, 0];
			}
		}

		for(int j=0;j<col;j++){
			if(state[0,j]!=0 && (state[0,j]==state[1,j] && state[1,j]==state[2,j] )){
				return state [0, j];
			}
		}

		if(state[0,0]==state[1,1]&& state[2,2]==state[1,1] || state[1,1] == state[0,2] && state[1,1]==state[2,0]){
			return state [1, 1];
		}

		return 0;
	}
}
