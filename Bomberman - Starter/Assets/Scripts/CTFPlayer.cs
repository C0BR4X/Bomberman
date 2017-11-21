using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTFPlayer : Player {

    public bool carryFlag = false;
    private GameObject flag;

    void Update() {

        UpdateMovement();

        if (carryFlag)
        {
            flag.transform.position = transform.position - new Vector3(0.4f, 0, 0);
        }
    }

    private void DropFlag()
    {
        Flag flag = this.flag.GetComponent<Flag>();
        flag.Dropped();
        this.flag = null;
    }

    public void TakeFlag(Flag flag)
    {
        carryFlag = true;
        this.flag = flag.gameObject;
    }

    public void DeliveredFlag()
    {
        carryFlag = false;
        Flag flag = this.flag.GetComponent<Flag>();

        flag.Dropped();
        flag.GoHome();
        this.flag = null;

        GlobalManager.Scored(playerNumber);
    }

    public void gotHit()
    {
        Debug.Log("P" + playerNumber + " hit by explosion!");
        if (life == 1)
        {
            dead = true; // 1
            Destroy(gameObject); // 3
            GlobalManager.PlayerDied(playerNumber); // 2
            DropFlag();
        }
        life--;
    }


}
