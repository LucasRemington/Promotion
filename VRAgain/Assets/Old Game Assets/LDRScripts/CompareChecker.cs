using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareChecker : MonoBehaviour {

    public ButtonCompare[] buttComp;
    public Work Work;
    public bool beginChecking;

	// Use this for initialization
	void Start () {
        beginChecking = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (buttComp[1].isSame == true && buttComp[2].isSame == true && buttComp[3].isSame == true && buttComp[4].isSame == true && buttComp[5].isSame == true && buttComp[0].isSame == true && beginChecking == true)
        {
            Debug.Log("Checked");
            Work.EndWork();
            beginChecking = false;
        }
	}
}
