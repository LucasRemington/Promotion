using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatedPuzzle : MonoBehaviour {

    public bool started;
    public bool active;
    public bool solved;
    public bool hintGiven;
    public bool coolTerminal;

    public AudioSource randomCode;
    public int codeNumber;

    public fourthEvent fourthE;
    public secondEvent secondE;
    public EventScript2 es2;
    public LockPoint lockP;
    public CodeLock cLock;

    public AudioSource OwFuck;
    public AudioSource StartHeat;
    public AudioSource HintsHeat;
    public AudioSource WateronHotHeat;
    public AudioSource[] CryoPluginHeat;
    public AudioSource WaterFreezeHeat;
    public AudioSource WaterFreezeAlt;
    public AudioSource FreezeFinishHeat;
    public AudioSource[] SuccessHeat;

    public GameObject AI;
    public GameObject codeDrive;
    public GameObject cryoTest;

    public int cryoVoice = 0;
    public bool canCryo;

    public IEnumerator Begin()
    {
        Debug.Log("Heated puzzle start");
        started = true;
        cryoTest.SetActive(true);
        fourthE.ActiveSetter(1); // 1 for active setter 
        RandomizeCode();
        es2.currentStop();
        es2.currentAudio = OwFuck;
        OwFuck.Play(0);
        lockP.fireOut();
        yield return new WaitForSeconds(2.5f);
        es2.currentStop();
        es2.currentAudio = StartHeat;
        StartHeat.Play(0);
        yield return new WaitUntil(() => StartHeat.isPlaying == false);
        fourthE.ActiveSetter(1); // 1 for active setter 
        Debug.Log("bucket check");
        yield return new WaitUntil(() => coolTerminal == true);
        es2.SuccessBeep();
        fourthE.ActiveSetter(1); // 1 for active setter 
        es2.currentStop();
        es2.currentAudio = SuccessHeat[0];
        SuccessHeat[0].Play(0);
        createCodeDrive();
        yield return new WaitUntil(() => cLock.hintGiven == true && fourthE.patiPuz.PlugIn[0].isPlaying == false);
        yield return new WaitForSeconds(1f);
        es2.currentStop();
        es2.currentAudio = SuccessHeat[1];
        SuccessHeat[1].Play(0);
        yield return new WaitUntil(() => SuccessHeat[1].isPlaying == false);
        solved = true;
        secondE.cryoDoor.SetTrigger("close");
        secondE.doorHiss.Play(0);
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

    public void createCodeDrive ()
    {
        codeDrive.SetActive(true);
        cLock.finalHintAudio = randomCode;
        cLock.codeNumber = codeNumber;
    }

    public IEnumerator CryoVoice()
    {
        if (cryoVoice == 0 && canCryo == false && cLock.isActiveAndEnabled == false)
        {
            cryoVoice++;
            es2.currentStop();
            secondE.cryoDoor.SetTrigger("open");
            secondE.doorHiss.Play(0);
            es2.currentAudio = CryoPluginHeat[0];
            CryoPluginHeat[0].Play(0);
            yield return new WaitUntil(() => CryoPluginHeat[0].isPlaying == false);
            cryoVoice++;
        } else if (cryoVoice == 2 && canCryo == false && cLock.isActiveAndEnabled == false)
        {
            cryoVoice++;
            es2.currentStop();
            es2.currentAudio = CryoPluginHeat[1];
            CryoPluginHeat[1].Play(0);
            yield return new WaitUntil(() => CryoPluginHeat[1].isPlaying == false);
        }
    }

    public void Hint()
    {
        if (hintGiven == false && solved == false)
         {
         Debug.Log("hint given");
         StartCoroutine(HintExtend());
         hintGiven = true;
         } else{
         Debug.Log("hint already given!");
         }
     }

    public IEnumerator HintExtend ()
    {
        if (es2.currentAudio.isPlaying == true)
        {
            yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
        }
        es2.currentAudio = HintsHeat;
        HintsHeat.Play(0);
        yield return new WaitForSeconds(45f);
        hintGiven = false;
    }
}
