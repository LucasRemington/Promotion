using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public EventScript2 es2;
    public TextMesh timerText;
    public int startMinutes;
    public int startSeconds;
    public AudioSource AIFiveMin;
    public AudioSource AIOneMin;
    public AudioSource AITenSeconds;
    public AudioSource heartbeat;
    public bool stopTiming;
    public bool fiveMin;
    public bool oneMin;

    public IEnumerator timerCallouts (int time)
    {
        if (time == 5)
        {
            yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
            yield return new WaitForSeconds(0.5f);
            Debug.Log("five minutes remaining!");
            es2.currentAudio = AIFiveMin;
            AIFiveMin.Play(0);
            fiveMin = true;
        } else if (time == 1)
        {
            yield return new WaitUntil(() => es2.currentAudio.isPlaying == false);
            yield return new WaitForSeconds(0.5f);
            Debug.Log("one minute remaining!");
            es2.currentAudio = AIOneMin;
            AIOneMin.Play(0);
            oneMin = true;
        }
    }


    // Calls once per second
    public IEnumerator TimerCount()
    {
        if (heartbeat.isPlaying == false)
        {
            heartbeat.Play(0);
        }
        yield return new WaitForSeconds(1.0f);
        
        if (startSeconds == 0 && stopTiming == false)
        {
            startMinutes--;
            startSeconds = 59;
            heartbeat.volume = heartbeat.volume + 0.05f;
        } else if (stopTiming == false)
        {
            startSeconds--;
        }

        if (startMinutes == 5 && startSeconds == 0 && fiveMin == false)
        {
            StartCoroutine(timerCallouts(5));
        }
        else if (startMinutes == 1 && startSeconds == 0 && oneMin == false)
        {
            StartCoroutine(timerCallouts(1));
        }
        else if (startMinutes == 0 && startSeconds == 10)
        {
            StartCoroutine(es2.GameOver());

        }
        if (startSeconds < 10)
        {
            timerText.text = startMinutes.ToString() + ":0" + startSeconds.ToString();
        } else
        {
            timerText.text = startMinutes.ToString() + ":" + startSeconds.ToString();
        }
        StartCoroutine(TimerCount());
    }
}
