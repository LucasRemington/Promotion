using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStopper : MonoBehaviour {

    public bool teleStop = false;
    public GameObject teleManager;

	// Use this for initialization
	void Update () {
		if (teleStop == true)
        {
            teleManager.SetActive(false);
        } else
        {
            teleManager.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (teleStop == true) {
                teleStop = false;
            } else {
                teleStop = true;
            }
        }
    }
}
