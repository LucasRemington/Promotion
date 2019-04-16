using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVanish : MonoBehaviour {

    public MeshRenderer hand;
    public GameObject theHand;

    private void Update()
    {
        if (theHand.transform.childCount == 0)
        {
            handAppear();
        }
    }

    public void handVanish () {
        hand.enabled = false;
	}

    public void handAppear()
    {
        hand.enabled = true;
    }
}
