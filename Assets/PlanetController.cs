using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public Planet[] planets;

	int numPlanets = 4;
	TrailRenderer trail;
	float massMax = 5f;
	float massMin;
	MouseOrbit morb;

	void OnGUI ()
	{

		if (GUI.Button (new Rect (Screen.width - 200, 0, 100, 20), "R to restart")) {
			Application.LoadLevel (0);  
		}


		int thisbox = 0;
		string planetsstr = "# planets";
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), planetsstr);
		if (GUI.Button (new Rect (Screen.width - 50, thisbox + 20, 50, 20), "+")) {
			numPlanets += 1;
			PlayerPrefs.SetInt("numplan", numPlanets);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox+ 20, 50, 20), "-")) {
			numPlanets -= 1;
			if(numPlanets < 2 ) numPlanets = 2;
			PlayerPrefs.SetInt("numplan", numPlanets);
		}
		string strnumPlanets = "N:" + (Mathf.Floor (numPlanets)).ToString ();
		GUI.Box (new Rect (Screen.width - 100, thisbox + 40, 100, 20), strnumPlanets);


		thisbox = thisbox + 90;
		planetsstr = "Trails";
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), planetsstr);
		if (GUI.Button (new Rect (Screen.width - 50, thisbox + 20, 50, 20), "on")) {
			PlayerPrefs.SetInt("trails", 1);
			trail.enabled = true;
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox+ 20, 50, 20), "off")) {
			PlayerPrefs.SetInt("trails", 0);
			trail.enabled = false;
		}
		strnumPlanets = "Trails: " + trail.enabled;
		GUI.Box (new Rect (Screen.width - 100, thisbox + 40, 100, 20), strnumPlanets);


		thisbox = thisbox + 100;
		planetsstr = "Mass Range";
		GUI.Box (new Rect (Screen.width - 100, thisbox, 100, 25), planetsstr);
		if (GUI.Button (new Rect (Screen.width - 25, thisbox + 20, 25, 20), "+")) {
			massMax += 1;
			PlayerPrefs.SetFloat("massMax", massMax);
		}
		if (GUI.Button (new Rect (Screen.width - 50, thisbox+ 20, 25, 20), "-")) {
			massMax -= 1;
			if (massMax <= (massMin + 1)) massMax = (massMin + 1);
			PlayerPrefs.SetFloat("massMax", massMax);
		}
		strnumPlanets = "" + massMax;
		GUI.Box (new Rect (Screen.width - 50, thisbox + 40, 50, 20), strnumPlanets);

		if (GUI.Button (new Rect (Screen.width - 75, thisbox + 20, 25, 20), "+")) {
			massMin += 1;
			if (massMin >= (massMax - 1)) massMin = (massMax - 1);
			PlayerPrefs.SetFloat("massMin", massMin);
		}
		if (GUI.Button (new Rect (Screen.width - 100, thisbox+ 20, 25, 20), "-")) {
			massMin -= 1;
			if(massMin < 1) massMin = 1;
			PlayerPrefs.SetFloat("massMin", massMin);
		}
		strnumPlanets = "" + massMin;
		GUI.Box (new Rect (Screen.width - 100, thisbox + 40, 50, 20), strnumPlanets);
	}


	void Start () {

		morb = Camera.main.GetComponent<MouseOrbit>();
//		MouseOrbit.distance = 200f;

		numPlanets = PlayerPrefs.GetInt ("numplan");
		//planets = new Planet[Random.Range (3, 15)];
		planets = new Planet[numPlanets];
		planets[0] = GameObject.Find ("Planet1").GetComponent ("Planet") as Planet;
		trail = planets[0].GetComponent<TrailRenderer>();

		if (PlayerPrefs.GetInt ("trails") == 0)
			trail.enabled = false;
		else if(PlayerPrefs.GetInt ("trails") == 1)
			trail.enabled = true;

		planets[1] = GameObject.Find ("Planet3").GetComponent ("Planet") as Planet;
		planets [1].renderer.material.color = Color.yellow;

		float maxrange = 200f;

		for(int i = 2; i < planets.Length; i++){
			Vector3 randLoc = new Vector3(Random.Range (-maxrange,maxrange), Random.Range (-maxrange,maxrange), Random.Range (-maxrange,maxrange));

			//planets[i] = GameObject.Find ("Planet" + (i + 1).ToString()).GetComponent ("Planet") as Planet;
			Planet newplanet = Instantiate (planets[0], randLoc, transform.rotation) as Planet;
			planets[i] = newplanet;

			//planets[i].rigidbody.mass = Random.Range (1f, 2f);
			planets[i].rigidbody.mass = Random.Range (PlayerPrefs.GetFloat ("massMin"), PlayerPrefs.GetFloat ("massMax"));
			massMax = PlayerPrefs.GetFloat ("massMax");
			massMin = PlayerPrefs.GetFloat ("massMin");

			planets[i].renderer.material.color = new Color(Random.Range (0f,1f),Random.Range (0f,1f),Random.Range (0f,1f));

			float randScale = Random.Range (1f, 6f);
			planets[i].transform.localScale += new Vector3(randScale,randScale,randScale);

			float rand = Random.Range (0f,1f);
			Vector3 dir;
			
			if(rand > 0f && rand < 0.25f)
				dir = Vector3.left;
			else if(rand > 0.25f && rand < 0.5f)
				dir = Vector3.right;
			else if(rand > 0.5f && rand < 0.75f)
				dir = Vector3.forward;
			else if(rand > 0.75f && rand < 1f)
				dir = Vector3.back;
			else
				dir = Vector3.back;
			
			planets[i].rigidbody.AddForce (dir * 900);
		}



//		planet1.rigidbody.AddForce (Vector3.left * 500000);
//		planet2.rigidbody.AddForce (Vector3.right * 500000);
//		planet3.rigidbody.AddForce (Vector3.left * 500000);



//		planets[0].rigidbody.AddForce (Vector3.left * 8900000);
//		planets[1].rigidbody.AddForce (Vector3.right * 8900000);
//		planets[2].rigidbody.AddForce (Vector3.left * 8900000);


//		for(int i = 0; i < planets.Length; i++){
//			float rand = Random.Range (0f,1f);
//			Vector3 dir;
//
//			if(rand > 0f && rand < 0.25f)
//				dir = Vector3.left;
//			else if(rand > 0.25f && rand < 0.5f)
//				dir = Vector3.right;
//			else if(rand > 0.5f && rand < 0.75f)
//				dir = Vector3.forward;
//			else if(rand > 0.75f && rand < 1f)
//				dir = Vector3.back;
//			else
//				dir = Vector3.back;
//
//			planets[i].rigidbody.AddForce (dir * 8);
//		}
	}
	

	void FixedUpdate () {
		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel (0); 
		}
		//global stuff
