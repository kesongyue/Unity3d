using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

namespace MyGame{
	public enum State{ BOATONSTART ,BOATMOVINGSTOE,BOATMOVINGETOS,BOATONEND,WIN,LOSE};

	public interface IUserActions{
		void priestStartOnBoat ();
		void priestEndOnBoat ();
		void devilStartOnBoat ();
		void devilEndBoat ();
		void moveBoat ();
		void offBoatLeft ();
		void offBoatRight ();
		void Restart ();
	}

	public class GameScenceController: System.Object,IUserActions{
		private static GameScenceController _instance;
		private SSDirector _ssdirector;
		private GenGameObjects _genGameObject;
		public State state = State.BOATONSTART;

		public static GameScenceController getInstance(){
			if(_instance == null){
				_instance = new GameScenceController ();
			}
			return _instance;
		}

		public SSDirector getSSDirector(){
			return _ssdirector;
		}

		internal void  setSSDirector(SSDirector ssd){
			if(_ssdirector == null){
				_ssdirector = ssd;
			}
		}

		public GenGameObjects getGenGameObject(){
			return _genGameObject;
		}

		internal void setGenGameObject(GenGameObjects gen){
			if(_genGameObject == null){
				_genGameObject = gen;
			}
		}

		public void priestStartOnBoat(){
			_genGameObject.priestStartOnBoat ();
		}
			
		public void priestEndOnBoat (){
			_genGameObject.priestEndOnBoat ();
		}

		public void devilStartOnBoat (){
			_genGameObject.devilStartOnBoat ();
		}

		public void devilEndBoat (){
			_genGameObject.devilEndBoat ();
		}

		public void moveBoat (){
			_genGameObject.moveBoat ();
		}

		public void offBoatLeft (){
			_genGameObject.getOffBoat (0);
		}

		public void offBoatRight (){
			_genGameObject.getOffBoat (1);
		}

		public void clickWhichOne(GameObject obj){
			_genGameObject.clickWhichOne (obj);
		}

		public void Restart (){
			Application.LoadLevel (Application.loadedLevelName);
			state = State.BOATONSTART;
		}
	}

}
public class SSDirector : MonoBehaviour {

	public string gameName;
	// Use this for initialization
	void Start () {
		GameScenceController my = GameScenceController.getInstance ();
		my.setSSDirector (this);
		gameName = "Priests and Devils!";
	}
	

}
		
