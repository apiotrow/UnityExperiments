using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 forward = Camera.mainCamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		Vector3 moveDirection = (h * right + v * forward);
		moveDirection = Quaternion.Inverse(this.transform.rotation) * moveDirection ;
		moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z); 
		moveDirection = this.transform.rotation * moveDirection;

		Vector3 globalUp;
		globalUp.x = moveDirection.x;
		globalUp.y = moveDirection.y + 1f;
		globalUp.z = moveDirection.z;
		//Debug.DrawLine (Camera.main.transform.position, transform.position);

		if ((Input.GetKey (KeyCode.Space))) {
			rigidbody.AddForce (globalUp * 50);
		}

		if (Input.GetKey (KeyCode.W)) {
			//transform.RotateAround (transform.position, transform.right, Time.deltaTime * 40);
			rigidbody.AddForce (moveDirection * 50);

		}
		if (Input.GetKey (KeyCode.S)) {
			//transform.RotateAround (transform.position, -transform.right, Time.deltaTime * 40);
		}
		if (Input.GetKey (KeyCode.A)) {
			//transform.RotateAround (transform.position, transform.forward, Time.deltaTime * 40);
		}
		if (Input.GetKey (KeyCode.D)) {
			//transform.RotateAround (transform.position, -transform.forward, Time.deltaTime * 40);
		}
	}
}
