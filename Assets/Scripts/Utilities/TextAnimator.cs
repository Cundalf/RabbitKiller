using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Text))]
public class TextAnimator : MonoBehaviour
{
    [Tooltip("Tiempo de pausa entre letra y letra")]
    public float pauseTime = 0.05f;
    [Tooltip("Delay para incrementar un valor")]
    public float numberDelay = 0.1f;

    private bool finished;
    public bool Finished
    {
        get
        {
            return finished;
        }
    }

    [Tooltip("Saltear texto actual")]
    public bool skip;

    [Tooltip("El usuario puede saltear texto o incremento numerico")]
    public bool canSkip;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    void Start()
    {
        skip = false;
        finished = true;

        if (_text.text != "")
        {
            SetText(_text.text);
        }
    }

    void Update()
    {
        //TODO: Evaluar como pasar el click a InputManager
        if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse) && canSkip)
        {
            skip = true;
        }
    }

    public void SetText(string txt)
    {
        finished = false;
        _text.text = "";
        StartCoroutine(TypeLetters(txt));
    }

    IEnumerator TypeLetters(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            if (skip)
            {
                _text.text = text;
                skip = false;
                break;
            }

            _text.text += letter;
            yield return new WaitForSeconds(pauseTime);
        }

        finished = true;
    }

    public void SetIncrement(int val)
    {
        finished = false;
        _text.text = "0";
        StartCoroutine(IncrementValue(val));
    }

    IEnumerator IncrementValue(int value)
    {
        int actualValue = 0;
        for (int i = 1; i <= value; i++)
        {
            if (skip)
            {
                _text.text = actualValue.ToString();
                skip = false;
                break;
            }

            actualValue++;
            _text.text = actualValue.ToString();
            yield return new WaitForSeconds(numberDelay);
        }

        finished = true;
    }
}
