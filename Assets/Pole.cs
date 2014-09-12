using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour {
	public Vector3 around;
	public Vector3 around2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, around, Time.deltaTime * 40);
		transform.RotateAround (transform.position, around2, Time.deltaTime * 10);
	}
}
