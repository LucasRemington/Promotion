using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdEvent : MonoBehaviour {

    //Plays third AI voice when player pulls it out of terminal. Waits until player plugs AI into watch, plays fourth AI voice.

    public EventScript2 es2;
    public GameTimer gameT;
    public LockPoint lockPoint;
    public AudioSource[] AIHint;
    public AudioSource terminalHint;
    public AudioSource shipStart;
    public AudioSource shipAmb;
    public bool noHints;
    int hint = 0;

    public IEnumerator Begin()
    {
        es2.eventSystem = 3;
        Debug.Log("begin event 3");
        StartCoroutine(HintTimer());
        StartCoroutine(TerminalTimer());
        yield return new WaitUntil(() => lockPoint.parentName == "Watch");
        lockPoint.cantPull = true;
        es2.SuccessBeep();
        Debug.Log("playvoice3");
        noHints = true;
        es2.currentStop();
        es2.currentAudio = es2.AIVoice[2];
        es2.AIVoice[2].Play(0);
        yield return new WaitForSeconds(13f);
        StartCoroutine(gameT.TimerCount());
        shipStart.Stop();
        shipAmb.Play(0);
        yield return new WaitUntil(() => es2.AIVoice[2].isPlaying == false);
        Debug.Log("timerstart");
        lockPoint.cantPull = false;
        es2.switchCheck();
    }

    // AI berates player after fifteen seconds have passed and they haven't picked him up
    public IEnumerator HintTimer()
    {
        Debug.Log("hintstarted");
        yield return new WaitForSeconds(20f);
        if (hint <= 2 && es2.eventSystem == 3)
        {
            if (es2.currentAudio.isPlaying == true)
            {
                yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
            }
            if (noHints == false)
            {
                es2.currentAudio = AIHint[hint];
                Debug.Log("hintgiven");
                AIHint[hint].Play(0);
                hint++;
                StartCoroutine(HintTimer());
            }
        }
    }

    // AI talks if plugged into another terminal
    public IEnumerator TerminalTimer()
    {
        Debug.Log("terminalhintstarted");
        yield return new WaitUntil(() => lockPoint.parentName == "HeatedTerminal" || lockPoint.parentName == "CryoTerminal" || lockPoint.parentName == "BrokenTerminal" || lockPoint.parentName == "TimedTerminal" || lockPoint.parentName == "ColorsTerminal" || es2.eventSystem != 3);
        if (es2.eventSystem == 3)
        {
            Debug.Log("terminalhintgiven");
            es2.currentStop();
            es2.currentAudio = terminalHint;
            terminalHint.Play(0);
        } else
        {
            Debug.Log("terminalhintexpired");
        }
    }

}
