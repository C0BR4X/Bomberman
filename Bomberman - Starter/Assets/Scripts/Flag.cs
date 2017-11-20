using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{

    [SerializeField] [Range(1, 2)] private int team; //1 for red, 2 for blue
    private Player player;
    private bool taken;
    private bool log;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (taken)
	    {
	        transform.position = player.gameObject.transform.position - new Vector3(0.4f, 0, 0);
	    }
	    if (log && team == 2)
	    {
	        Debug.Log(this.tag +" "+ this.gameObject.transform.position);
	    }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();

            if (player.playerNumber != team && !taken)
            {
                player.TakeFlag(this);
                taken = true;
                GetComponent<Collider>().enabled = false;
            }

            if (player.playerNumber == team && player.carryFlag && !taken)
            {
                player.DeliveredFlag();
            }
        }
        
    }

    public void Dropped()
    {
        this.player = null;
        taken = false;
        GetComponent<Collider>().enabled = true;
    }

    public void GoHome()
    {
        this.gameObject.transform.position = new Vector3(1,0,7);
        log = true;
    }
}
