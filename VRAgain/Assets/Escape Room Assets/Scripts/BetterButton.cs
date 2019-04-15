using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterButton : MonoBehaviour {

    public GameObject[] individualButtons;
    public Animator[] sevenAnim = new Animator[6];
    public Animator[] colorPop = new Animator[4];
    public LockPoint lockP;
    public colorsPuzzle colrPuz;
    public fifthEvent fifthE;

    public AudioSource buttonPush;
    public AudioSource dormantPush;
    public bool buttonActive; // true when AI is plugged into either puzzle
    public bool buttonPushed;

    public int[] colorArray = new int[7];
    public int buttonNumber;
    public bool rightArray;

    public void ButtonPushed (int x)
    {
        buttonPushed = true;
        buttonPush.Play(0);

        if (buttonNumber <= 6 && lockP.parentName == "ColorsTerminal" && lockP.transform.parent != null && colrPuz.solved == false && x != 0 && x!= 5 && rightArray == false && colrPuz.es2.eventSystem == 4)
        {
            //Debug.Log("button string is " + colorArray[0] + colorArray[1] + colorArray[2] + colorArray[3] + colorArray[4] + colorArray[5] + colorArray[6]);
            colorArray[buttonNumber] = x;
            sevenAnim[buttonNumber].SetInteger("color", x);
            buttonNumber++;
            //Debug.Log("button string is " + colorArray[0] + colorArray[1] + colorArray[2] + colorArray[3] + colorArray[4] + colorArray[5] + colorArray[6]);
            if (colorArray[0] == 2 && colorArray[1] == 3 && colorArray[2] == 3 && colorArray[3] == 1 && colorArray[4] == 2 && colorArray[5] == 4 && colorArray[6] == 1)
            {
                rightArray = true;
            }
        }
        else if (lockP.parentName == "StartTerminal" && lockP.transform.parent != null && x != 0 && x != 5 && colrPuz.es2.eventSystem == 5)
        {
            colorPop[x - 1].SetTrigger("flash");
            switch (x)
            {
                case 0:
                    break;

                case 1:
                    fifthE.redPressed++;
                    if (fifthE.redPressed == fifthE.redCodeNumber)
                    {
                        colorPop[0].SetBool("flashing", true);
                    } else if (fifthE.redPressed > fifthE.redCodeNumber)
                    {
                        fifthStopFlashing();
                        resetPress();
                    }
                    break;

                case 2:
                    fifthE.yellowPressed++;
                    if (fifthE.yellowPressed == fifthE.yellowCodeNumber)
                    {
                        colorPop[1].SetBool("flashing", true);
                    }
                    else if (fifthE.yellowPressed > fifthE.yellowCodeNumber)
                    {
                        fifthStopFlashing();
                        resetPress();
                    }
                    break;

                case 3:
                    fifthE.greenPressed++;
                    if (fifthE.greenPressed == fifthE.greenCodeNumber)
                    {
                        colorPop[2].SetBool("flashing", true);
                    }
                    else if (fifthE.greenPressed > fifthE.greenCodeNumber)
                    {
                        fifthStopFlashing();
                        resetPress();
                    }
                    break;

                case 4:
                    fifthE.bluePressed++;
                    if (fifthE.bluePressed == fifthE.blueCodeNumber)
                    {
                        colorPop[3].SetBool("flashing", true);
                    }
                    else if (fifthE.bluePressed > fifthE.blueCodeNumber)
                    {
                        fifthStopFlashing();
                        resetPress();
                    }
                    break;
            }
        }
        else if ((colrPuz.es2.eventSystem == 4 || colrPuz.es2.eventSystem == 5) && x == 0)
        {
            WipeButton();
        }
        else if (colrPuz.es2.eventSystem == 4)
        {
            colrPuz.FailPlay();
            WipeButton();
        } 
    }

    public void resetPress ()
    {
        fifthE.redPressed = 0;
        fifthE.yellowPressed = 0;
        fifthE.greenPressed = 0;
        fifthE.bluePressed = 0;
    }

    public void sevenAnimEnd ()
    {
        for (int i = 0; i <= 6; i++)
        {
            sevenAnim[i].SetTrigger("end");
            sevenAnim[i].SetInteger("color", 0);
        }
    }

    public void sevenAnimWhite()
    {
        for (int i = 0; i <= 6; i++)
        {
            sevenAnim[i].SetTrigger("white");
            sevenAnim[i].SetInteger("color", 0);
        }
    }

    public void fifthStopFlashing()
    {
        for (int i = 0; i <= 3; i++)
        {
            colorPop[i].SetBool("flashing", false);
        }
    }

    public void WipeButton ()
    {
        Debug.Log("ButtonWipe");
        buttonNumber = 0;
        dormantPush.Play(0);
        sevenAnimEnd();
        sevenAnimWhite();
        for (int i = 0; i <= 6; i++)
        {
            colorArray[i] = 0;
        }

    }

    public void DormantButtonPushed()
    {
        dormantPush.Play(0);
    }
}
