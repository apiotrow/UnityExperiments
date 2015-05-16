using UnityEngine;
using System.Collections;

public class DriveWheel : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
		transform.Rotate(-Vector3.up * Time.deltaTime * 200f);
//		transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
	}
}
