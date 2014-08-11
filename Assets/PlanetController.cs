using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

//	public Planet planet1;
//	public Planet planet2;
//	public Planet planet3;
//	public Planet planet4;
//	public Planet planet5;
//	public Planet planet6;
//	public Planet planet7;

	public Planet[] planets;


	void Start () {
		planets = new Planet[7];
		//		planet1 = GameObject.Find ("Planet1").GetComponent ("Planet") as Planet;
		//		planet2 = GameObject.Find ("Planet2").GetComponent ("Planet") as Planet;
		//		planet3 = GameObject.Find ("Planet3").GetComponent ("Planet") as Planet;

		for(int i = 0; i < planets.Length; i++){
			planets[i] = GameObject.Find ("Planet" + (i + 1).ToString()).GetComponent ("Planet") as Planet;
		}



//		planet1.rigidbody.AddForce (Vector3.left * 500000);
//		planet2.rigidbody.AddForce (Vector3.right * 500000);
//		planet3.rigidbody.AddForce (Vector3.left * 500000);



//		planets[0].rigidbody.AddForce (Vector3.left * 8900000);
//		planets[1].rigidbody.AddForce (Vector3.right * 8900000);
//		planets[2].rigidbody.AddForce (Vector3.left * 8900000);


		for(int i = 0; i < planets.Length; i++){
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

			planets[i].rigidbody.AddForce (dir * 8);
		}
	}
	

	void FixedUpdate () {
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
			for(int j = 0; j < planets.Length; j++){
					if (i != j)
					planets[i].rigidbody.AddForce((planets[j].transform.position - planets[i].transform.position)
				                           		   / (planets[j].rigidbody.mass / 18));

			}

		}

	
	}
}
