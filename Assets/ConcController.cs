using UnityEngine;
using System.Collections;

public class ConcController : MonoBehaviour {
	GameObject activeGuy;
	MonoBehaviour guyFPSController;
	GameObject guyCam;
	SimpleSmoothMouseLook guyMouseLook;
	Camera guyCamComponent;

	void Start () {


	}

	void retrieveGuyComponents(string g){
		activeGuy = GameObject.Find (g);
		guyFPSController = activeGuy.GetComponent("CharacterMotor") as MonoBehaviour;
		
		string guyCamString = g + "Cam";
		guyCam = GameObject.Find (guyCamString);
		guyMouseLook = guyCam.GetComponent<SimpleSmoothMouseLook>();;
		guyCamComponent = guyCam.GetComponent<Camera> ();
	}

	void enableGuy(string g){
		retrieveGuyComponents(g);

		guyFPSController.enabled = true;
		guyMouseLook.enabled = true;
		guyCamComponent.enabled = true;
	}

	void disableGuy(string g){
		retrieveGuyComponents(g);
		
		guyFPSController.enabled = false;
		guyMouseLook.enabled = false;
		guyCamComponent.enabled = false;
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Alpha1)) {
			enableGuy("Guy1");
			disableGuy("Guy2");
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			enableGuy("Guy2");
			disableGuy("Guy1");
		}

	}
}
