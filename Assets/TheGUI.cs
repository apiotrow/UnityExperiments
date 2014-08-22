using UnityEngine;
using System.Collections;

public class TheGUI : MonoBehaviour {
	public PlanetController pc;

	void Start(){
		//this won't work in code. being forced to use the drag and drop in unity
		//pc = GameObject.FindGameObjectWithTag ("PlanetController").GetComponent ("PlanetController") as PlanetController;
	}
	
	void Update(){

	}
	
	int strLengthToPixels(string str){
		return str.Length * 8;
	}


	void booleanBox(string txt, int tb, bool booly, string tck){
		//trails on/off title
		GUI.Box (new Rect (Screen.width - 100, tb, 100, 25), txt);
		//trails on/off change
		tb += 20;
		if (GUI.Button (new Rect (Screen.width - 50, tb, 50, 20), "on")) {
			PlayerPrefs.SetInt(tck, 1);
			booly = true;
		}
		if (GUI.Button (new Rect (Screen.width - 100, tb, 50, 20), "off")) {
			PlayerPrefs.SetInt(tck, 0);
			booly = false;
		}
	}


	
	void OnGUI ()
	{

		string text = "R to update";
		int controlswidth = strLengthToPixels (text);
		int controlsheight = Screen.height - 25;
		if (GUI.Button (new Rect (0, controlsheight, controlswidth, 20), text)) {
			Application.LoadLevel (0);  
		}
		text = "Move mouse to rotate,";
		GUI.Box (new Rect (controlswidth, controlsheight, strLengthToPixels(text), 20), text);
		controlswidth += strLengthToPixels(text);
		text = "Scroll mouse to zoom";
		GUI.Box (new Rect (controlswidth, controlsheight, strLengthToPixels(text), 20), text);
		
		
		//# planets title
		int thisbox = 0;
		text = "# planets";
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), text);
		//# planets + - 
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 50, 20), "+")) {
			pc.numPlanets += 1;
			PlayerPrefs.SetInt("numplan", pc.numPlanets);
			//PlayerPrefs.SetInt("pc.numPlanetsSet", 60);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 50, 20), "-")) {
			pc.numPlanets -= 1;
			if(pc.numPlanets < 2 ) pc.numPlanets = 2;
			PlayerPrefs.SetInt("numplan", pc.numPlanets);
			//PlayerPrefs.SetInt("pc.numPlanetsSet", 60);
		}
		//# planets ++ --
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 50, 20), "++")) {
			pc.numPlanets += 10;
			PlayerPrefs.SetInt("numplan", pc.numPlanets);
			//PlayerPrefs.SetInt("pc.numPlanetsSet", 60);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 50, 20), "--")) {
			pc.numPlanets -= 10;
			if(pc.numPlanets < 2 ) pc.numPlanets = 2;
			PlayerPrefs.SetInt("numplan", pc.numPlanets);
			//PlayerPrefs.SetInt("pc.numPlanetsSet", 60);
		}
		//# planets output
		thisbox += 20;
		text = "N:" + (Mathf.Floor (pc.numPlanets - 1)).ToString ();
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 20), text);
		



		//trails on/off title
		thisbox += 50;
		text = "All have trails";
//		string tochangekey = "trails";
//		booleanBox (text, thisbox, ref pc.trail.enabled, tochangekey);
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), text);
		//trails on/off change
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 50, 20), "True")) {
			PlayerPrefs.SetInt("trails", 1);
			//pc.trail.enabled = true;
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 50, 20), "False")) {
			PlayerPrefs.SetInt("trails", 0);
			//pc.trail.enabled = false;
		}
		//trails on/off output
//		thisbox += 20;
//		text = "Trails: " + pc.trail.enabled;
//		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 20), text);

