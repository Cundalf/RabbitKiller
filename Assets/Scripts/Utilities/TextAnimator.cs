using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Text))]
public class TextAnimator : MonoBehaviour
{
    public bool finished;
    public float pauseTime = 0.05f;
    private Text _text;
    public bool skip;
    
    void Start()
    {
        skip = false;
        finished = true;
        _text = GetComponent<Text>();

        if(_text.text != "")
        {
            SetText(_text.text);
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) {
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
            if(skip)
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
}
