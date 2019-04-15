using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patiencePuzzle : MonoBehaviour {

    public bool started;
    public bool active;
    public bool solved;
    public bool hintGiven;

    public AudioSource randomCode;
    public AudioSource computerNoise;
    public int codeNumber;

    public fourthEvent fourthE;
    public EventScript2 es2;
    public LockPoint lockP;
    public CodeLock cLock;

    public AudioSource beepBoop;
    public AudioSource StartPatience;
    public AudioSource[] PlugIn;
    public AudioSource[] PullOut;
    public AudioSource[] PatienceHints;
    public AudioSource[] SuccessPatience;

    public bool dontEnd;
    public bool stopOnce;
    public bool doneDecoding;
    public bool pulledOut;
    public int pulledInt = 0;
    public int hintManage = 0;
    public bool calmDown;

    public GameObject codeDrive;

    public IEnumerator Begin()
    {
        Debug.Log("Patience puzzle start");
        started = true;
        fourthE.ActiveSetter(2); // 2 for active setter 
        RandomizeCode();
        es2.currentStop();
        es2.currentAudio = StartPatience;
        StartPatience.Play(0);
        yield return new WaitUntil(() => StartPatience.isPlaying == false);
        StartCoroutine(MonologueManager());
        StartCoroutine(MonologueStopper());
    }

    public IEnumerator MonologueManager()
    {
        if (solved == false)
        {
            Debug.Log("monologue manager start");
            beepBoop.Play(0);
            if (pulledOut == true)
            {
                es2.currentStop();
                es2.currentAudio = PlugIn[1];
                PlugIn[1].Play(0);
                yield return new WaitUntil(() => PlugIn[1].isPlaying == false);
                yield return new WaitForSeconds(0.25f);
            }
            if (PullOut[0].isPlaying == false && PullOut[1].isPlaying == false) {
                es2.currentStop();
                es2.currentAudio = PlugIn[0];
                PlugIn[0].Play(0);
                yield return new WaitUntil(() => PlugIn[0].isPlaying == false);
                if (dontEnd == false)
                {
                    Debug.Log("Plugin stopped playing");
                    es2.SuccessBeep();
                    beepBoop.Stop();
                    es2.currentStop();
                    es2.currentAudio = SuccessPatience[0];
                    SuccessPatience[0].Play(0);
                    createCodeDrive();
                    calmDown = true;
                    yield return new WaitUntil(() => cLock.hintGiven == true);
                    yield return new WaitForSeconds(1f);
                    es2.currentStop();
                    es2.currentAudio = SuccessPatience[1];
                    SuccessPatience[1].Play(0);
                    yield return new WaitUntil(() => SuccessPatience[1].isPlaying == false);
                    solved = true;
                }
                else
                {
                    dontEnd = false;
                }
            }
        }
    }

    public IEnumerator MonologueStopper()
    {
        yield return new WaitUntil(() => lockP.locked == false);
        if (solved == false && codeDrive.activeInHierarchy == false)
        {
            Debug.Log("Plugin pulled out");
            beepBoop.Stop();
            dontEnd = true;
            pulledOut = true;
            yield return new WaitForSeconds(0.01f);
            PlugIn[1].Stop();
            PlugIn[0].Stop();
            if (pulledInt == 0 && calmDown == true)
            {
                es2.currentAudio = PullOut[0];
                Debug.Log("pull out played");
                PullOut[0].Play(0);
                pulledInt++;
                yield return new WaitUntil(() => PullOut[0].isPlaying == false && lockP.parentName == "TimedTerminal" && lockP.transform.parent != null);
            }
            else if (pulledInt == 1 && calmDown == true)
            {
                es2.currentAudio = PullOut[1];
                Debug.Log("pull out played");
                PullOut[1].Play(0);
                pulledInt++;
                yield return new WaitUntil(() => PullOut[1].isPlaying == false && lockP.parentName == "TimedTerminal" && lockP.transform.parent != null);
            } else
            {
                yield return new WaitUntil(() => lockP.parentName == "TimedTerminal" && lockP.transform.parent != null);
            }
            StartCoroutine(MonologueManager());
            StartCoroutine(MonologueStopper());
        }
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

    public void createCodeDrive()
    {
        codeDrive.SetActive(true);
        cLock.finalHintAudio = randomCode;
        cLock.codeNumber = codeNumber;
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
        es2.currentAudio = PatienceHints[hintManage];
        PatienceHints[hintManage].Play(0);
        yield return new WaitForSeconds(15f);
        hintGiven = false;
        if (hintManage == 1)
        {
            hintManage--;
        } else if (hintManage == 0)
        {
            hintManage++;
        }
    }

    public IEnumerator RobotNoises()
    {
        yield return new WaitUntil(() => PlugIn[0].isPlaying == true || solved == true);
        if (solved == false)
        {
            computerNoise.Play(0);
            yield return new WaitUntil(() => PlugIn[0].isPlaying == false);
            computerNoise.Stop();
            StartCoroutine(RobotNoises());
        }
    }
}