//		//Debug.Log (pc.trail.enabled);
		if (PlayerPrefs.GetInt("trails", 0) == 0) {
			//go through trails
			int leftOfBox = thisbox - 20;;
			text = "Planet with trail";
			GUI.Box (new Rect (Screen.width - 200, leftOfBox, 100, 25), text);
			//got through trails buttons
			leftOfBox += 20;
			if (GUI.Button (new Rect (Screen.width - 200, leftOfBox, 50, 20), "-")) {
				if (pc.trailPlanet > 0)
					pc.trailPlanet -= 1;
			
			}
			if (GUI.Button (new Rect (Screen.width - 150, leftOfBox, 50, 20), "+")) {
				if (pc.trailPlanet < (pc.planets.Length - 1))
					pc.trailPlanet += 1;
			}
			//collisions on/off output
			leftOfBox += 20;
			text = "" + pc.trailPlanet;
			GUI.Box (new Rect (Screen.width - 200, leftOfBox, 100, 20), text);
		}
		
		//mass range title
		thisbox += 50;
		text = "Mass Range";
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), text);
		//mass range min + -
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "+")) {
			pc.massMax += 1;
			PlayerPrefs.SetFloat("massMax", pc.massMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "-")) {
			pc.massMax -= 1;
			if (pc.massMax <= (pc.massMin + 1)) pc.massMax = (pc.massMin + 1);
			PlayerPrefs.SetFloat("massMax", pc.massMax);
		}
		//mass range max + -
		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "+")) {
			pc.massMin += 1;
			if (pc.massMin >= (pc.massMax - 1)) pc.massMin = (pc.massMax - 1);
			PlayerPrefs.SetFloat("massMin", pc.massMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "-")) {
			pc.massMin -= 1;
			if(pc.massMin < 1) pc.massMin = 1;
			PlayerPrefs.SetFloat("massMin", pc.massMin);
		}
		//mass range min ++ --
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "++")) {
			pc.massMax += 10;
			PlayerPrefs.SetFloat("massMax", pc.massMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "--")) {
			pc.massMax -= 10;
			if (pc.massMax <= (pc.massMin + 1)) pc.massMax = (pc.massMin + 1);
			PlayerPrefs.SetFloat("massMax", pc.massMax);
		}
		//mass range max ++ --
		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "++")) {
			pc.massMin += 10;
			if (pc.massMin >= (pc.massMax - 1)) pc.massMin = (pc.massMax - 1);
			PlayerPrefs.SetFloat("massMin", pc.massMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "--")) {
			pc.massMin -= 10;
			if(pc.massMin < 1) pc.massMin = 1;
			PlayerPrefs.SetFloat("massMin", pc.massMin);
		}
		//mass range max and min outputs
		thisbox += 20;
		text = "" + pc.massMax;
		GUI.Box (new Rect (Screen.width - 50, thisbox, 50, 20), text);
		text = "" + pc.massMin;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 50, 20), text);




		//speed range title
		thisbox += 50;
		text = "Speed Range";
		float smallChangeAmount = 10;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), text);
		//speed range min + -
//		thisbox += 20;
//		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "+")) {
//			pc.speedMax += smallChangeAmount;
//			PlayerPrefs.SetFloat("speedMax", pc.speedMax);
//		}
//		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "-")) {
//			pc.speedMax -= smallChangeAmount;
//			if (pc.speedMax <= (pc.speedMin + 1)) pc.speedMax = (pc.speedMin + 1);
//			PlayerPrefs.SetFloat("speedMax", pc.speedMax);
//		}
		//speed range max + -
