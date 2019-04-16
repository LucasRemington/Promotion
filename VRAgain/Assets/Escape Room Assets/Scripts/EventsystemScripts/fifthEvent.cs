using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fifthEvent : MonoBehaviour {

    public EventScript2 es2;
    public thirdEvent thirdE;
    public heatedPuzzle heatPuz;
    public patiencePuzzle patiPuz;
    public colorsPuzzle colrPuz;
    public brokenPuzzle brokPuz;
    public GameObject[] colorPop;

    public GameObject[] lightObjects;
    public GameObject[] mediumObjects;
    public GameObject[] heavyObjects;
    public GameObject wholeRoom;
    public GameObject[] target;
    public GameObject[] canTele;
    public GameObject airBlock;
    public Animator airLock;
    public AudioSource endingAudio;
    public AudioSource suckingAir;
    public GameObject teleboy;
    public GameObject stars;

    public float speed;
    public float torque;
    public int sendFlying;

    public int blueCodeNumber;
    public int redCodeNumber;
    public int yellowCodeNumber;
    public int greenCodeNumber;

    public int bluePressed;
    public int redPressed;
    public int greenPressed;
    public int yellowPressed;

    public AudioSource FiveStart;
    public AudioSource FiveRant;
    public AudioSource[] FiveSuccess;
    public LockPoint lockPoint;
    public BetterButton bButt;

    public GameTimer gTime;
    public bool TestEnd;
    public GameObject[] smallOBJ;

    public AudioSource correctEnter;
    public AudioSource failEnter;

    public void Start()
    {
        //StartCoroutine(succPlayer());
    }

    public IEnumerator Begin()
    {
        Debug.Log("begin event 5");
        es2.currentStop();
        Debug.Log("pressed - red:" + redCodeNumber + "blue:" + blueCodeNumber + "yellow:" + yellowCodeNumber + "green:" + greenCodeNumber);
        es2.currentAudio = FiveStart;
        FiveStart.Play(0);
        es2.eventSystem = 5;
        blueCodeNumber = brokPuz.codeNumber;
        Debug.Log("brok puz blue code:" + brokPuz.codeNumber + "blue code number:" + blueCodeNumber);
        redCodeNumber = heatPuz.codeNumber;
        Debug.Log("heat puz red code:" + heatPuz.codeNumber + "red code number:" + redCodeNumber);
        greenCodeNumber = patiPuz.codeNumber;
        Debug.Log("pati puz green code:" + patiPuz.codeNumber + "green code number:" + greenCodeNumber);
        yellowCodeNumber = colrPuz.codeNumber;
        Debug.Log("colr puz yellow code:" + colrPuz.codeNumber + "yellow code number:" + yellowCodeNumber);
        Debug.Log("pressed - red:" + redCodeNumber + "blue:" + blueCodeNumber + "yellow:" + yellowCodeNumber + "green:" + greenCodeNumber);
        yield return new WaitUntil(() => lockPoint.parentName == "StartTerminal" && lockPoint.transform.parent != null);
                lockPoint.cantPull = true;
        Debug.Log("rant");
        bButt.buttonActive = true;
        es2.currentStop();
        es2.currentAudio = FiveRant;
        FiveRant.Play(0);
        for (int i = 0; i <= 3; i++)
        {
            colorPop[i].SetActive(true);
        }
        yield return new WaitUntil(() => (redPressed == redCodeNumber && bluePressed == blueCodeNumber && greenPressed == greenCodeNumber && yellowPressed == yellowCodeNumber) || TestEnd == true);
        es2.longVictory.Play(0);
        thirdE.shipAmb.Stop();
        es2.currentStop();
        es2.currentAudio = FiveSuccess[0];
        FiveSuccess[0].Play(0);
        gTime.stopTiming = true;
        yield return new WaitForSeconds(3f);
        airBlock.SetActive(false);
        airLock.SetTrigger("open");
        thirdE.shipAmb.Stop();
        endingAudio.Play();
        yield return new WaitUntil(() => FiveSuccess[0].isPlaying == false);
        es2.currentStop();
        es2.currentAudio = FiveSuccess[1];
        FiveSuccess[1].Play(0);
        yield return new WaitForSeconds(0.1f);
        endingAudio.Play(0);
        suckingAir.Play();
        StartCoroutine(succPlayer());
        stars.GetComponent<ConstantForce>().enabled = true;
        teleboy.SetActive(false);
        yield return new WaitUntil(() => FiveSuccess[1].isPlaying == false);
        StartCoroutine(es2.fadeToDeath());
    }

    public IEnumerator succPlayer ()
    {
        yield return new WaitForSeconds(0.05f);
        wholeRoom.transform.position = new Vector3(wholeRoom.transform.position.x, wholeRoom.transform.position.y, wholeRoom.transform.position.z + 2.5f);
        for (int i = 0; i < smallOBJ.Length; i++)
        {
            smallOBJ[i].transform.position = new Vector3(smallOBJ[i].transform.position.x, smallOBJ[i].transform.position.y, smallOBJ[i].transform.position.z + 2.5f);
        }
        StartCoroutine(succPlayer());
    }

    public IEnumerator giveThemTheSucc ()
    {
        float step = speed * Time.deltaTime;
        float h = Input.GetAxis("Horizontal") * torque * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * torque * Time.deltaTime;
        if (sendFlying == 0)
        {
            for (int i = 0; i < lightObjects.Length; i++)
            {
                ConstantForce cf = lightObjects[i].AddComponent(typeof(ConstantForce)) as ConstantForce;
                lightObjects[i].GetComponent<ConstantForce>().force = new Vector3(0f, 3f, Random.Range(-20, -30));
                lightObjects[i].GetComponent<ConstantForce>().torque = new Vector3(5f, 0f, 0f);
                lightObjects[i].GetComponent<Rigidbody>().useGravity = false;
                lightObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                lightObjects[i].GetComponent<Rigidbody>().AddTorque(transform.up * h);
                lightObjects[i].GetComponent<Rigidbody>().AddTorque(transform.right * v);
            }
        }
        else if (sendFlying == 1)
        {
            for (int i = 0; i < mediumObjects.Length; i++)
            {
                ConstantForce cf = mediumObjects[i].AddComponent(typeof(ConstantForce)) as ConstantForce;
                mediumObjects[i].GetComponent<ConstantForce>().force = new Vector3(0f, 3f, Random.Range(-15, -25));
                mediumObjects[i].GetComponent<ConstantForce>().torque = new Vector3(5f, 0f, 0f);
                mediumObjects[i].GetComponent<Rigidbody>().useGravity = false;
                mediumObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                mediumObjects[i].GetComponent<Rigidbody>().AddTorque(transform.up * h);
                mediumObjects[i].GetComponent<Rigidbody>().AddTorque(transform.right * v);
            }
        }
        else if (sendFlying == 2)
        {
            for (int i = 0; i < heavyObjects.Length; i++)
            {
                //heavyObjects[i].transform.position = Vector3.MoveTowards(transform.position, target[0].transform.position, step);
                ConstantForce cf = heavyObjects[i].AddComponent(typeof(ConstantForce)) as ConstantForce;
                heavyObjects[i].GetComponent<ConstantForce>().force = new Vector3(0f, 3f, Random.Range(-10, -20));
                heavyObjects[i].GetComponent<ConstantForce>().torque = new Vector3(5f, 0f, 0f);
                heavyObjects[i].GetComponent<Rigidbody>().useGravity = false;
                heavyObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                heavyObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heavyObjects[i].GetComponent<Rigidbody>().AddTorque(transform.up * h);
                heavyObjects[i].GetComponent<Rigidbody>().AddTorque(transform.right * v);
            }
        }
        else if (sendFlying == 3)
        {
            //wholeRoom.transform.position = Vector3.MoveTowards(transform.position, target[1].transform.position, step);
            ConstantForce cf = wholeRoom.AddComponent(typeof(ConstantForce)) as ConstantForce;
            wholeRoom.GetComponent<ConstantForce>().force = new Vector3(0f, 3f, 20f);
            wholeRoom.GetComponent<ConstantForce>().torque = new Vector3(2f, 0f, 0f);
        }
        else if (sendFlying == 4)
        {
            es2.fadeToDeath();
        }
        sendFlying++;
        yield return new WaitForSeconds(5f);
        StartCoroutine(giveThemTheSucc());
    }

    public IEnumerator succTimer()
    {
        yield return new WaitForSeconds(5f);
        sendFlying++;
        yield return new WaitForSeconds(5f);
        sendFlying++;
        yield return new WaitForSeconds(5f);
        sendFlying++;
    }
 }

