using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVanish : MonoBehaviour {

    public MeshRenderer hand;

    private void Update()
    {
        if (this.transform.childCount == 3)
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