//		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "+")) {
//			pc.speedMin += smallChangeAmount;
//			if (pc.speedMin >= (pc.speedMax - 1)) pc.speedMin = (pc.speedMax - 1);
//			PlayerPrefs.SetFloat("speedMin", pc.speedMin);
//		}
//		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "-")) {
//			pc.speedMin -= smallChangeAmount;
//			if(pc.speedMin < 1) pc.speedMin = 1;
//			PlayerPrefs.SetFloat("speedMin", pc.speedMin);
//		}
		//speed range min ++ --
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "+")) {
			pc.speedMax += smallChangeAmount * 10;
			PlayerPrefs.SetFloat("speedMax", pc.speedMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "-")) {
			pc.speedMax -= smallChangeAmount * 10;
			if (pc.speedMax <= (pc.speedMin + 1)) pc.speedMax = (pc.speedMin + 1);
			PlayerPrefs.SetFloat("speedMax", pc.speedMax);
		}
		//speed range max ++ --
		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "+")) {
			pc.speedMin += smallChangeAmount * 10;
			if (pc.speedMin >= (pc.speedMax - 1)) pc.speedMin = (pc.speedMax - 1);
			PlayerPrefs.SetFloat("speedMin", pc.speedMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "-")) {
			pc.speedMin -= smallChangeAmount * 10;
			if(pc.speedMin < 1) pc.speedMin = 1;
			PlayerPrefs.SetFloat("speedMin", pc.speedMin);
		}
		//speed range min +++ ---
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "++")) {
			pc.speedMax += smallChangeAmount * 100;
			PlayerPrefs.SetFloat("speedMax", pc.speedMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "--")) {
			pc.speedMax -= smallChangeAmount * 100;
			if (pc.speedMax <= (pc.speedMin + 1)) pc.speedMax = (pc.speedMin + 1);
			PlayerPrefs.SetFloat("speedMax", pc.speedMax);
		}
		//speed range max +++ ---
		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "++")) {
			pc.speedMin += smallChangeAmount * 100;
			if (pc.speedMin >= (pc.speedMax - 1)) pc.speedMin = (pc.speedMax - 1);
			PlayerPrefs.SetFloat("speedMin", pc.speedMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "--")) {
			pc.speedMin -= smallChangeAmount * 100;
			if(pc.speedMin < 1) pc.speedMin = 1;
			PlayerPrefs.SetFloat("speedMin", pc.speedMin);
		}
		//speed range max and min outputs
		thisbox += 20;
		text = "" + pc.speedMax;
		GUI.Box (new Rect (Screen.width - 50, thisbox, 50, 20), text);
		text = "" + pc.speedMin;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 50, 20), text);




		//spawn range title
		thisbox += 50;
		text = "Spawn Range";
		smallChangeAmount = 500f;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), text);
		//spawn range min + -
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 50, 20), "+")) {
			pc.maxrange += smallChangeAmount;
			PlayerPrefs.SetFloat("maxrange", pc.maxrange);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 50, 20), "-")) {
			pc.maxrange -= smallChangeAmount;
			if(pc.maxrange < 1 ) pc.maxrange = 1;
			PlayerPrefs.SetFloat("maxrange", pc.maxrange);
		}
		//spawn range max and min outputs
		thisbox += 20;
		text = "" + pc.maxrange;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 20), text);




		//size range title
		thisbox += 50;
		text = "Size Range";
		smallChangeAmount = 10;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), text);
		//size range min + -
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "+")) {
			pc.sizeMax += smallChangeAmount;
			PlayerPrefs.SetFloat("sizeMax", pc.sizeMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "-")) {
			pc.sizeMax -= smallChangeAmount;
			if (pc.sizeMax <= (pc.sizeMin + 1)) pc.sizeMax = (pc.sizeMin + 1);
			PlayerPrefs.SetFloat("sizeMax", pc.sizeMax);
		}
		//size range max + -
		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "+")) {
			pc.sizeMin += smallChangeAmount;
			if (pc.sizeMin >= (pc.sizeMax - 1)) pc.sizeMin = (pc.sizeMax - 1);
			PlayerPrefs.SetFloat("sizeMin", pc.sizeMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "-")) {
			pc.sizeMin -= smallChangeAmount;
			if(pc.sizeMin < 1) pc.sizeMin = 1;
			PlayerPrefs.SetFloat("sizeMin", pc.sizeMin);
		}
		//size range min ++ --
		thisbox += 20;
		if (GUI.Button (new Rect (Screen.width - 25, thisbox, 25, 20), "++")) {
			pc.sizeMax += smallChangeAmount * 10;
			PlayerPrefs.SetFloat("sizeMax", pc.sizeMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox, 25, 20), "--")) {
			pc.sizeMax -= smallChangeAmount * 10;
			if (pc.sizeMax <= (pc.sizeMin + 1)) pc.sizeMax = (pc.sizeMin + 1);
			PlayerPrefs.SetFloat("sizeMax", pc.sizeMax);
		}
		//size range max ++ --
		if (GUI.Button (new Rect (Screen.width - 75, thisbox, 25, 20), "++")) {
			pc.sizeMin += smallChangeAmount * 10;
			if (pc.sizeMin >= (pc.sizeMax - 1)) pc.sizeMin = (pc.sizeMax - 1);
			PlayerPrefs.SetFloat("sizeMin", pc.sizeMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox, 25, 20), "--")) {
			pc.sizeMin -= smallChangeAmount * 10;
			if(pc.sizeMin < 1) pc.sizeMin = 1;
			PlayerPrefs.SetFloat("sizeMin", pc.sizeMin);
		}
		//size range max and min outputs
		thisbox += 20;
		text = "" + pc.sizeMax;
		GUI.Box (new Rect (Screen.width - 50, thisbox, 50, 20), text);
		text = "" + pc.sizeMin;
		GUI.Box (new Rect (Screen.width - 100, thisbox, 50, 20), text);












		/*
		 * 
		 * LEFT SIDE OF SCREEN
		 * 
		 */
		//collisions on/off title
		thisbox = 0;
		text = "Planet Collision";
		GUI.Box (new Rect (0, thisbox, 100, 25), text);
		//trails on/off change
		thisbox += 20;
		if (GUI.Button (new Rect (0, thisbox, 50, 20), "on")) {
			PlayerPrefs.SetInt("planCollide", 1);
			pc.planCollide = true;
		}
		if (GUI.Button (new Rect (50, thisbox, 50, 20), "off")) {
			PlayerPrefs.SetInt("planCollide", 0);
			pc.planCollide = false;
		}
		//collisions on/off output
		thisbox += 20;
		text = "" + pc.planCollide;
		GUI.Box (new Rect (0, thisbox, 100, 20), text);






	}
}
