using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPoint : MonoBehaviour {

    public Rigidbody rb;

    //Relevant scripts for puzzles and general evejnts
    public EventScript2 es2;
    public fourthEvent fourthE;
    public heatedPuzzle heatPuz;
    public patiencePuzzle patiPuz;
    public colorsPuzzle colrPuz;
    public brokenPuzzle brokPuz;

    //Relevant bools and sounds for plugging into and dropping out of terminals
    public bool triggerPress;
    public bool locked;
    public bool hintGiven;
    public AudioSource lockIn;
    public AudioSource lockOut;

    public AudioSource fireOt;
    public Rigidbody roomCenter;
    public float fireForce;
    public AudioSource mainTerminal;
    public bool mtPlayed;
    public bool cantPull;

    public string parentName;  //String that holds name of current parent

    public bool playerTouch; //Bool checks if player has touched it yet

    void Update()
    {
        if (locked == true && transform.parent != null && parentName == "Watch")
        {
            this.transform.localPosition = new Vector3(0, 0, 9f);
            this.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (locked == true && transform.parent != null && (parentName == "StartTerminal" || parentName == "HeatedTerminal" || parentName == "CryoTerminal" || parentName == "BrokenTerminal"))
        {
            this.transform.localPosition = new Vector3(0, 0, 0.005f);
            this.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (locked == true && transform.parent != null && parentName == "ColorsTerminal")
        {
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.localRotation = Quaternion.Euler(-180, -180, 90);
        }
        else if (locked == true && transform.parent != null && parentName == "TimedTerminal")
        {
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.localRotation = Quaternion.Euler(0, 360, 90);
        }
        if (transform.parent != null)
        {
            parentName = transform.parent.name;
        } else
        {
            parentName = "null";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cantPull == false)
        {
            Debug.Log("locked");
            if (playerTouch == false)
            {
                playerTouch = true;
                es2.SuccessBeep();
            }
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            if (lockOut.isPlaying == false && locked == true)
            {
                Debug.Log("play shutdown");
                lockOut.Play(0);
            }
            locked = false;
            //hintGiven = false;
            this.transform.parent = null;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AILocker") && triggerPress == true)
        {
            this.transform.parent = other.transform;
            Debug.Log("locked");
            if (lockIn.isPlaying == false && locked == false)
            {
                Debug.Log("play lockin");
                lockIn.Play(0);
            }
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = false;
            locked = true;
            if (es2.eventSystem == 4)
            {
                if (parentName == "HeatedTerminal")
                {
                    Debug.Log("heated terminal exists");
                    if (es2.eventSystem == 4)
                    {
                        fourthE.ActiveSetter(1);
                    }
                    if (heatPuz.started == false)
                    {
                        Debug.Log("heated terminal start");
                        StartCoroutine(heatPuz.Begin());
                    }
                    else if (heatPuz.started == true && heatPuz.coolTerminal == false)
                    {
                        fireOut();
                    }
                }
                else if (parentName == "TimedTerminal")
                {
                    if (es2.eventSystem == 4)
                    {
                        fourthE.ActiveSetter(2);
                    }
                    if (patiPuz.started == false)
                    {
                        Debug.Log("timed terminal start");
                        StartCoroutine(patiPuz.Begin());
                    }
                }
                else if (parentName == "BrokenTerminal")
                {
                    if (es2.eventSystem == 4)
                    {
                        fourthE.ActiveSetter(3);
                    }
                    if (brokPuz.started == false)
                    {
                        Debug.Log("broken terminal start");
                        StartCoroutine(brokPuz.Begin());
                    }
                }
                else if (parentName == "ColorsTerminal")
                {
                    if (es2.eventSystem == 4)
                    {
                        fourthE.ActiveSetter(4);
                    }
                    if (colrPuz.started == false)
                    {
                        Debug.Log("colors terminal start");
                        StartCoroutine(colrPuz.Begin());
                    }
                }
                else if (parentName == "CryoTerminal")
                {
                    if (es2.eventSystem == 4)
                    {
                        fourthE.ActiveSetter(1);
                        StartCoroutine(heatPuz.CryoVoice());
                    }
                }
                else if (parentName == "StartTerminal" && mtPlayed == false)
                {
                    if (es2.eventSystem == 4)
                    {
                        mtPlayed = true;
                        mainTerminal.Play(0);
                    }
                }
                else if (parentName == "Watch") // heat = 1 patience = 2 broken = 3 colors = 4
                {
                    if (fourthE.currentActivePuzzle == 1)
                    {
                        heatPuz.Hint();
                        heatPuz.hintGiven = true;
                    } else if (fourthE.currentActivePuzzle == 2)
                    {
                        patiPuz.Hint();
                        patiPuz.hintGiven = true;
                    }
                    else if (fourthE.currentActivePuzzle == 3)
                    {
                        brokPuz.Hint();
                        brokPuz.hintGiven = true;
                    }
                    else if (fourthE.currentActivePuzzle == 4)
                    {
                        colrPuz.Hint();
                        colrPuz.hintGiven = true;
                    }
                }
            }
        }
    }

    public void triggerInpt()
    {
        StartCoroutine(triggerInput());
    }

    public IEnumerator triggerInput()
    {
        triggerPress = true;
        yield return new WaitForSeconds(0.1f);
        triggerPress = false;
    }

    public void fireOut() //fucked with this a bit
    {
        Debug.Log("fire out");
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        fireOt.Play(0);
        locked = false;
        this.transform.parent = null;
        rb.AddForce((roomCenter.position - transform.position) * fireForce);
        if (heatPuz.StartHeat.isPlaying == false)
        {
            es2.currentStop();
            es2.currentAudio = heatPuz.OwFuck;
            heatPuz.OwFuck.Play(0);
        }
    }
}
