using UnityEngine;
using System.Collections;

public class Plate : MonoBehaviour
{
	Vector3 platePos;
	Vector3 ballPos;
	float dist;
	public GameObject ball;
	public Quaternion thingy;

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width - 200, 0, 100, 20), "R to restart")) {
			Application.LoadLevel (0);  
		}
	}

	void Start ()
	{
		ball = GameObject.Find ("Ball");
		//ball.renderer.material.color = Color.red;
	}

	void FixedUpdate ()
	{
		platePos = transform.position;
		ballPos = ball.transform.position;

		float xdiff = platePos.x - ballPos.x;
		float zdiff = platePos.z - ballPos.z;
		dist = Vector3.Distance (ballPos, platePos);
		float xrot = transform.rotation.eulerAngles.x;
		float zrot = transform.rotation.eulerAngles.z;
		//dist *= 1f;

		Vector3 start = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

		if (xdiff < 0) {
	
//			if (zrot >= 70 && zrot <= 180) {
//				Vector3 end = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, start.z - 1);
//				transform.eulerAngles = end;
//			} else {
				transform.RotateAround (transform.position, Vector3.forward, Time.deltaTime * dist);
//			}
					
		} else {
	
//			if (zrot >= 180 && zrot <= 270) {
//				Vector3 end = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, start.z + 1);
//				transform.eulerAngles = end;
//			} else {
				transform.RotateAround (transform.position, Vector3.back, Time.deltaTime * dist);
//			}
		}

		if (zdiff < 0) {

//			if (xrot >= 70 && xrot <= 180) {
//				Vector3 end = new Vector3 (start.x - 1, transform.eulerAngles.y, transform.eulerAngles.z);
//				transform.eulerAngles = end;
//			}else{
				transform.RotateAround (transform.position, Vector3.left, Time.deltaTime * dist);
//			}
		} else {

//			if (xrot >= 180 && xrot <= 270) {
//				Vector3 end = new Vector3 (start.x + 1, transform.eulerAngles.y, transform.eulerAngles.z);
//				transform.eulerAngles = end;
//			}else{
				transform.RotateAround (transform.position, Vector3.right, Time.deltaTime * dist);
//			}
		}

		Vector3 v = ball.rigidbody.velocity;
		v.y = -10f;
		//ball.rigidbody.velocity = v;


		//Debug.Log (transform.rotation.eulerAngles.z);


	}
}
