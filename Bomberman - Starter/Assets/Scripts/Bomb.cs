using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	
	public GameObject explosionPrefab; // adds explosion object	
	public LayerMask levelMask; // A LayerMask selectively filters out certain layers	
    public LayerMask boxMask;
	private bool exploded = false; // check the colliding object
    private Player player;
    private int explosionRange = 3;

    // Use this for initialization
    void Start () {
		
		/*
		 * Invoke() takes 2 parameters: - firstly: the name of the method you want to be called
		 * 								- secondly: the delay before it gets called.
		 */
		Invoke("Explode", 3f);	
	}
	
	// Update is called once per frame
	void Update () {}
	
	private void Explode() {
	/*
	 * 1. Spawns an explosion at the bomb’s position.
     * 2. Disables the mesh renderer, making the bomb invisible.
     * 3. Disables the collider, allowing players to move through and walk into an explosion.
     * 4. Destroys the bomb after 0.3 seconds; this ensures all explosions will spawn before the GameObject is destroyed.
	 */
		Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1
		// The StartCoroutine calls will start up the CreateExplosions IEnumerator once for every direction
		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left)); 
		GetComponent<MeshRenderer>().enabled = false; //2
		exploded = true;
	    player.IncreaseBombAmount();
		transform.Find("Collider").gameObject.SetActive(false); //3
		Destroy(gameObject, .3f); //4
	}

	
	/*
	 * 1. Iterates a for loop for every unit of distance you want the explosions to cover.
	 * 	  In this case, the explosion will reach two meters.
	 * 2. A RaycastHit object holds all the information about what and at which position the Raycast hits -- or doesn't hit.
	 * 3. This important line of code sends out a raycast from the center of the bomb towards the direction you passed through the StartCoroutine call.
	 * 	  It then outputs the result to the RaycastHit object. The i parameter dictates the distance the ray should travel.
	 * 	  Finally, it uses a LayerMask named levelMask to make sure the ray only checks for blocks in the level and ignores the player and other colliders.
	 * 4. If the raycast doesn't hit anything then it's a free tile.
	 * 5. Spawns an explosion at the position the raycast checked.
	 * 6. The raycast hits a block.
	 * 7. Once the raycast hits a block, it breaks out of the for loop.
	 * 	  This ensures the explosion can't jump over walls.
	 * 8. Waits for 0.05 seconds before doing the next iteration of the for loop.
	 * 	  This makes the explosion more convincing by making it look like it's expanding outwards.
	 */
	private IEnumerator CreateExplosions(Vector3 direction)
	{
	    //1
	    for (int i = 1; i < explosionRange; i++) { 
			//2
			RaycastHit hit; 
			//3
			Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit, i, levelMask); 
			//4
			if (!hit.collider)
			{
			    Instantiate(explosionPrefab, transform.position + (i * direction),
			        //5 
			        explosionPrefab.transform.rotation);

                RaycastHit boxHit;

			    Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out boxHit, i, boxMask);


			    if (boxHit.collider)
			    {
                    break; //Prevents the explosion from going "through" the boxes
                }
			//6
			} else { 
				//7
				break; 
			}

			//8
			yield return new WaitForSeconds(.05f); 
		}
	}

	// OnTriggerEnter is a pre-defined method in a MonoBehaviour that gets called upon collision of a trigger collider and a rigidbody
	private void OnTriggerEnter(Collider other) {
		
		/*
		 * 1. Checks the the bomb hasn't exploded.
		 * 2. Checks if the trigger collider has the Explosion tag assigned.
		 * 3. Cancel the already called Explode invocation by dropping the bomb -- if you don't do this the bomb might explode twice.
		 * 4. Explode!
		 */
		if (!exploded && other.CompareTag("Explosion")) { // 1 & 2
			CancelInvoke("Explode"); // 3
			Explode(); // 4
		}
	}

    public void SetPlayerProperties(Player player, int range)
    {
        this.player = player;
        this.explosionRange = range;
    }
}

