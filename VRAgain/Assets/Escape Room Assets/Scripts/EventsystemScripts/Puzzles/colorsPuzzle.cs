using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorsPuzzle : MonoBehaviour {

    public bool started;
    public bool active;
    public bool solved;
    public bool hintGiven;
    public bool cantFail;

    public AudioSource randomCode;
    public int codeNumber;

    public AudioSource StartColor;
    public AudioSource FirstPress;
    public AudioSource SecondPress;
    public AudioSource[] ColorHints;
    public AudioSource[] ColorFail;
    public AudioSource[] ColorSucceed;

    public BetterButton bButt;
    public fourthEvent fourthE;
    public EventScript2 es2;
    public LockPoint lockP;
    public CodeLock cLock;
    public GameObject codeDrive;
    public GameObject colorPop;
    public int hintManage = 0;
    public int failManage = 0;
    public GameObject ButtonPressing; //holds all buttons
    public Animator dormantAnim;
    public Animator activeAnim;
    public Animator[] sevenAnim = new Animator[7];

    public IEnumerator Begin()
    {
        Debug.Log("Color puzzle start");
        started = true;
        cantFail = true;
        fourthE.ActiveSetter(4); // 4 for active setter 
        bButt.buttonActive = true;
        RandomizeCode();
        es2.currentStop();
        es2.currentAudio = StartColor;
        StartColor.Play(0);
        bButt.sevenAnimWhite();
        StartCoroutine(TrackIfOff());
        yield return new WaitUntil(() => bButt.buttonPushed == true && StartColor.isPlaying == false);
        es2.currentStop();
        es2.currentAudio = FirstPress;
        FirstPress.Play(0);
        yield return new WaitUntil(() => bButt.colorArray[0] != 0 && FirstPress.isPlaying == false);
        es2.currentStop();
        es2.currentAudio = SecondPress;
        SecondPress.Play(0);
        yield return new WaitUntil(() => SecondPress.isPlaying == false); //fucked with this a bit
        cantFail = false;
        yield return new WaitUntil(() => bButt.rightArray == true);
        es2.SuccessBeep();
        cantFail = true;
        es2.currentStop();
        es2.currentAudio = ColorSucceed[0];
        ColorSucceed[0].Play(0);
        createCodeDrive();
        yield return new WaitUntil(() => cLock.hintGiven == true && fourthE.patiPuz.PlugIn[0].isPlaying == false);
        yield return new WaitForSeconds(1f);
        es2.currentStop();
        es2.currentAudio = ColorSucceed[1];
        ColorSucceed[1].Play(0);
        yield return new WaitUntil(() => ColorSucceed[1].isPlaying == false);
        solved = true;
    }

    public IEnumerator TrackIfOff()
    {
        if (es2.eventSystem == 4)
        {
            yield return new WaitUntil(() => lockP.transform.parent == null);
            bButt.buttonActive = false;
            bButt.sevenAnimEnd();
            if (ButtonPressing.activeSelf == true) // if not dormant
            {
                ButtonPressing.SetActive(false);
                activeAnim.SetTrigger("Press");
                dormantAnim.SetTrigger("Press");
            }
            yield return new WaitUntil(() => lockP.parentName == "ColorsTerminal");
            bButt.buttonActive = true;
            bButt.sevenAnimWhite();
            StartCoroutine(TrackIfOff());
        }
    }

    public void FailPlay()
    {
        if (failManage <= 2 && solved == false && cantFail == false)
        {
            Debug.Log("color puzzle failure");
            es2.currentStop();
            es2.currentAudio = ColorFail[failManage];
            ColorFail[failManage].Play(0);
            failManage++;
        }
    }

    public void createCodeDrive()
    {
        codeDrive.SetActive(true);
        cLock.finalHintAudio = randomCode;
        cLock.codeNumber = codeNumber;
    }

    public void RandomizeCode()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                codeNumber = 5;
                break;

            case 1:
                codeNumber = 4;
                break;

            case 2:
                codeNumber = 3;
                break;

            case 3:
                codeNumber = 2;
                break;
        }
        randomCode = fourthE.randomCodes[rand];
    }

    public void Hint()
    {
        if (hintGiven == false && solved == false)
        {
            Debug.Log("hint given");
            StartCoroutine(HintExtend());
            hintGiven = true;
        }
        else
        {
            Debug.Log("hint already given!");
        }
    }

    public IEnumerator HintExtend()
    {
        if (es2.currentAudio.isPlaying == true)
        {
            yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
        }
        es2.currentAudio = ColorHints[hintManage];
        ColorHints[hintManage].Play(0);
        yield return new WaitForSeconds(15f);
        hintGiven = false;
        if (hintManage == 1)
        {
            hintManage--;
        }
        else if (hintManage == 0)
        {
            hintManage++;
        }
    }
}
