using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // The target marker.
    public Transform target;
    public Transform target2;
    public bool startMoving;
    public bool loopMovement;
    public int loopNumber;

    // Speed in units per sec.
    public float speed;

    private void Start()
    {
        loopNumber = 1;
    }

    void Update()
    {
        if (startMoving == true && loopNumber == 1)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            if (loopMovement == true)
            {
                loopNumber = 2;
            }
        }
        else if (startMoving == true && loopNumber == 2)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target2.position, step);
            if (loopMovement == true)
            {
                loopNumber = 1;
            }
        }
    }
}
