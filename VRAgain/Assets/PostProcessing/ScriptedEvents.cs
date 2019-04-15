using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI; 

public class ScriptedEvents : MonoBehaviour {

    public AudioSource[] talkingVoice;
    public AudioSource compMagic;
    public AudioSource doorSound;

    public AudioSource backgroundChatter;
    public AudioSource psychadelicMusic;
    public AudioSource scaryMusic;
    public AudioSource heavenlyChoir;

    public GameObject display;
    public GameObject mouseObj;
    public Mover door;
    public Mover mouse;
    public GameObject teleporter;
    public Work Work;
    public DrugMaker DrugMaker;
    public Drugs Drugs;
    public bool stopMiceTimer;
    public bool mouseFlag;

    public Image tint;
    public Text credits;
    public float tintTrans;
    public Animator rainbowImage;
    public Animator posterImage;

    //public GameObject OVRCamera1;
    //public GameObject OVRCamera2;

    public PostProcessingBehaviour PPB;
    public PostProcessingProfile drugs1;
    //public int hueShift;

    public int eventManage;
    public bool emLimit = true;
    public bool[] eventCalled;
    public bool event5Delayed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Begin());
        eventManage = 1;
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        for (int i = 0; i < 40; i++)
        {
            var tempColor = tint.color;
            tempColor.r = 0;
            tempColor.g = 0;
            tempColor.b = 0;
            tempColor.a = (2-(i*0.05f)); //1f makes it fully visible, 0f makes it fully transparent.
            tint.color = tempColor;
            yield return new WaitForSeconds(0.1f);  
        }
    }

    //Event1
    public IEnumerator Begin()
    {
        AudioListener.volume = 0;
        yield return new WaitForSeconds(0.5f);
        AudioListener.volume = 1;
        talkingVoice[0].Play(0);
        StartCoroutine(WaitForSound(talkingVoice[0]));
    }
    //Event1
    public IEnumerator WaitForSound(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[0].isPlaying == false);
        display.SetActive(true);
        Work.EndWork();
        eventManage++;
    }
    //Event2
    public IEnumerator WaitForSound2(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[1].isPlaying == false);
        teleporter.SetActive(true);
        door.startMoving = true;
        doorSound.Play(0);
        posterImage.SetTrigger("switch");
        eventManage++;
        //Work.EndWork();
    }
    //Event 3
    public IEnumerator WaitForSound3(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[2].isPlaying == false);
        eventManage++;
    }
    //Event 4
    public IEnumerator WaitForSound4(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[3].isPlaying == false);
        eventManage++;
    }
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(60f);
        event5Delayed = true;
    }
    //Event 5
    public IEnumerator WaitForSound5(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[4].isPlaying == false);
        eventManage++;
    }
    //Event 6
    public IEnumerator WaitForSound6(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[6].isPlaying == false);
        eventManage++;
    }
    //Event 7
    public IEnumerator WaitForSound7(AudioSource Sound)
    {
        compMagic.Play(0);
        yield return new WaitUntil(() => compMagic.isPlaying == false);
        talkingVoice[7].Play(0);
        yield return new WaitUntil(() => talkingVoice[7].isPlaying == false);
        eventManage++;
    }
    //Event 8
    public IEnumerator WaitForSound8(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[8].isPlaying == false);
        StartCoroutine(DrugMaker.OutOfControl());
        talkingVoice[9].Play(0);
        yield return new WaitUntil(() => talkingVoice[9].isPlaying == false);
        eventManage++;
    }
    //Event 9
    public IEnumerator WaitForSound9(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[10].isPlaying == false);
        eventManage++;
    }
    //Event 10
    public IEnumerator WaitForSound10(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[11].isPlaying == false);
        eventManage++;
    }
    public IEnumerator MiceTimer()
    {
        yield return new WaitForSeconds(10f);
        talkingVoice[11].Play(0);
        posterImage.SetTrigger("switch");
        if (stopMiceTimer == false)
        {
            StartCoroutine(MiceTimer());
        }
    }
    //Event 11
    public IEnumerator WaitForSound11(AudioSource Sound)
    {
        yield return new WaitUntil(() => talkingVoice[12].isPlaying == false);
        StartCoroutine(DrugMaker.OutOfControl());
        Drugs.deathScene = true;
        eventManage++;
    }
    //death
    public IEnumerator ChoirTimer()
    {
        yield return new WaitForSeconds(4f);
        heavenlyChoir.Stop();
        talkingVoice[13].Play(0);
        yield return new WaitForSeconds(4f);
        eventManage++;
        credits.enabled = true;
        yield return new WaitForSeconds(6f);
        Application.Quit();

    }

    //Checks for events 
    void Update()
    {
        if (eventManage == 2 && Work.amountWorked >= 6 && eventCalled[0] == false)
        {
            eventCalled[0] = true;
            talkingVoice[1].Play(0);
            StartCoroutine(WaitForSound2(talkingVoice[1]));   
        }
        if (eventManage == 3 && DrugMaker.firstPill == true && eventCalled[1] == false)
        {
            eventCalled[1] = true;
            talkingVoice[2].Play(0);
            StartCoroutine(WaitForSound3(talkingVoice[2]));
        }
        if (eventManage == 4 && Drugs.drugs == 1 && eventCalled[2] == false)
        {
            eventCalled[2] = true;
            talkingVoice[3].Play(0);
            PPB.enabled = true;
            StartCoroutine(WaitForSound4(talkingVoice[3]));
            StartCoroutine(Timer());
        }
        if (eventManage == 5 && event5Delayed == true)
        {
            talkingVoice[5].Play(0);
            event5Delayed = false;
        }
        if (eventManage == 5 && Drugs.drugs == 2 && eventCalled[3] == false)
        {
            eventCalled[3] = true;
            talkingVoice[4].Play(0);
            backgroundChatter.Stop();
            psychadelicMusic.Play(0);
            StartCoroutine(WaitForSound5(talkingVoice[4]));
        }
        if (eventManage == 6 && Drugs.drugs == 3 && eventCalled[4] == false)
        {
            eventCalled[4] = true;
            talkingVoice[6].Play(0);
            rainbowImage.SetTrigger("rainbow");
            tintTrans = 0.1f;
            var tempColor = tint.color;
            tempColor.r = 0.5f;
            tempColor.g = 0.5f;
            tempColor.b = 0.5f;
            tempColor.a = tintTrans; //1f makes it fully visible, 0f makes it fully transparent.
            tint.color = tempColor;
            rainbowImage.speed = 0.8f;
            StartCoroutine(WaitForSound6(talkingVoice[6]));
        }
        if (eventManage == 7 && Drugs.drugs == 4 && eventCalled[5] == false)
        {
            eventCalled[5] = true;
            tintTrans = 0.125f;
            var tempColor = tint.color;
            tempColor.a = tintTrans; //1f makes it fully visible, 0f makes it fully transparent.
            tint.color = tempColor;
            rainbowImage.speed = 1f;
            StartCoroutine(WaitForSound7(talkingVoice[7]));
        }
        if (eventManage == 8 && Drugs.drugs == 5 && eventCalled[6] == false)
        {
            eventCalled[6] = true;
            talkingVoice[8].Play(0);
            tintTrans = 0.15f;
            var tempColor = tint.color;
            tempColor.a = tintTrans; //1f makes it fully visible, 0f makes it fully transparent.
            tint.color = tempColor;
            rainbowImage.speed = 1.2f;
            StartCoroutine(WaitForSound8(talkingVoice[8]));
            
        }
        if (eventManage >= 9)
        {
            if (Drugs.drugs <= 10)
            {
                tintTrans = (float)(Drugs.drugs * 0.03);
            } else
            {
                tintTrans = .3f;
            }
            var tempColor = tint.color;
            tempColor.a = tintTrans; 
            tint.color = tempColor;
            if (Drugs.drugs <= 10)
            {
                rainbowImage.speed = (float)(0.7 + (Drugs.drugs * 0.1));
            } else
            {
                rainbowImage.speed = 1.8f;
            }
            
        }
        if (eventManage == 9 && Drugs.drugs >= 10 && eventCalled[7] == false)
        {
            eventCalled[7] = true;
            talkingVoice[10].Play(0);
            StartCoroutine(WaitForSound9(talkingVoice[10]));

        }
        if (eventManage == 10 && Drugs.drugs >= 40 && eventCalled[8] == false)
        {
            eventCalled[8] = true;
            talkingVoice[11].Play(0);
            DrugMaker.strayMice = true;
            mouse.startMoving = true;
            mouseObj.SetActive(true);
            rainbowImage.SetTrigger("black");
            tintTrans = 0.25f;
            var tempColor = tint.color;
            tempColor.r = 0f;
            tempColor.g = 0f;
            tempColor.b = 0f;
            tempColor.a = tintTrans; //1f makes it fully visible, 0f makes it fully transparent.
            tint.color = tempColor; 
            scaryMusic.Play(0);
            psychadelicMusic.Stop();
            StartCoroutine(WaitForSound10(talkingVoice[10]));

        }
        if (eventManage == 11 && mouseFlag == true && eventCalled[9] == false)
        {
            eventCalled[9] = true;
            talkingVoice[12].Play(0);
            StartCoroutine(WaitForSound11(talkingVoice[12]));
            
            
        }
        if (Drugs.deathScene == true && eventCalled[10] == false)
        {
                eventCalled[10] = true;
                rainbowImage.SetTrigger("blacktowhite");
                tintTrans = 1f;
                var tempColor = tint.color;
                tempColor.r = 0.8f;
                tempColor.g = 0.8f;
                tempColor.b = 0.8f;
                tempColor.a = tintTrans; //1f makes it fully visible, 0f makes it fully transparent.
                tint.color = tempColor;
                scaryMusic.Stop();
                heavenlyChoir.Play(0);
                StartCoroutine(ChoirTimer());
                
        }


    }


}
