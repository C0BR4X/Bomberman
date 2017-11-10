using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	// adds explosion object
	public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
		
		/*
		 * Invoke() takes 2 parameters: - firstly: the name of the method you want to be called
		 * 								- secondly: the delay before it gets called.
		 * In this case, you we to make the bomb explode in three seconds, so we call Explode()
		 */
		Invoke("Explode", 3f);	
	}
	
	// Update is called once per frame
	void Update () {
		void Explode() {
			
		}

	}
}
