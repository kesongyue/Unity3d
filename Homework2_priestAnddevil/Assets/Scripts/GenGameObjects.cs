using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class GenGameObjects : MonoBehaviour {

	const int priest_num = 3;
	GameScenceController my;
	Stack<GameObject> priests_start = new Stack<GameObject>();
	Stack<GameObject> priests_end = new Stack<GameObject>();
	Stack<GameObject> devils_start = new Stack<GameObject>();
	Stack<GameObject> devils_end = new Stack<GameObject>();

	Vector3 shoreStartPosition = new Vector3(0,0,-7);
	Vector3 shoreEndPosition =new Vector3(0,0,6);
	Vector3 boatStartPosition =new Vector3(0,0,-3.2f);
	Vector3 boatEndPosition = new Vector3(0,0,2.2f);

	GameObject[] boats =new GameObject[2];
	GameObject boat_object;

	public float speed=2f;

	float gap =1.2f;
	Vector3 priestStartPosition = new Vector3(0,1.2f,-8.2f);
	Vector3 priestEndPosition = new Vector3(0,1.2f,5.2f);
	Vector3 devilStartPosition = new Vector3(0,1.2f,-7.5f);
	Vector3 devilEndPosition = new Vector3(0,1.2f,4.5f);
	// Use this for initialization
	void Start () {
		
		my = GameScenceController.getInstance ();
		my.setGenGameObject (this);
		loadSource ();
	}
	
	// Update is called once per frame
	void Update () {
		setCharacterPosition (priests_start, priestStartPosition);
		setCharacterPosition (priests_end, priestEndPosition);
		setCharacterPosition (devils_start, devilStartPosition);
		setCharacterPosition (devils_end, devilEndPosition);

		if (my.state == State.BOATMOVINGSTOE) {
			boat_object.transform.position = Vector3.MoveTowards (boat_object.transform.position, boatEndPosition, speed * Time.deltaTime);
			if (boat_object.transform.position == boatEndPosition) {
				if (boat_object.transform.position == boatEndPosition) {
					my.state = State.BOATONEND;
				}
			}
		} else if (my.state == State.BOATMOVINGETOS) {
			boat_object.transform.position = Vector3.MoveTowards (boat_object.transform.position, boatStartPosition, speed * Time.deltaTime);
			if (boat_object.transform.position == boatStartPosition) {
				my.state = State.BOATONSTART;
			}
		} else
			check ();
	}

	void loadSource(){
		Instantiate (Resources.Load ("Prefabs/Coast"), shoreStartPosition, Quaternion.identity);
		Instantiate (Resources.Load ("Prefabs/Coast"), shoreEndPosition, Quaternion.identity);

		boat_object = Instantiate (Resources.Load ("Prefabs/Boat"), boatStartPosition, Quaternion.identity) as GameObject;
		for(int i=0;i<priest_num;i++){
			// this can be improved!
			priests_start.Push (Instantiate (Resources.Load ("Prefabs/Priest1")) as GameObject);
			devils_start.Push (Instantiate (Resources.Load ("Prefabs/Devil1")) as GameObject);
		}
			
	}

	int boatCapcity(){
		int capcity = 0;
		for(int i=0;i<2;i++){
			if(boats[i] == null){
				capcity++;
			}
		}
		return capcity;
	}

	void getOnBoat(GameObject person){
		
		if(boatCapcity()!=0){
			//Debug.Log ("weizhi:" + boatCapcity ());
			person.transform.parent = boat_object.transform;

			if(boats[0]==null){
				boats [0] = person;
				person.transform.localPosition = new Vector3 (0, 0.8f, -0.3f);


			}
			else{
				boats [1] = person;
				person.transform.localPosition = new Vector3 (0, 0.8f, 0.3f);

			}
		}
		else{
			Debug.Log ("full");
		}
	}

	public void getOffBoat(int side){
		if (side < 0 || side > 1)
			return;
		if(boats[side] != null){
			boats [side].transform.parent = null;
			if(my.state == State.BOATONEND){
				if(boats[side].tag == "Priest"){
					priests_end.Push (boats [side]);
				}
				else if(boats[side].tag == "Devil"){
					devils_end.Push (boats [side]);
				}
			}
			else if(my.state == State.BOATONSTART){
				if(boats[side].tag == "Priest"){
					priests_start.Push (boats [side]);
				}
				else if(boats[side].tag == "Devil"){
					devils_start.Push (boats [side]);
				}
			}
			boats [side] = null;
		}
	}

	public void moveBoat(){
		if(boatCapcity() !=2 ){
			if(my.state == State.BOATONSTART){
				my.state = State.BOATMOVINGSTOE;
			}
			else if(my.state == State.BOATONEND){
				my.state = State.BOATMOVINGETOS;
			}
		}
	}

	public void clickWhichOne(GameObject obj){
		if(obj == boats[0]){
			getOffBoat (0);
		}
		else if(obj == boats[1]){
			getOffBoat (1);
		}
		else{
			//getOnBoat (obj);
			if(my.state == State.BOATONSTART){
				if(obj.tag=="Priest" && priests_start.Count >0){
					getOnBoat (priests_start.Pop ());
				}
				else if(obj.tag== "Devil" && devils_start.Count >0){
					getOnBoat (devils_start.Pop ());
				}
			}
			else if(my.state == State.BOATONEND){
				if(obj.tag == "Priest" && priests_end.Count >0){
					getOnBoat (priests_end.Pop ());
				}
				else if(obj.tag=="Devil" && devils_end.Count >0){
					getOnBoat (devils_end.Pop ());
				}
			}
		}
	}

	public void priestStartOnBoat(){
		if(priests_start.Count >0 && boatCapcity()>0 && my.state == State.BOATONSTART){
			getOnBoat (priests_start.Pop ());
		}
	}

	public void priestEndOnBoat (){
		if(boatCapcity()>0 && priests_end.Count >0 && my.state == State.BOATONEND){
			getOnBoat (priests_end.Pop ());
		}
	}
	public void devilStartOnBoat (){
		if(devils_start.Count >0 && boatCapcity()>0 && my.state == State.BOATONSTART){
			getOnBoat (devils_start.Pop ());
		}
	}

	public void devilEndBoat (){
		if(devils_end.Count >0 && boatCapcity()>0 && my.state == State.BOATONEND){
			getOnBoat (devils_end.Pop ());
		}
	}

	void setCharacterPosition(Stack<GameObject> stack,Vector3 position){
		GameObject[] arr = stack.ToArray ();
		for(int i=0;i<stack.Count;i++){
			arr [i].transform.position = new Vector3 (position.x, position.y, position.z + gap * i);
		}
	}

	void check(){
		int priestsOnBoat = 0, devilsOnBoat = 0;
		int priestStart = 0, devilStart = 0, priestEnd = 0, devilEnd = 0;

		if(priests_end.Count == priest_num && devils_end.Count==priest_num){
			my.state = State.WIN;
			return;
		}

		for(int i=0;i<2;i++){
			if (boats [i] != null && boats [i].tag == "Priest")
				priestsOnBoat++;
			else if (boats [i] != null && boats [i].tag == "Devil")
				devilsOnBoat++;
		}

		if(my.state == State.BOATONSTART){
			priestStart = priests_start.Count + priestsOnBoat;
			devilStart = devils_start.Count + devilsOnBoat;
			priestEnd = priests_end.Count;
			devilEnd = devils_end.Count;
		}
		else if(my.state == State.BOATONEND){
			priestStart = priests_start.Count;
			devilStart = devils_start.Count ;
			priestEnd = priests_end.Count + priestsOnBoat;
			devilEnd = devils_end.Count + devilsOnBoat;
		}
		if((priestStart !=0 && priestStart < devilStart) || (priestEnd !=0 && priestEnd < devilEnd)){
			my.state = State.LOSE;
		}
	}
}
