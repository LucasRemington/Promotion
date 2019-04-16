using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventScript2 : MonoBehaviour {

    public int eventSystem = 0; //int tracking which puzzles are active
   
    public firstEvent firstE; //scripts for each event
    public secondEvent secondE;
    public thirdEvent thirdE;
    public fourthEvent fourthE;
    public fifthEvent fifthE;

    public AudioSource[] AIVoice; // Audio files for narration
    public AudioSource gettingFired;
    public AudioSource longVictory;
    public AudioSource positiveBeep;
    public AudioSource smallVictory;
    public AudioSource smallSuccess;
    public AudioSource currentAudio; // currently playing audio

    public AudioSource backgroundRumble; //background rumbling

    public AudioSource interCom; //Intercom SE
    public AudioSource partyHorn; // Horn SE

    public AudioListener playerListen;

	void Start () {
        switchCheck(); // Calls switchcheck
	}

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    public void switchCheck () {
        switch (eventSystem)
        {
            case 0:
                StartCoroutine(firstE.Begin());
                break;

            case 1:
                StartCoroutine(secondE.Begin());
                break;

            case 2:
                StartCoroutine(thirdE.Begin());
                break;

            case 3:
                StartCoroutine(fourthE.Begin());
                break;

            case 4:
                StartCoroutine(fifthE.Begin());
                break;

        }
    }

    public void currentStop ()
    {
        if (currentAudio.isPlaying == true)
        {
            currentAudio.Stop();
        }
    }

    public void SuccessBeep ()
    {
        smallSuccess.Play(0);
    }

    public IEnumerator GameOver()
    {
        Debug.Log("GameOver");
        currentStop();
        currentAudio = gettingFired;
        gettingFired.Play(0);
        yield return new WaitForSeconds(7f);
        StartCoroutine(fadeToDeath());
    }

    public IEnumerator fadeToDeath ()
    {
        firstE.fadeIn.SetTrigger("fade");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ERMainMenu");
        Debug.Log("credits");
    }

}
