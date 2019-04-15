using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstEvent : MonoBehaviour {

    //Fades in from black, plays initial AI speech. No input required.

    public EventScript2 es2;
    public Animator fadeIn;
    public GameObject vision;

	void Start () {
		
	}

    public IEnumerator Begin()
    {
        es2.eventSystem = 1;
        Debug.Log("begin event 1");
        yield return new WaitForSeconds(1.0f);
        es2.interCom.Play(0);
        yield return new WaitForSeconds(4.0f);
        es2.currentAudio = es2.AIVoice[0];
        es2.AIVoice[0].Play(0);
        yield return new WaitUntil(() => es2.interCom.isPlaying == false);
        fadeIn.SetTrigger("fade");
        yield return new WaitUntil(() => es2.AIVoice[0].isPlaying == false);
        vision.SetActive(false);
        es2.switchCheck();
    }
}
