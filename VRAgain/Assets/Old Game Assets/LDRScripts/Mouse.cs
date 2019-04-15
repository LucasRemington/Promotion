using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public Animator anim;
    public Animator blendAnim;
    public Mover Mover;
    public GameObject mouseMesh;
    public ScriptedEvents ScriptedEvents;

    public AudioSource blender;
    public AudioSource mouseSqueak;
    public AudioSource mouseScreech;
    

	// Use this for initialization
	void Start () {
        anim.SetTrigger("Idle");
	}
	
	// Update is called once per frame
	void Update () {
        if (Mover.startMoving == true)
        {
            anim.SetTrigger("Walk");
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Squirm");
            Mover.startMoving = false;
            mouseSqueak.Play(0);
            StartCoroutine(waitForSqueak());
        } else if (other.gameObject.CompareTag("Blender"))
        {
            blender.Play(0);
            blendAnim.SetBool("blending", true);
            StartCoroutine(waitForBlend());
            mouseMesh.SetActive(false);
        } else if (other.gameObject.CompareTag("cantele"))
        {
            anim.SetTrigger("Walk");
            Mover.startMoving = true;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            if (Mover.loopNumber == 1)
            {
                transform.LookAt(Mover.target);
            } else if (Mover.loopNumber == 2)
            {
                transform.LookAt(Mover.target2);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mouseScreech.Stop();
            mouseSqueak.Play(0);
        }
    }

        IEnumerator waitForBlend ()
    {
        yield return new WaitUntil(() => blender.isPlaying == false);
        blendAnim.SetBool("blending", false);
        ScriptedEvents.mouseFlag = true;
        Destroy(this);
    }

        IEnumerator waitForSqueak()
    {
        yield return new WaitUntil(() => mouseSqueak.isPlaying == false);
        mouseScreech.Play(0);
    }
}
