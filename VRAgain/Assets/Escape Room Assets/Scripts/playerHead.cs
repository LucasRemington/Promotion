using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHead : MonoBehaviour {

    public EventScript2 es2;
    public secondEvent secondE;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("door") && secondE.doorActive == true)
        {
            Debug.Log("passed through door");
            secondE.doorActive = false;
        }
    }
}
