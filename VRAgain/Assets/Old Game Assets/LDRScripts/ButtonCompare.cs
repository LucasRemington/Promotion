using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCompare : MonoBehaviour {

    public Animator buttonAnim;
    Animator anim;
    public bool isSame;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
    }

	public void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Red") && buttonAnim.GetCurrentAnimatorStateInfo(0).IsName("Red"))
        {
            isSame = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Yelo") && buttonAnim.GetCurrentAnimatorStateInfo(0).IsName("Yelo"))
        {
            isSame = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Blu") && buttonAnim.GetCurrentAnimatorStateInfo(0).IsName("Blu"))
        {
            isSame = true;
        }
        else
        {
            isSame = false;
        }

    }
}
