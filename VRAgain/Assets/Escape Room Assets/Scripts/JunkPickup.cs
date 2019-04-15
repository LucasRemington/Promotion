using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkPickup : MonoBehaviour
{
    public GameObject hiddenObj;
    public GameObject thisObj;
    public brokenPuzzle brokPuz;
    public EventScript2 es2;
    public bool pickedUp;
    public bool isMotherboard;
    public bool isFan;
    public bool isDisc;
    //public int pluggedIn;

    private void Start()
    {
        thisObj = this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && pickedUp == false && brokPuz.started == true && brokPuz.solved == false)
        {
            if (isMotherboard == true || isDisc == true || isFan == true)
            {
                es2.SuccessBeep();
                StartCoroutine(WaitForSilence());
            }
            else if (brokPuz.junkNumber <= 4)
            {
                pickedUp = true;
                brokPuz.junkNumber++;
                es2.currentStop();
                es2.currentAudio = brokPuz.JunkPickup[brokPuz.junkNumber];
                brokPuz.JunkPickup[brokPuz.junkNumber].Play(0);
            }
        }
        if (other.gameObject.CompareTag("Broken") && brokPuz.started == true && brokPuz.solved == false)
        {
            brokPuz.repairsIncorp.Play(0);
            this.transform.parent = other.transform;
            if (isMotherboard == true)
            {
                brokPuz.boardYes = true;
                kill();
                /*this.transform.localPosition = new Vector3(0.1f, 0.125f, 0.1f);
                this.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
                this.GetComponent<Rigidbody>().isKinematic = true;*/
            }
            if (isFan == true)
            {
                brokPuz.fanYes = true;
                kill();
                /* this.transform.localPosition = new Vector3(-0.1f, 0.25f, 0.1f);
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                this.GetComponent<Rigidbody>().isKinematic = true;*/
            }
            if (isDisc == true)
            {
                brokPuz.discYes = true;
                kill();
                /*this.transform.localPosition = new Vector3(0.1f, 0.225f, 0.06f);
                this.GetComponent<Rigidbody>().isKinematic = true;
                this.transform.localRotation = Quaternion.Euler(0, 180f, 0);*/
            }
        }
    }

    public void kill ()
    {
        hiddenObj.SetActive(true);
        Destroy(thisObj);
    }

    public IEnumerator WaitForSilence()
    {
        yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
        if (isMotherboard == true)
        {
            pickedUp = true;
            es2.currentStop();
            es2.currentAudio = brokPuz.MotherboardPickup;
            brokPuz.MotherboardPickup.Play(0);
        }
        else if (isFan == true)
        {
            pickedUp = true;
            es2.currentStop();
            es2.currentAudio = brokPuz.FanPickup;
            brokPuz.FanPickup.Play(0);
        }
        else if (isDisc == true)
        {
            pickedUp = true;
            es2.currentStop();
            es2.currentAudio = brokPuz.DiscPickup;
            brokPuz.DiscPickup.Play(0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cantele") && brokPuz.droppedImportant == false && (isMotherboard == true|| isFan == true || isDisc == true) && brokPuz.solved == false)
        {
            Debug.Log("hit cantele");
            brokPuz.droppedImportant = true;
            es2.currentStop();
            es2.currentAudio = brokPuz.DropPickup;
            brokPuz.DropPickup.Play(0);
        }
    }

 }