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
	public bool planCollide;
	public int trailPlanet;
	public Planet sourcePlanet;


	void Start () {

		trailPlanet = 1;

		moi = Camera.main.GetComponent<MouseOrbitImproved> ();
		moi.distance = PlayerPrefs.GetFloat ("cameradistance", 500);

		numPlanets = PlayerPrefs.GetInt ("numplan", 8);

		if (PlayerPrefs.GetInt ("planCollide", 0) == 0)
			planCollide = false;
		else if(PlayerPrefs.GetInt ("planCollide", 0) == 1)
			planCollide = true;

		planets = new Planet[numPlanets];

		planets[0] = GameObject.Find ("Sun").GetComponent ("Planet") as Planet;
		planets [0].renderer.material.color = Color.yellow;

		//planets[1] = GameObject.Find ("Planet").GetComponent ("Planet") as Planet;

		sourcePlanet = GameObject.Find ("Planet").GetComponent ("Planet") as Planet;

		//toggle all trails on/off
		//give trails different colors
		//make thickness of trails change with zoom level
		//time trail lasts


		if (planCollide)
			//planets [1].collider.isTrigger = false;
			sourcePlanet.collider.isTrigger = false;
		else
			//planets [1].collider.isTrigger = true;
			sourcePlanet.collider.isTrigger = true;

//		if (PlayerPrefs.GetInt ("trails") == 0)
//			trail.enabled = false;
//		else if(PlayerPrefs.GetInt ("trails") == 1)
//			trail.enabled = true;



		maxrange = PlayerPrefs.GetFloat ("maxrange", 300f);

		massMax = PlayerPrefs.GetFloat ("massMax", 1f);
		massMin = PlayerPrefs.GetFloat ("massMin", 10f);
		
		speedMax = PlayerPrefs.GetFloat ("speedMax", 1f);
		speedMin = PlayerPrefs.GetFloat ("speedMin", 100f);

		sizeMin = PlayerPrefs.GetFloat ("sizeMin", 1f);;
		sizeMax = PlayerPrefs.GetFloat ("sizeMax", 100f);;

		for(int i = 1; i < planets.Length; i++){
			Vector3 randLoc = new Vector3(Random.Range (-maxrange,maxrange), Random.Range (-maxrange,maxrange), Random.Range (-maxrange,maxrange));

			//planets[i] = GameObject.Find ("Planet" + (i + 1).ToString()).GetComponent ("Planet") as Planet;
			//Planet newplanet = Instantiate (planets[1], randLoc, transform.rotation) as Planet;
			Planet newplanet = Instantiate (sourcePlanet, randLoc, transform.rotation) as Planet;
			planets[i] = newplanet;

			if (planCollide)
				planets [i].collider.isTrigger = false;
			else
				planets [i].collider.isTrigger = true;

			//planets[i].rigidbody.mass = Random.Range (PlayerPrefs.GetFloat ("massMin"), PlayerPrefs.GetFloat ("massMax"));
			planets[i].rigidbody.mass = Random.Range (massMin, massMax);

			//planets[i].renderer.material.color = new Color(Random.Range (0f,1f),Random.Range (0f,1f),Random.Range (0f,1f));
			planets[i].renderer.material.color = 
				ColorFromHSV(Random.Range(0.0f, 360f), 1f, Random.Range(1f, 5f), 1f);

			float randScale = Random.Range (sizeMin, sizeMax);
			planets[i].transform.localScale += new Vector3(randScale,randScale,randScale);

			float rand = Random.Range (0f,1f);
			Vector3 dir;

			dir = new Vector3(Random.Range (-1f, 1f),Random.Range (-1f, 1f),Random.Range (-1f, 1f)) ;
			
			//planets[i].rigidbody.AddForce (dir * 9000);

			planets[i].rigidbody.velocity = (dir * Random.Range (speedMin, speedMax));
		}

		sourcePlanet.renderer.enabled = false;

	}
	

	void FixedUpdate () {
		moi = Camera.main.GetComponent<MouseOrbitImproved> ();
		PlayerPrefs.SetFloat ("cameradistance", moi.distance);

//		TrailRenderer trails = planets[3].GetComponent<TrailRenderer>();
//		trails.enabled = true;
//		trails.material.color = Color.green;

		//Debug.Log (moi.distance);

//		trail = planets[trailPlanet].GetComponent<TrailRenderer>();
//		trail.enabled = true;


		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel (0); 
		}

		//Debug.Log (trailPlanet + ", " + planets.Length);
		for(int i = 0; i < planets.Length; i++){
			trail = planets[i].GetComponent<TrailRenderer>();
			
			if (PlayerPrefs.GetInt ("trails") == 0){
				//trail.enabled = false;
				trail.startWidth = 0f;
				trail.endWidth = 0f;
				if (i == trailPlanet) {
					//trail.enabled = true;
					trail.startWidth = planets[i].transform.localScale.x * 3f;
					trail.endWidth = 1f;
				}//else {
//					//trail.enabled = false;
//					trail.startWidth = 0f;
//					trail.endWidth = 0f;
//				}
			}else if(PlayerPrefs.GetInt ("trails") == 1){
				//trail.enabled = true;
				trail.startWidth = planets[i].transform.localScale.x * 3f;
				trail.endWidth = 1f;
			}

			Color nColor = planets[i].renderer.material.color;
			//nColor = ColorFromHSV(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1f);
			//ColorToHSV(nColor, Random.Range(0.0f, 1.0f),1.0f, 1.0f);
			Material traild = planets[i].GetComponent<TrailRenderer>().material;
			traild.SetColor("_Color", nColor);



			for(int j = 0; j < planets.Length; j++){
					if (i != j)
					planets[i].rigidbody.AddForce((planets[j].transform.position - planets[i].transform.position)
				                           		   / (planets[j].rigidbody.mass / 18));

			}

		}

	
	}
















	public static Color ColorFromHSV(float h, float s, float v, float a = 1)
	{
		// no saturation, we can return the value across the board (grayscale)
		if (s == 0)
			return new Color(v, v, v, a);
		
		// which chunk of the rainbow are we in?
		float sector = h / 60;
		
		// split across the decimal (ie 3.87 into 3 and 0.87)
		int i = (int)sector;
		float f = sector - i;
		
		float p = v * (1 - s);
		float q = v * (1 - s * f);
		float t = v * (1 - s * (1 - f));
		
		// build our rgb color
		Color color = new Color(0, 0, 0, a);
		
		switch(i)
		{
		case 0:
			color.r = v;
			color.g = t;
			color.b = p;
			break;
			
		case 1:
			color.r = q;
			color.g = v;
			color.b = p;
			break;
			
		case 2:
			color.r  = p;
			color.g  = v;
			color.b  = t;
			break;
			
		case 3:
			color.r  = p;
			color.g  = q;
			color.b  = v;
			break;
			
		case 4:
			color.r  = t;
			color.g  = p;
			color.b  = v;
			break;
			
		default:
			color.r  = v;
			color.g  = p;
			color.b  = q;
			break;
		}
		
		return color;
	}
	
	public static void ColorToHSV(Color color, out float h, out float s, out float v)
	{
		float min = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
		float max = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
		float delta = max - min;
		
		// value is our max color
		v = max;
		
		// saturation is percent of max
		if (!Mathf.Approximately(max, 0))
			s = delta / max;
		else
		{
			// all colors are zero, no saturation and hue is undefined
			s = 0;
			h = -1;
			return;
		}
		
		// grayscale image if min and max are the same
		if (Mathf.Approximately(min, max))
		{
			v = max;
			s = 0;
			h = -1;
			return;
		}
		
		// hue depends which color is max (this creates a rainbow effect)
		if (color.r == max)
			h = (color.g - color.b) / delta;         	// between yellow & magenta
		else if (color.g == max)
			h = 2 + (color.b - color.r) / delta; 		// between cyan & yellow
		else
			h = 4 + (color.r - color.g) / delta; 		// between magenta & cyan
		
		// turn hue into 0-360 degrees
		h *= 60;
		if (h < 0 )
			h += 360;
	}
}
