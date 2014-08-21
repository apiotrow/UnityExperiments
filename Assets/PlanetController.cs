using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public Planet[] planets;

	public int numPlanets;
	public TrailRenderer trail;
	public float massMax;
	public float massMin;
	public float speedMax;
	public float speedMin;
	public MouseOrbitImproved moi;
	public float maxrange;
	public float sizeMin;
	public float sizeMax;
	public bool collisions;


	void Start () {

		moi = Camera.main.GetComponent<MouseOrbitImproved> ();
		moi.distance = PlayerPrefs.GetFloat ("cameradistance", 500);

		numPlanets = PlayerPrefs.GetInt ("numplan", 8);

		planets = new Planet[numPlanets];
		planets[0] = GameObject.Find ("Planet1").GetComponent ("Planet") as Planet;
		trail = planets[0].GetComponent<TrailRenderer>();

		if (PlayerPrefs.GetInt ("trails") == 0)
			trail.enabled = false;
		else if(PlayerPrefs.GetInt ("trails") == 1)
			trail.enabled = true;

		massMax = PlayerPrefs.GetFloat ("massMax", 1f);
		massMin = PlayerPrefs.GetFloat ("massMin", 10f);

		speedMax = PlayerPrefs.GetFloat ("speedMax", 1f);
		speedMin = PlayerPrefs.GetFloat ("speedMin", 100f);




		planets[1] = GameObject.Find ("Planet3").GetComponent ("Planet") as Planet;
		planets [1].renderer.material.color = Color.yellow;

		maxrange = PlayerPrefs.GetFloat ("maxrange", 300f);

		sizeMin = PlayerPrefs.GetFloat ("sizeMin", 1f);;
		sizeMax = PlayerPrefs.GetFloat ("sizeMax", 100f);;

		for(int i = 2; i < planets.Length; i++){
			Vector3 randLoc = new Vector3(Random.Range (-maxrange,maxrange), Random.Range (-maxrange,maxrange), Random.Range (-maxrange,maxrange));

			//planets[i] = GameObject.Find ("Planet" + (i + 1).ToString()).GetComponent ("Planet") as Planet;
			Planet newplanet = Instantiate (planets[0], randLoc, transform.rotation) as Planet;
			planets[i] = newplanet;

			//planets[i].rigidbody.mass = Random.Range (PlayerPrefs.GetFloat ("massMin"), PlayerPrefs.GetFloat ("massMax"));
			planets[i].rigidbody.mass = Random.Range (massMin, massMax);

			planets[i].renderer.material.color = new Color(Random.Range (0f,1f),Random.Range (0f,1f),Random.Range (0f,1f));

			float randScale = Random.Range (sizeMin, sizeMax);
			planets[i].transform.localScale += new Vector3(randScale,randScale,randScale);

			float rand = Random.Range (0f,1f);
			Vector3 dir;

			dir = new Vector3(Random.Range (-1f, 1f),Random.Range (-1f, 1f),Random.Range (-1f, 1f)) ;
			
			//planets[i].rigidbody.AddForce (dir * 9000);

			planets[i].rigidbody.velocity = (dir * Random.Range (speedMin, speedMax));
		}

	}
	

	void FixedUpdate () {
		moi = Camera.main.GetComponent<MouseOrbitImproved> ();
		PlayerPrefs.SetFloat ("cameradistance", moi.distance);

		Debug.Log (moi.distance);


		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel (0); 
		}

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
