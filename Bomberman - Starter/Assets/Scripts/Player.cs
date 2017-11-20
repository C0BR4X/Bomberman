/*
 * Copyright (c) 2015 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    //Player parameters
    public GlobalStateManager GlobalManager; // Reference to the GlobalStateManager, a script that is notified of all player deaths and determines which player won
    [Range(1, 2)] // Enables a nifty slider in the editor
    public int playerNumber = 1; // Indicates what player this is: P1 or P2
    public float moveSpeed = 5f;
    public bool canDropBombs = true; // Can the player drop bombs?
    public bool canMove = true; // Can the player move?
    public bool dead = false; // Is player dead ?
    
    public int bombs = 2; //Amount of bombs the player has left to drop, gets decreased as the player drops a bomb, increases as an owned bomb explodes (maybe find better solution than public)
    public int explosionRange = 3; //Range of the bombs dropped by this player (maybe find better solution than public)
    private IUnityInput unityInput;
    public int life = 2; //Amount of life one player has (maybe find better solution than public)
    
    //Prefabs
    public GameObject bombPrefab;

    //Cached components
    private Rigidbody rigidBody;
    private Transform myTransform;
    public  Animator animator; //Maybe other solution than public

    private bool carryFlag = false;

    // Use this for initialization
    void Start() {
        //Cache the attached components for better performance and less typing
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.Find("PlayerModel").GetComponent<Animator>();
        unityInput = new UnityInput();
    }

    // Update is called once per frame
    void Update() {
        UpdateMovement();
    }

    private void UpdateMovement() {
        animator.SetBool("Walking", false);

        if (!canMove) { //Return if player can't move
            return;
        }

        //Depending on the player number, use different input for moving
        if (playerNumber == 1) {
            UpdatePlayer1Movement();
        }
        else {
            UpdatePlayer2Movement();
        }
    }

    /// <summary>
    /// Updates Player 1's movement and facing rotation using the WASD keys and drops bombs using Space
    /// </summary>
    private void UpdatePlayer1Movement() {
        if (unityInput.KeyPressed(KeyCode.W)) { //Up movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walking",true);
        }

        if (unityInput.KeyPressed(KeyCode.A)) { //Left movement
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walking", true);
        }

        if (unityInput.KeyPressed(KeyCode.S)) { //Down movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walking", true);
        }

        if (unityInput.KeyPressed(KeyCode.D)) { //Right movement
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walking", true);
        }

        if (canDropBombs && unityInput.KeyDown(KeyCode.Space)) { //Drop bomb
            DropBomb();
        }
    }

    /// <summary>
    /// Updates Player 2's movement and facing rotation using the arrow keys and drops bombs using Enter or Return
    /// </summary>
    private void UpdatePlayer2Movement() {
        if (unityInput.KeyPressed(KeyCode.UpArrow)) { //Up movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walking", true);
        }

        if (unityInput.KeyPressed(KeyCode.LeftArrow)) { //Left movement
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walking", true);
        }

        if (unityInput.KeyPressed(KeyCode.DownArrow)) { //Down movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walking", true);
        }

        if (unityInput.KeyPressed(KeyCode.RightArrow)) { //Right movement
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walking", true);
        }

        if (canDropBombs && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))) { //Drop Bomb. For Player 2's bombs, allow both the numeric enter as the return key or players without a numpad will be unable to drop bombs
            DropBomb();
        }
    }

    /// <summary>
    /// Drops a bomb beneath the player
    /// </summary>
    private void DropBomb() {
        if (bombPrefab && bombs > 0) { //Check if bomb prefab is assigned first
            var droppedBomb = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), 
                        bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
                        bombPrefab.transform.rotation).GetComponent<Bomb>();
            droppedBomb.SetPlayerProperties(this, explosionRange);
            bombs--;
        }
    }

    public void OnTriggerEnter(Collider other) {
        
        /*
         * 1. Sets the dead variable so you can keep track of the player's death.
         * 2. Notifies the global state manager that the player died.
         * 3. Destroys the player GameObject.
         */
        if (other.CompareTag("Explosion")) {
            Debug.Log("P" + playerNumber + " hit by explosion!");
            if(life == 1) {
                dead = true; // 1
                Destroy(gameObject); // 3
                GlobalManager.PlayerDied(playerNumber); // 2
            }
            life--;
        }

         if (playerNumber == 1)
        {
            if (other.CompareTag("Blue Flag"))
            {
                carryFlag = true;
            }

            if (other.CompareTag("Red Flag") && carryFlag)
            {
                GlobalManager.redScoresCTF();
                carryFlag = false;
            }
        }

        if (playerNumber == 2)
        {
            if (other.CompareTag("Blue Flag") && carryFlag)
            {
                GlobalManager.redScoresCTF();
                carryFlag = false;
            }

            if (other.CompareTag("Red Flag"))
            {
                carryFlag = true;
            }
        }
    }

    //for testing purposes
    public void Contruct(IUnityInput unityInput)
    {
        this.unityInput = unityInput;
    }

    public void IncreaseBombAmount()
    {
        bombs++;
    }
}