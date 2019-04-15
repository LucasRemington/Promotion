using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualButton : MonoBehaviour {

    //This script goes on each individual button.

    public GameObject ButtonPressing; //holds all buttons
    public GameObject Dormant; // for 'turning on' and 'turning off' animations

    public BetterButton bButt;
    public int color; //0 white 1 red 2 yellow 3 green 4 blue 5 dormant
    Animator anim;
    public Animator dormantAnim;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.name == "RightHand" && bButt.buttonActive == true)
        {
            bButt.ButtonPushed(color);
            anim.SetTrigger("Press");
        } else if (other.CompareTag("Player") && other.name == "RightHand")
        {
            bButt.DormantButtonPushed();
            anim.SetTrigger("DormantPress");
        }
    }

     public void Swap() // called by animation event
    {

        Debug.Log("buttonswap");
        if (ButtonPressing.activeSelf == true) // if not dormant
        {
            ButtonPressing.SetActive(false);
            anim.SetTrigger("Press");
            dormantAnim.SetTrigger("Press");
        }
        else
        {
            ButtonPressing.SetActive(true);
        }

    }
}
