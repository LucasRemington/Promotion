using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugMaker : MonoBehaviour {

    public GameObject pill;
    public static int pillCount;
    public static bool drugExists;
    public bool drugTime;
    public bool firstPill = false;
    public bool strayMice;
    public int timeUntilDrug;
    public ScriptedEvents ScriptedEvents;
    public Drugs Drugs;

    public Transform insTarget;
    public Animator handle;
    public AudioSource makePill;
    public AudioSource deny;

    void Start () {
        //MakeDrug();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (drugExists == false & drugTime == false)
            {
                MakeDrug();
                if (firstPill == false)
                {
                    firstPill = true;
                }
            } else
            {
                deny.Play(0);
            }
        }
    }

    void MakeDrug () {
        Instantiate(pill, new Vector3(insTarget.position.x, insTarget.position.y, insTarget.position.z), Quaternion.identity); 
        pillCount++;
        makePill.Play(0);
        drugExists = true;
        StartCoroutine(DrugCounter());
    }

    IEnumerator DrugCounter()
    {
        handle.SetBool("Up", false);
        drugTime = true;
        yield return new WaitForSeconds(timeUntilDrug);
        drugTime = false;
        handle.SetBool("Up", true);
    }

    public IEnumerator OutOfControl ()
    {
        Instantiate(pill, new Vector3(insTarget.position.x, insTarget.position.y, insTarget.position.z), Quaternion.identity);
        Drugs.OutOfControl = true;
        pillCount++;
        makePill.Play(0);
        yield return new WaitForSeconds(0.1f);
        if (strayMice == false)
        {
            StartCoroutine(OutOfControl());
        }
    }
}
