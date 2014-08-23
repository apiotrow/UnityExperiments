using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	public Person person;
	float distToPerson;
	Vector3 planetPos;
	Vector3 personPos;
	Vector3 personToPlanet;
	float planetMass;


	void Start () {
		planetPos = transform.position;
//		person =  GameObject.Find ("Person").GetComponent ("Person") as Person;
//		personPos = person.transform.position;
//		distToPerson = Vector3.Distance (personPos, personPos);

		planetMass = rigidbody.mass;
	}
	

	void FixedUpdate () {

		//interaction with person
//		planetPos = transform.position;
//		personPos = person.transform.position;
//		personToPlanet = planetPos - personPos;
//
//		person.rigidbody.AddForce (personToPlanet / (planetMass / 500));

	}

	void OnCollisionEnter (Collision collision){
		if (collision.gameObject.tag == "FollowingAgent") {
		}
	}
}
