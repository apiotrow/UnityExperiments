using UnityEngine;
using System.Collections;

public class MakeMoreBox : MonoBehaviour
{
	public bool readyForNext;
	GameObject cube;
	Vector3 pos;
	public MakeMoreBox thing;
	public Vector3[] sides;
	public bool placeIsGood;
	public bool placeDetermined;
	public Color start;
	public int stackOverflowPreventer;
	public bool moving;
	public Color old = Color.red;

	// Use this for initialization
	void Start ()
	{

		//Camera.main.transform.LookAt (transform.position);
		sides = new[] 
		   {new Vector3 (1, 1, 1), 
			new Vector3 (-1, 1, 1), 
			new Vector3 (-1, -1, 1),  
			new Vector3 (-1, -1, -1),  //4
			new Vector3 (1, -1, -1),  
			new Vector3 (1, 1, -1),  
			new Vector3 (1, -1, 1),  
			new Vector3 (-1, 1, -1), //8
			new Vector3 (0, -1, 1),  
			new Vector3 (0, 1, -1),  
			new Vector3 (0, -1, -1),  
			new Vector3 (0, -1, -1), //12  
			new Vector3 (-1, 0, 1),  
			new Vector3 (1, 0, -1),  
			new Vector3 (-1, 0, -1),  
			new Vector3 (1, 0, 1),  //16
			new Vector3 (-1, 1, 0),  
			new Vector3 (1, -1, 0),  
			new Vector3 (-1, -1, 0),  
			new Vector3 (1, 1, 0),  //20
			new Vector3 (0, 0, 1),  
			new Vector3 (0, 0, -1),  
			new Vector3 (1, 0, 0),  
			new Vector3 (-1, 0, 0),  //24
			new Vector3 (0, 1, 0),  
			new Vector3 (0, -1, 0)};  //26
		pos = transform.position;
		//cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		//cube.name = "Coob";
		//Instantiate (cube, pos, transform.rotation);
		//readyForNext = true;
		thing.name = "boop";
		Rigidbody gameObjectsRigidBody = thing.gameObject.AddComponent<Rigidbody>();

		placeIsGood = true;
		placeDetermined = false;
		moving = true;

		//if (old == Color.red) {
			float r = Random.Range (0.1f, 1);
			float g = Random.Range (0.1f, 1);
			float b = Random.Range (0.1f, 1);
			start = new Color (r, g, b);
			GetComponent<Renderer>().enabled = true;
			GetComponent<Renderer>().material.color = start;
		//} else {
		//	old.g += 0.001f;
		//	start = old;
		//	renderer.enabled = true;
		//	renderer.material.color = start;
		//}

		StartCoroutine (CubeWait ());

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (readyForNext) {


			//pos.x += Random.Range (-1,2);
			//pos.y += Random.Range (-1,2);
			//pos.z += Random.Range (-1,2);
			while (!placeDetermined) {
				pos = transform.position;
				int randomIndex = Random.Range (0, 25);
				pos = pos + sides [randomIndex];

				Collider[] thingsThere = Physics.OverlapSphere (pos, 0.4f);
				int p = 0;
				while (p < thingsThere.Length) {
					placeIsGood = false;
					p++;
				}

				if (placeIsGood 
				    && pos.x < 40 
				    && pos.x > -40
				    && pos.y < 40 
				    && pos.y > -15
				    && pos.z < 40 
				    && pos.z > -40) {
					placeDetermined = true;
				}else{
					placeIsGood = true;
				}

				stackOverflowPreventer++;
				if(stackOverflowPreventer > 100){
					placeDetermined = true;
					stackOverflowPreventer = 0;
				}
			}

			MakeMoreBox clone;
			clone = Instantiate (thing, pos, transform.rotation) as MakeMoreBox;
			//StartCoroutine (CubeWait ());

			clone.old = start;
			readyForNext = false;

			/*
			MakeMoreBox other = gameObject.GetComponent<MakeMoreBox>();
			if(other != null)
			{
				Destroy(other);
			}
			*/
		}


		pos.y -= 0.1f;
		if (moving) {
			//transform.position = pos;
		}
	}

	IEnumerator CubeWait ()
	{
		readyForNext = false;
		yield return new WaitForSeconds (0.00001f);
		readyForNext = true;
	}

	/*
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Cube" || other.tag == "Ground") {
			Debug.Log ("sdg");
			moving = false;
		}
	}
*/
	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Cube" || collision.gameObject.tag == "Ground") {
			Destroy(GetComponent<Rigidbody>());
			moving = false;


		}
	}

}



