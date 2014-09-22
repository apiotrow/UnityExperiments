using UnityEngine;
using System.Collections;

public class ConcController : MonoBehaviour {
	GameObject activeGuy;
	public string activeGuyString;
	GameObject activeGuyCam;

	MonoBehaviour guyFPSController;
	MonoBehaviour guyCharacterMotor;
	GameObject guyCam;
	SimpleSmoothMouseLook guyMouseLook;
	Camera guyCamComponent;
	int numGuys = 2;
	GameObject[] guys;

	GameObject concBullet;

	void Start () {
		guys = GameObject.FindGameObjectsWithTag("Guy");
		setActiveGuy ("Guy1");


	}

	public void setActiveGuy(string guyString){
		//enable Guyi, and disable every other guy

		activeGuyString = guyString;
		activeGuy = GameObject.Find (guyString);
		string guyCamStrings = guyString + "Cam";
		activeGuyCam = GameObject.Find (guyCamStrings);

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
		//activeGuy = GameObject.Find (g);

		//get active guy's movement scripts
		guyFPSController = GameObject.Find (g).GetComponent("FPSInputController") as MonoBehaviour;
		guyCharacterMotor = GameObject.Find (g).GetComponent("CharacterMotor") as MonoBehaviour;

		//get active guy's camera scripts
		string guyCamString = g + "Cam";
		guyCam = GameObject.Find (guyCamString);
		guyMouseLook = guyCam.GetComponent<SimpleSmoothMouseLook>();
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
			setActiveGuy ("Guy1");
		}else if (Input.GetKey (KeyCode.Alpha2)) {
			setActiveGuy ("Guy2");
		}


		if (Input.GetKeyDown (KeyCode.R)) {
			concBullet = (GameObject)Instantiate(Resources.Load("ConcBullet"));
			Vector3 guyPos = activeGuy.transform.position;
			concBullet.transform.position = new Vector3(guyPos.x, guyPos.y + 0.5f, guyPos.z);
//			concBullet.transform.forward = activeGuy.transform.forward;
			concBullet.transform.forward = activeGuyCam.transform.forward;
		}
		//Debug.Log (activeGuyString);
	}
}
