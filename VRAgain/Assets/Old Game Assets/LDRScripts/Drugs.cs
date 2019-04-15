using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Drugs : MonoBehaviour {

    public static int drugs = 0;
    public AudioSource popPill;
    public AudioSource[] noStop;
    public ScriptedEvents ScriptedEvents;
    public bool OutOfControl;
    public int audioMuter;
    public bool deathScene;
    public int deathDrug;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drug"))
        {
            if (deathScene == false)
            {
                drugs++;
            } else
            {
                deathDrug++;
            }

            if (OutOfControl == true)
            {
                int x = Mathf.RoundToInt(Random.value);
                audioMuter = 0;
                for (int i = 0; i < 12; i++)
                {

                    if (ScriptedEvents.talkingVoice[i].isPlaying)
                    {
                        audioMuter++;
                    }
                }
                if (audioMuter >= 1 || noStop[x].isPlaying == true)
                {
                    noStop[x].mute = true;
                }
                else
                {
                    noStop[x].mute = false;
                }

                noStop[x].Play(0);
            }
                popPill.Play(0);
            Destroy(other.gameObject);
            DrugMaker.drugExists = false;
        }
    }
}
