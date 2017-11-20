using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class Heart : MonoBehaviour {
	
	[SerializeField] private float amplitude = 0.003f;

	[SerializeField] private float frequency = 1;

	private Vector3 posStart = new Vector3();
	private Vector3 tempPos = new Vector3();
	
	
	// Use this for initialization
	void Start () {
		
		//Store the starting position	
		posStart = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		tempPos = transform.position;
		tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;
		
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<Player>();
			player.life++;
			Destroy(this.gameObject);
		}
	}
}
