using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	private bool exploded = false; // Has box exploded ?
	private Rigidbody rigidBody;
	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnTriggerEnter(Collider other) {
        
		/*
		 * 1. Sets the exploded variable
		 * 2. Destroys the player GameObject.
		 */
		if (exploded && other.CompareTag("Explosion")) {
			exploded = true; // 1
			Destroy(gameObject); // 2
		}
	}
	
}
