using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Text promotion;
    public Text instruction;
    public float speed;
    public bool instruct;

    private void Start()
    {
        promotion.color = new Color(promotion.color.r, promotion.color.g, promotion.color.b, 0);
        instruction.color = new Color(instruction.color.r, instruction.color.g, instruction.color.b, 0);
        StartCoroutine(countToThree());
    }

    void Update () {
        StartCoroutine(FadeTextToFullAlpha(1f, promotion));
        if (instruct == true)
        {
            StartCoroutine(FadeTextToFullAlpha(1f, instruction));
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / (speed * t)));
            yield return null;
        }
    }

    public IEnumerator countToThree ()
    {
        yield return new WaitForSeconds(3f);
        instruct = true;
    }

    public void StartGame ()
    {
        SceneManager.LoadScene("EscapeRoom");
    }

}