//		Vector3 planet1ToPlanet2 = planet2.transform.position - planet1.transform.position;
//		planet1.rigidbody.AddForce (planet1ToPlanet2 / (planet2.rigidbody.mass / 500000));
//		
//		Vector3 planet2ToPlanet3 = planet3.transform.position - planet2.transform.position;
//		planet2.rigidbody.AddForce (planet2ToPlanet3 / (planet3.rigidbody.mass / 50000));
//		
//		Vector3 planet3ToPlanet1 = planet1.transform.position - planet3.transform.position;
//		planet3.rigidbody.AddForce (planet3ToPlanet1 / (planet1.rigidbody.mass / 50000));

		//planets[0].rigidbody.AddForce((planets[1].transform.position - planets[0].transform.position)
		                        //      / (planets[1].rigidbody.mass / 50000));

		for(int i = 0; i < planets.Length; i++){
			trail = planets[i].GetComponent<TrailRenderer>();
			
			if (PlayerPrefs.GetInt ("trails") == 0)
				trail.enabled = false;
			else if(PlayerPrefs.GetInt ("trails") == 1)
				trail.enabled = true;

			for(int j = 0; j < planets.Length; j++){
					if (i != j)
					planets[i].rigidbody.AddForce((planets[j].transform.position - planets[i].transform.position)
				                           		   / (planets[j].rigidbody.mass / 18));

			}

		}

	
	}
}
