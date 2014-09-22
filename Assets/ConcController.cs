using UnityEngine;
using System.Collections;

public class ConcController : MonoBehaviour {
	GameObject activeGuy;
	MonoBehaviour guyFPSController;
	MonoBehaviour guyCharacterMotor;
	GameObject guyCam;
	SimpleSmoothMouseLook guyMouseLook;
	Camera guyCamComponent;
	int numGuys = 2;
	GameObject[] guys;

	void Start () {
		guys = GameObject.FindGameObjectsWithTag("Guy");
		setActiveGuy (1);
	}

	void setActiveGuy(int i){
		string guyString = "Guy" + i;

		//enable Guyi, and disable every other guy
		for (int h = 0; h < guys.Length; h++) {
			if(guys[h].name.Equals(guyString)){
				enableGuy(guys[h].name);
			}else{
				disableGuy(guys[h].name);
			}
		}
	}

	void retrieveGuyComponents(string g){
		//get active guy
		activeGuy = GameObject.Find (g);

		//get active guy's movement scripts
		guyFPSController = activeGuy.GetComponent("FPSInputController") as MonoBehaviour;
		guyCharacterMotor = activeGuy.GetComponent("CharacterMotor") as MonoBehaviour;

		//get active guy's camera scripts
		string guyCamString = g + "Cam";
		guyCam = GameObject.Find (guyCamString);
		guyMouseLook = guyCam.GetComponent<SimpleSmoothMouseLook>();;
		guyCamComponent = guyCam.GetComponent<Camera> ();
	}

	void setGuyComponents(bool b){
		guyFPSController.enabled = b;
		guyCharacterMotor.enabled = b;
		guyMouseLook.enabled = b;
		guyCamComponent.enabled = b;
	}

	void enableGuy(string g){
		retrieveGuyComponents(g);
		setGuyComponents (true);
	}

	void disableGuy(string g){
		retrieveGuyComponents(g);
		setGuyComponents (false);
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Alpha1)) {
			setActiveGuy (1);
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			setActiveGuy (2);
		}

	}
}
