using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondEvent : MonoBehaviour {

    //Opens first door, checks to see when player teleports, plays second AI voice, closes door behind player.

    public Animator cryoDoor;
    public EventScript2 es2;
    public bool doorActive;

    public AudioSource doorHiss;

    public IEnumerator Begin()
    {
        es2.eventSystem = 2;
        Debug.Log("begin event 2");
        cryoDoor.SetTrigger("open");
        doorHiss.Play(0);
        doorActive = true;
        yield return new WaitUntil(() => doorActive == false);
        es2.smallVictory.Play(0);
        yield return new WaitUntil(() => es2.smallVictory.isPlaying == false);
        Debug.Log("playvoice2");
        es2.AIVoice[1].Play(0);
        es2.currentAudio = es2.AIVoice[1];
        yield return new WaitUntil(() => es2.AIVoice[1].isPlaying == false);
        es2.switchCheck();
    }

    public void teleportOut()
    {
        if (es2.eventSystem == 2 && doorActive == true)
        {
            doorActive = false;
            cryoDoor.SetTrigger("close");
            doorHiss.Play(0);
        }
    }
}
