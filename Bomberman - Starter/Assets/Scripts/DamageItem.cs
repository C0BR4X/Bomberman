using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : MonoBehaviour {

	// Use this for initialization
	void Start() {}

	// Update is called once per frame
	void Update () {}
	
	public void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			var player = other.GetComponent<Player>();
			player.explosionRange++;
			Destroy(this.gameObject);
		}
	}
}
