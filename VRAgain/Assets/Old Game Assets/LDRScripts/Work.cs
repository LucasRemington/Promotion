using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour {

    //public Animator[] buttons;
    public Animator[] display;
    public int[] displayNumber;
   // public CompareChecker compChek;
    public ButtonCompare[] buttComp;
    //public Work Work;
    public bool beginChecking;
    public int amountWorked;
    public ScriptedEvents ScriptedEvents;
    public bool workLimit;
    public bool firstWord;
    private int x;
    public AudioSource[] talkingVoice;
    public bool muteAudio;
    public int audioMuter;

    // Use this for initialization
    void Start () {
        GenerateWork();
        beginChecking = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (buttComp[1].isSame == true && buttComp[2].isSame == true && buttComp[3].isSame == true && buttComp[4].isSame == true && buttComp[5].isSame == true && buttComp[0].isSame == true && beginChecking == true)
        {
            Debug.Log("Checked");
            EndWork();
            StartCoroutine(Timer());
        }
        audioMuter = 0;
        for (int i = 0; i < 12; i++)
        {
            
            if (ScriptedEvents.talkingVoice[i].isPlaying)
            {
                audioMuter++;
            }
        }
        if (audioMuter >= 1)
        {
            talkingVoice[x].mute = true;
        } else
        {
            talkingVoice[x].mute = false;
        }
        
    }

    public void EndWork ()
    {
        if (workLimit == false)
        {
            workLimit = true;
            StartCoroutine(workLimiter());
            GenerateWork();
            amountWorked++;
            x = Mathf.FloorToInt(Random.Range(0, 8));
            if (firstWord == false)
            {
                firstWord = true;
            }
            else
            {
                talkingVoice[x].Play(0);
            }
        }
    }

    public IEnumerator workLimiter ()
    {
        yield return new WaitForSeconds(1f);
        workLimit = false;
    }

    public IEnumerator Timer ()
    {
        Debug.Log("update called me");
        if (beginChecking == true)
        {
            beginChecking = false;
            yield return new WaitForSeconds(0.1f);
            beginChecking = true;
        } else
        {
            x = 0;
        }
    }

    void GenerateWork ()
    {

        for (int i = 0; i < 6; i++)
        {
            int RBY = Random.Range(0, 3);
            displayNumber[i] = RBY;
            if (RBY == 0)
            {
                display[i].SetTrigger("Red");
            } else if (RBY == 1)
            {
                display[i].SetTrigger("Blue");
            }
            else if (RBY == 2)
            {
                display[i].SetTrigger("Yellow");
            } else
            {
                display[i].SetTrigger("Red");
            }

        }
    }
}
