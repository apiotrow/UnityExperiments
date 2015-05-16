using UnityEngine;
using System.Collections;

public class Movement_LeadingAgent : MonoBehaviour
{
	public float speed;
	public Vector3 destination;
	public Vector3 agentPos;
	public Vector3 dir;
	public Vector3 mousePos;
	public bool arrivedAtDestination;
	float destx;
	float desty;
	float destz;
	public float timer;
	public float maxDistanceToTravel;
	public float bounds;
	public float sphereCheckRadius;
	public float distToDestination;
	public float currDistToDestination;
	public bool XisPos;
	public bool ZisPos;
	public bool destSettledOn;
	public bool agentThere;
	public int stackOverflowPreventer;
	public bool safeRouteOn;
	public bool usingKeyb;

	void Start ()
	{
		maxDistanceToTravel = 30;
		speed = 90;
		bounds = 100;
		sphereCheckRadius = 20;
		safeRouteOn = true;
		usingKeyb = false;


		agentPos = transform.position;
		destSettledOn = false;
		agentThere = false;
		stackOverflowPreventer = 0;
		distToDestination = 0;
		arrivedAtDestination = false;
		currDistToDestination = 0;


		getNewDestination ();
	}

	void Update(){

		agentPos = transform.position;
		Debug.DrawLine (agentPos, destination);


		
		if (!usingKeyb) {
			if (arrivedAtDestination == true) {
				getNewDestination ();
			}
		} else {
			if (Input.GetKey (KeyCode.W)) {
				transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z + 3f);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z - 3f);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.position = new Vector3(transform.position.x - 3f,transform.position.y, transform.position.z);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.position = new Vector3(transform.position.x + 3f,transform.position.y, transform.position.z);
			}
		}
		
	}
	
	void FixedUpdate ()
	{

		if (!usingKeyb) {
			agentPos = transform.position;


			currDistToDestination = Vector3.Distance (agentPos, destination);
			//Debug.Log (XisPos);
			//Debug.Log (distToDestination);


			if (((destination.x - agentPos.x > 0) && XisPos == false)
				|| ((destination.z - agentPos.z > 0) && ZisPos == false)
				|| ((destination.x - agentPos.x < 0) && XisPos == true)
				|| ((destination.z - agentPos.z < 0) && ZisPos == true)) {
				arrivedAtDestination = true;
			} else {
				transform.position += transform.forward * Time.deltaTime * speed;
			}
		}

		/*
		if(((destination.x - agentPos.x < 0) && XisPos == false)
		&& ((destination.z - agentPos.z < 0) && ZisPos == false)){
			transform.position += transform.forward * Time.deltaTime * speed;
		}else if(((destination.x - agentPos.x > 0) && XisPos == true)
		         && ((destination.z - agentPos.z > 0) && ZisPos == true)){
			transform.position += transform.forward * Time.deltaTime * speed;
		}else{
			arrivedAtDestination = true;
		}
		*/

		/*
		if(Mathf.Abs(distToDestination) - Mathf.Abs (currDistToDestination) < 0){

		}else {
			transform.position += transform.forward * Time.deltaTime * speed;
		}
		*/
	}

	void getNewDestination ()
	{
		agentPos = transform.position;

		if (safeRouteOn) {
			while (!destSettledOn) {
				destx = Random.Range (agentPos.x - maxDistanceToTravel, agentPos.x + maxDistanceToTravel);
				while (Mathf.Abs(destx) > bounds) {
					destx = Random.Range (agentPos.x - maxDistanceToTravel, agentPos.x + maxDistanceToTravel);
				}
	
				desty = agentPos.y;

				destz = Random.Range (agentPos.z - maxDistanceToTravel, agentPos.z + maxDistanceToTravel);
				while (Mathf.Abs(destz) > bounds) {
					destz = Random.Range (agentPos.z - maxDistanceToTravel, agentPos.z + maxDistanceToTravel);
				}

				destination = new Vector3 (destx, desty, destz);
				distToDestination = Vector3.Distance (agentPos, destination);

				if(destination.x - agentPos.x < 0){
					XisPos = false;
				}else{
					XisPos = true;
				}
				if(destination.z - agentPos.z < 0){
					ZisPos = false;
				}else{
					ZisPos = true;
				}

				Collider[] thingsThere = Physics.OverlapSphere (destination, sphereCheckRadius);
				int p = 0;
				while (agentThere == false && p < thingsThere.Length) {
					if (thingsThere [p].GetComponent<Collider>().gameObject.tag == "FollowingAgent") {
						agentThere = true;
					}
					p++;
				}
				if (agentThere == true) {
					agentThere = false;
				} else {
					destSettledOn = true;
				}
				stackOverflowPreventer++;
				if(stackOverflowPreventer > 100){
					destSettledOn = true;
					stackOverflowPreventer = 0;
				}
			}
		} else {
			destx = Random.Range (agentPos.x - maxDistanceToTravel, agentPos.x + maxDistanceToTravel);
			while (Mathf.Abs(destx) > bounds) {
				destx = Random.Range (agentPos.x - maxDistanceToTravel, agentPos.x + maxDistanceToTravel);
			}
			
			desty = agentPos.y;
			
			destz = Random.Range (agentPos.z - maxDistanceToTravel, agentPos.z + maxDistanceToTravel);
			while (Mathf.Abs(destz) > bounds) {
				destz = Random.Range (agentPos.z - maxDistanceToTravel, agentPos.z + maxDistanceToTravel);
			}
			
			destination = new Vector3 (destx, desty, destz);
			distToDestination = Vector3.Distance (agentPos, destination);
			if(destination.x - agentPos.x < 0){
				XisPos = false;
			}else{
				XisPos = true;
			}
			if(destination.z - agentPos.z < 0){
				ZisPos = false;
			}else{
				ZisPos = true;
			}
		}
		

		destSettledOn = false;

		dir = destination - agentPos;
		transform.forward = dir;
		arrivedAtDestination = false;
	}
}
