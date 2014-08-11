using UnityEngine;
using System.Collections;

public class Movement_FollowingAgent2 : MonoBehaviour
{
	public Transform leadingAgent;
	public float speed;
	public Vector3 destination;
	public Vector3 agentPos;
	public Vector3 dir;
	public bool mated;
	public bool usingMating;
	public Color debugLineColor;
	public float colorAdder;
	
	void Start ()
	{
		speed = Random.Range (1, 35);
		usingMating = false;
		renderer.enabled = true;
		colorAdder = 0.01f;
		
		agentPos = transform.position;
		mated = false;
		debugLineColor = new Color(0.1f,0.1f,0.1f);
		
	}
	
	void Update ()
	{
		agentPos = transform.position;
		destination = leadingAgent.position;
		dir = destination - agentPos;
		transform.forward = dir;
		transform.position += transform.forward * Time.deltaTime * speed;
		
		
		Debug.DrawLine (agentPos, destination, debugLineColor);
		
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if (usingMating) {
			if (mated == false && collision.gameObject.tag == "FollowingAgent2") {
				if (collision.gameObject.GetComponent<Movement_FollowingAgent> ().speed > speed) {
					leadingAgent = collision.gameObject.transform;
					mated = true;
				}
			}
		} else {
			if (collision.gameObject.tag == "FollowingAgent2") {
				if (collision.gameObject.GetComponent<Movement_FollowingAgent> ().speed > speed) {
					leadingAgent = collision.gameObject.transform;
					debugLineColor.r += colorAdder;
					//Debug.Log (debugLineColor.r);
					if(debugLineColor.r >= 1f) colorAdder = -colorAdder;
					
					debugLineColor = new Color(debugLineColor.r, debugLineColor.b, debugLineColor.g);
				}
			}
		}
	}
}
