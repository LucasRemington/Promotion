using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenPuzzle : MonoBehaviour {

    public bool started;
    public bool active;
    public bool solved;
    public bool hintGiven;

    public AudioSource randomCode;
    public int codeNumber;

    public fourthEvent fourthE;
    public EventScript2 es2;
    public LockPoint lockP;
    public int junkNumber;
    public bool droppedImportant;

    public AudioSource StartBroken;
    public AudioSource[] BrokenHints;
    public AudioSource[] JunkPickup;
    public AudioSource DropPickup;
    public AudioSource MotherboardPickup;
    public AudioSource DiscPickup;
    public AudioSource FanPickup;
    public AudioSource[] BrokenSuccess;
    public AudioSource repairsIncorp;

    public bool fanYes;
    public bool discYes;
    public bool boardYes;

    public CodeLock cLock;
    public GameObject codeDrive;
    public int hintManage = 0;

    public IEnumerator Begin()
    {
        Debug.Log("Broken puzzle start");
        started = true;
        fourthE.ActiveSetter(3); // 3 for active setter 
        RandomizeCode();
        es2.currentStop();
        es2.currentAudio = StartBroken;
        StartBroken.Play(0);
        yield return new WaitUntil(() => fanYes == true && discYes == true && boardYes == true);
        es2.SuccessBeep();
        repairsIncorp.Stop();
        es2.currentStop();
        es2.currentAudio = BrokenSuccess[0];
        BrokenSuccess[0].Play(0);
        createCodeDrive();
        yield return new WaitUntil(() => cLock.hintGiven == true && fourthE.patiPuz.PlugIn[0].isPlaying == false);
        yield return new WaitForSeconds(1f);
        es2.currentStop();
        es2.currentAudio = BrokenSuccess[1];
        BrokenSuccess[1].Play(0);
        yield return new WaitUntil(() => BrokenSuccess[1].isPlaying == false);
        solved = true;
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
        es2.currentAudio = BrokenHints[hintManage];
        BrokenHints[hintManage].Play(0);
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
