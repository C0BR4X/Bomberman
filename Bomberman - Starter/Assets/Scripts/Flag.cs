using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{

    [SerializeField] [Range(1, 2)] private int team; //1 for red, 2 for blue
    private Player player;
    private bool taken;
    private bool home;

    private  Vector3 RED_HOME = new Vector3();

    // Use this for initialization
	void Start ()
	{
	    taken = false;
	    home = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (taken)
	    {
	        transform.position = player.gameObject.transform.position - new Vector3(0.4f, 0, 0);
	    }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();

            if (player.playerNumber != team && !taken && home)
            {
                player.TakeFlag(this);
                taken = true;
                home = false;
                GetComponent<Collider>().enabled = false;
            }

            if (player.playerNumber == team && player.carryFlag && !taken && home) //same team, delivered to home flag
            {
                player.DeliveredFlag();
            }

            if (player.playerNumber == team && !taken && !home)
            {
                GoHome();
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
        this.transform.localPosition = new Vector3(0,0,0);
        home = true;
    }
}
