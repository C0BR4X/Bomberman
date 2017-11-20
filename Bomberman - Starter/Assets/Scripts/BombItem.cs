using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour{
    
    // Use this for initialization
    void Start () {}
	
    // Update is called once per frame
    void Update () {}

    public void OnTriggerEnter(Collider other) {
	    if (other.CompareTag("Player")) {
		    var player = other.GetComponent<Player>();
		    player.bombs++;
		    Destroy(this.gameObject);
	    }
    }
}
