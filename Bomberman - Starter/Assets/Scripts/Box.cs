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
         * 2. Notifies the Itemspawner that it got destroyed
		 * 3. Destroys the player GameObject.
		 */
		if (other.CompareTag("Explosion")) {
			exploded = true; // 1
		    ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
		    itemSpawner.BoxDestroyed(this); //2
            Destroy(gameObject); // 3
		}
	}
	
}
