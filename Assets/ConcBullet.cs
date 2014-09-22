using UnityEngine;
using System.Collections;

public class ConcBullet : MonoBehaviour {
	Vector3 initPosition;
	float distTraveled;
	GameObject concControllerObject;
	ConcController CC;

	void Start () {
		initPosition = transform.position;
		concControllerObject = GameObject.Find("ConcController");
		CC = concControllerObject.GetComponent<ConcController>();
	}
	

	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * 10f);

		distTraveled = Vector3.Distance (initPosition, transform.position);
		if (distTraveled > 50f) {
			Destroy (gameObject);
		}

		CC = concControllerObject.GetComponent<ConcController>();

	}

	void OnTriggerEnter(Collider other) {

		if (!other.name.Equals (CC.activeGuyString) && !other.name.Equals ("Ground")) {
			Debug.Log (other.name);
			CC.setActiveGuy (other.name);
			//Debug.Log (CC.activeGuyString);
		}

	}
}
