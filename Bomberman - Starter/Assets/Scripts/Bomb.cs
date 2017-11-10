using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	// adds explosion object
	public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		
		/*
		 * Invoke() takes 2 parameters: - firstly: the name of the method you want to be called
		 * 								- secondly: the delay before it gets called.
		 * In this case, you we to make the bomb explode in three seconds, so we call Explode()
		 */
		Invoke("Explode", 5f);	
	}
	
	// Update is called once per frame
	void Update () {
		Explode();
	}

	private void Explode()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1
        
        			GetComponent<MeshRenderer>().enabled = false; //2
        			transform.Find("Collider").gameObject.SetActive(false); //3
        			Destroy(gameObject, .5f); //4
	}
}
