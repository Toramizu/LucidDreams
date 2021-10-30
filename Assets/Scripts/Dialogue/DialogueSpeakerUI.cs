using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSpeakerUI : MonoBehaviour
{
    [SerializeField] Image image;

    public bool Focus
    {
        set
        {
            Color c = image.color;

            if (value)
                c.a = 1f;
            else
                c.a = .5f;

            image.color = c;
        }
    }


    [SerializeField] TMP_Text text;
    public string Text
    {
        get { return text.text; }
        set
        {
            if (value == null)
            {
                Toggle(false);
            }
            else
            {
                Toggle(true);
                text.text = value;
            }
        }
    }

    public void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
