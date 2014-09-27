using UnityEngine;
using System.Collections;

public class FootMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bob = new Vector3 (10, 0, 10);

			rigidbody.AddForce (bob * 2f);
	}
}
